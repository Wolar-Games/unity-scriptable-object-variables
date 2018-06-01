using UnityEditor;
using WolarGames.Variables.Utils;

namespace WolarGames.Variables
{
    [CustomEditor(typeof(Variable<>))]
    public class VariableDrawer<T> : Editor
    {
        Variable<T> m_Instance;
        PropertyField[] m_fields;

        public void OnEnable() {
            m_Instance = target as Variable<T>;
            m_fields = ExposeProperties.GetProperties(m_Instance);
        }

        public override void OnInspectorGUI() {

            if (m_Instance == null)
                return;

            DrawDefaultInspector();
            ExposeProperties.Expose(m_fields);

        }
    }
}