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
        private Variable<T> _target;
        private PropertyField[] _fields;

        private IDisposable _disposable;

        public void OnEnable() {
            _target = target as Variable<T>;
            _fields = ExposeProperties.GetProperties(_target);

            _disposable = _target.AsObservable().Subscribe(value => {
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

            if (_target == null)
                return;

            DrawDefaultInspector();
            ExposeProperties.Expose(_fields);

            if (!_target.CurrentValue.Equals(_target.DefaultValue)) {
                if (GUILayout.Button("Current -> Default")) {
                    _target.DefaultValue = _target.CurrentValue;
                }
            }
        }
    }
}