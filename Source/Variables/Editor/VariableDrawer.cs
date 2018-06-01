using UnityEngine;
using UnityEditor;
using WolarGames.Variables.Utils;
using UniRx;
using System;

namespace WolarGames.Variables
{
    [CustomEditor(typeof(Variable<>))]
    public class VariableDrawer<T> : Editor
    {
        private Variable<T> m_Instance;
        private PropertyField[] m_fields;

        private IDisposable _disposable;

        public void OnEnable() {
            m_Instance = target as Variable<T>;
            m_fields = ExposeProperties.GetProperties(m_Instance);

            _disposable = m_Instance.AsObservable().Subscribe(value => {
                Repaint();
            });
        }

        public void OnDisable() {
            if (_disposable != null) {
                _disposable.Dispose();
                _disposable = null;
            }
        }

        public override void OnInspectorGUI() {

            if (m_Instance == null)
                return;

            DrawDefaultInspector();
            ExposeProperties.Expose(m_fields);

            if (!m_Instance.CurrentValue.Equals(m_Instance.DefaultValue)) {
                if (GUILayout.Button("Current -> Default")) {
                    m_Instance.DefaultValue = m_Instance.CurrentValue;
                }
            }
        }
    }
}