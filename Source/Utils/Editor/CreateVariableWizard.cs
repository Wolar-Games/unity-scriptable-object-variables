using UnityEditor;
using UnityEngine;

namespace WolarGames.Variables.Utils
{
    public class CreateVariableWizard : ScriptableWizard
    {
        public string VariableType;

        public string StorePath;
        public string StoreEditorPath;

        private const string StorePathKey = "Wolargames.Variables.StorePath";
        private const string EditorStorePathKey = "Wolargames.Variables.EditorStorePath";
        
        [MenuItem("Tools/Reactive Variables/Create New")]
        static void CreateWizard()
        {
            var wizard = DisplayWizard<CreateVariableWizard>("Create Variable", "Create");
            wizard.StorePath = EditorPrefs.GetString(StorePathKey, null);
            wizard.StoreEditorPath = EditorPrefs.GetString(EditorStorePathKey, null);
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
            
            // TODO: Create class here
        }

        protected override bool DrawWizardGUI()
        {
            // TODO: selector of class
            
            // Path to folder
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            GUILayout.Label("Where to store class:");
            GUILayout.Label(StorePath ?? "Please pick" );
            EditorGUILayout.EndVertical();
            if (GUILayout.Button("Select"))
            {
                // TODO: Selector of path
                Debug.Log("Select path");
            }
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            GUILayout.Label("Where to store editor class:");
            GUILayout.Label(StoreEditorPath  ?? "Please pick");
            EditorGUILayout.EndVertical();
            if (GUILayout.Button("Select"))
            {
                // TODO: Selector of path
                Debug.Log("Select path");
            }
            EditorGUILayout.EndHorizontal();
            
            return false;
        }
    }
}