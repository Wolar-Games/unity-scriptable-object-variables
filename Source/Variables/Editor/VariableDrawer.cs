using UnityEngine;
using UnityEditor;
using WolarGames.Variables.Utils;
#if REACTIVE_VARIABLE_RX_ENABLED
using UniRx;
#endif
using System;

namespace WolarGames.Variables
{
    [CustomEditor(typeof(Variable<>))]
    public class VariableDrawer<T> : Editor
    {
        private Variable<T> _target;
        private PropertyField[] _fields;

        private IDisposable _disposable;

        public void OnEnable() {
            _target = target as Variable<T>;
            _fields = ExposeProperties.GetProperties(_target);
            
#if REACTIVE_VARIABLE_RX_ENABLED
            _disposable = _target.AsObservable().Subscribe(value => {
                Repaint();
            });
#else
            _target.OnValueChanged += HandleValueChanged;
#endif
        }

        public void OnDisable() {
#if REACTIVE_VARIABLE_RX_ENABLED
            if (_disposable != null) {
                _disposable.Dispose();
                _disposable = null;
            }
#else
            _target.OnValueChanged -= HandleValueChanged;
#endif
        }

        private void HandleValueChanged(T value)
        {
            Repaint();
        }

        public override void OnInspectorGUI() {

            if (_target == null)
                return;

            DrawDefaultInspector();
            ExposeProperties.Expose(_fields);

            if (_target != null && _target.CurrentValue != null && _target.DefaultValue != null && !_target.CurrentValue.Equals(_target.DefaultValue)) {
                if (GUILayout.Button("Current -> Default")) {
                    _target.DefaultValue = _target.CurrentValue;
                }
            }
        }
    }
}