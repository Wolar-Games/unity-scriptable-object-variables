using System;
using UnityEngine;
using WolarGames.Variables.Utils;
using UniRx;

namespace WolarGames.Variables
{
    /// Generic base variable that all other variables inherit
    public abstract class Variable<T> : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        /// Default value of the variable, exposed in editor if T is serializable, should not be changed from game code without a good reason
        public T DefaultValue;

        private T _currentValue;
        /// Current value of the variable
        [ExposeProperty]
        public T CurrentValue {
            get {
                return _currentValue;
            }
            set {
                if (!_currentValue.Equals(value)) {
                    _currentValue = value;
                    if (_publisher != null) {
                        _publisher.OnNext(value);
                    }
                }
            }
        }

        [NonSerialized]
        private BehaviorSubject<T> _publisher;

        public IObservable<T> AsObservable() {
            if (_publisher == null) {
                _publisher = new BehaviorSubject<T>(_currentValue);
            }
            return _publisher;
        }

        public void SetValue(Variable<T> value) {
            CurrentValue = value.CurrentValue;
        }

        private void OnEnable() {
            CurrentValue = DefaultValue;
        }

        public static implicit operator T(Variable<T> variable) {
            if (variable == null) {
                return default(T);
            }
            return variable.CurrentValue;
        }
    }
}
