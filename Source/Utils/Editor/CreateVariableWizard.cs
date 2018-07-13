using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace WolarGames.Variables.Utils
{
    public class CreateVariableWizard : ScriptableWizard
    {
        public string VariableType;

        public string StorePath;
        public string StoreEditorPath;

        public IEnumerable<Type> AllTypes;

        private string _newString;
        private string _filterString;
        private IEnumerable<Type> _filteredTypes;
        private string[] _filteredTypeNames;

        private int _selectedIndex = 0;
        private float _timeSinceLastChange = 0;
        public bool ImportAfterCreating = true;
        
        private const float TimeSinceLastChangeTreshold = 0.5f;
        
        private const string StorePathKey = "Wolargames.Variables.StorePath";
        private const string EditorStorePathKey = "Wolargames.Variables.EditorStorePath";
        private const string ImportingKey = "Wolargames.Variables.Importing";
        
        [MenuItem("Tools/Reactive Variables/Create New", false, 1)]
        static void CreateWizardLocal()
        {
            CreateNewWizard(new []
            {
                typeof(IntVariable).Assembly
            });
        }

        [MenuItem("Tools/Reactive Variables/Create New (All assemblies)", false, 2)]
        static void CreateWizardAllAssemblies()
        {
            CreateNewWizard(AppDomain.CurrentDomain.GetAssemblies());
        }

        private static void CreateNewWizard(Assembly[] assemblies)
        {
            var wizard = DisplayWizard<CreateVariableWizard>("Create Variable", "Create");
            wizard.StorePath = EditorPrefs.GetString(StorePathKey, null);
            wizard.StoreEditorPath = EditorPrefs.GetString(EditorStorePathKey, null);
            wizard.ImportAfterCreating = EditorPrefs.GetBool(ImportingKey, true);

            var allTypes = new List<Type>();
            foreach (var type in assemblies)
            {
                var types = type.GetTypes();
                allTypes.AddRange(types);
            }

            wizard.AllTypes = allTypes.OrderBy(type => type.Name);
        }

        void OnWizardCreate()
        {
            if (string.IsNullOrEmpty(StorePath))
            {
                Debug.LogError("Please select store where to store created class");
                return;
            }
            
            if (string.IsNullOrEmpty(StoreEditorPath))
            {
                Debug.LogError("Please select store where to store created editor class");
                return;
            }
            
            CreateFiles();
        }

        protected override bool DrawWizardGUI()
        {
            var somethingChanged = false;
            
            // Path to folder
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            GUILayout.Label("Where to store class:", EditorStyles.boldLabel);
            GUILayout.Label(string.IsNullOrEmpty(StorePath) ? "Please pick" : StorePath );
            EditorGUILayout.EndVertical();
            if (GUILayout.Button("Select"))
            {
                string path = EditorUtility.OpenFolderPanel("Select folder", "", "");
                if (path != null)
                {
                    EditorPrefs.SetString(StorePathKey, path);
                    StorePath = path;
                    somethingChanged = true;
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            GUILayout.Label("Where to store editor class:", EditorStyles.boldLabel);
            GUILayout.Label(string.IsNullOrEmpty(StoreEditorPath) ? "Please pick" : StoreEditorPath);
            EditorGUILayout.EndVertical();
            if (GUILayout.Button("Select"))
            {
                string path = EditorUtility.OpenFolderPanel("Select folder", "", "");
                if (path != null)
                {
                    EditorPrefs.SetString(EditorStorePathKey, path);
                    StoreEditorPath = path;
                    somethingChanged = true;
                }
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Label("Filter:", EditorStyles.boldLabel);
            var newString = GUILayout.TextField(_newString);
            if (newString != _newString)
            {
                _timeSinceLastChange = 0;
                _newString = newString;
            }

            if ((_timeSinceLastChange >= TimeSinceLastChangeTreshold && _newString != _filterString))
            {
                _filterString = _newString;
                if (_filterString.Length >= 3)
                {
                    _filteredTypes = AllTypes.Where(type => type.Name.Contains(_filterString));
                    _filteredTypeNames = _filteredTypes.Select(type => type.Name).ToArray();
                }
            }

            _timeSinceLastChange += Time.deltaTime;

            if (_filterString != null && _filterString.Length >= 3 && _filteredTypeNames != null)
            {
                _selectedIndex = EditorGUILayout.Popup("Class", _selectedIndex, _filteredTypeNames);
                isValid = _filteredTypeNames.Length > 0;
            }
            else
            {
                GUILayout.Label("Please enter at leasr 3 characters into filter");
                isValid = false;
            }

            var newValue = GUILayout.Toggle(ImportAfterCreating, "Import after created");
            if (newValue != ImportAfterCreating)
            {
                ImportAfterCreating = newValue;
                EditorPrefs.SetBool(ImportingKey, ImportAfterCreating);
            }
            
            return somethingChanged;
        }

        private void CreateFiles()
        {
            CreateScript("template_Variable", StorePath, "Variable");
            CreateScript("template_Reference", StorePath, "Reference");
            CreateScript("template_Editor_VariableDrawer", StoreEditorPath, "VariableDrawer");
            CreateScript("template_Editor_ReferenceDrawer", StoreEditorPath, "ReferenceDrawer");
        }

        private void CreateScript(string templaName, string targetFolder, string filenameSuffix)
        {
            Assert.IsTrue(Directory.Exists(targetFolder), targetFolder + " does not exist");

            var guids = AssetDatabase.FindAssets(templaName);
            Assert.IsTrue(guids.Length > 0, string.Format("Missing template: \"{0}\"", templaName));
            Assert.IsTrue(guids.Length < 2, string.Format("To many templates for name: \"{0}\"", templaName));
            
            var firstGuid = guids.First();
            var templatePath = AssetDatabase.GUIDToAssetPath(firstGuid);
            try
            {
                var type = _filteredTypes.ElementAt(_selectedIndex);
                var template = File.ReadAllText(templatePath);
                template = template.Replace("#VARIABLE#", type.Name);
                
                if (!string.IsNullOrEmpty(type.Namespace))
                {
                    template = template.Replace("#NAMESPACE#", type.Namespace);
                    template = template.Replace("#NAMESPACE_KEYWORD#", "namespace");
                    template = template.Replace("#NAMESPACE_OPEN#", "{");
                    template = template.Replace("#NAMESPACE_CLOSE#", "}");
                }
                else
                {
                    template = template.Replace("#NAMESPACE#", string.Empty);
                    template = template.Replace("#NAMESPACE_KEYWORD#", string.Empty);
                    template = template.Replace("#NAMESPACE_OPEN#", string.Empty);
                    template = template.Replace("#NAMESPACE_CLOSE#", string.Empty);
                }
                
                var filename = string.Format("{0}{1}.cs", type.Name, filenameSuffix);
                var path = Path.Combine(targetFolder, filename);
                File.WriteAllText(path, template);
                
                if (path.StartsWith(Application.dataPath)) {
                    var relativePath =  "Assets" + path.Substring(Application.dataPath.Length);
                    Debug.LogFormat("Created file at path: {0}", relativePath);
                    if (ImportAfterCreating) {
                        AssetDatabase.ImportAsset(relativePath, ImportAssetOptions.Default);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}