using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace WolarGames.Variables.Utils
{
    public class EnableRxEditor: MonoBehaviour
    {
        [MenuItem("Tools/Reactive Variables/Enable UniRx Integration", false, 15)]
        static void Enable()
        {
            var symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(GetCurrentBuildGroup());
            symbols += ";REACTIVE_VARIABLE_RX_ENABLED";
            PlayerSettings.SetScriptingDefineSymbolsForGroup(GetCurrentBuildGroup(), symbols);
        }
        
        [MenuItem("Tools/Reactive Variables/Disable UniRx Integration", false, 16)]
        static void Disable()
        {
            var symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(GetCurrentBuildGroup());
            symbols = symbols.Replace("REACTIVE_VARIABLE_RX_ENABLED", string.Empty);
            PlayerSettings.SetScriptingDefineSymbolsForGroup(GetCurrentBuildGroup(), symbols);
        }
        
        [MenuItem("Tools/Reactive Variables/Enable UniRx Integration", true, 15)]
        static bool CanEnable()
        {
            return !ContainsPreprocessor();
        }
        
        [MenuItem("Tools/Reactive Variables/Disable UniRx Integration", true, 16)]
        static bool CanDisable()
        {
            return ContainsPreprocessor();
        }

        private static bool ContainsPreprocessor()
        {
            // TODO: Improve this
            var symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(GetCurrentBuildGroup());
            return symbols.Contains("REACTIVE_VARIABLE_RX_ENABLED");
        }

        private static BuildTargetGroup GetCurrentBuildGroup()
        {
            var target = EditorUserBuildSettings.activeBuildTarget;
            return BuildPipeline.GetBuildTargetGroup(target);
        }
    }
}