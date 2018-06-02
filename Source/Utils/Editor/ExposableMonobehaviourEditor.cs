using UnityEditor;

// Idea taken from: http://wiki.unity3d.com/index.php/ExposePropertiesInInspector_Generic
// Original author: Venryx (venryx) (variant from Mift (mift) )
// Variant author: Tiago Roldão(Tiago Roldão) 

namespace WolarGames.Variables.Utils
{
    [CustomEditor(typeof(ExposableMonoBehaviour), true)]
    public class ExposableMonobehaviourEditor : Editor
    {
        ExposableMonoBehaviour m_Instance;
        PropertyField[] m_fields;

        public virtual void OnEnable() {
            m_Instance = target as ExposableMonoBehaviour;
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