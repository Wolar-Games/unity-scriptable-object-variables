using System;
using UnityEngine;
using WolarGames.Variables.Utils;
#if REACTIVE_VARIABLE_RX_ENABLED
using UniRx;
#endif
using System.Collections.Generic;

namespace WolarGames.Variables
{
    /// Generic base variable that all other variables inherit
    public abstract class Variable<T> : ScriptableObject
    {
        // This used to be #if UNITY_EDITOR but there was a problem with memory layout on desktop platforms
        [Multiline]
        public string DeveloperDescription = "";

        /// Default value of the variable, exposed in editor if T is serializable, should not be changed from game code without a good reason
        public T DefaultValue;

        // If this is set to true, setting the same value will notify the observers
        [HideInInspector]
        public bool AllowValueRepeating;

        private T _currentValue;
        /// Current value of the variable
        [ExposeProperty]
        public T CurrentValue {
            get {
                return _currentValue;
            }
            set {
                // TODO: Make the comparer setable
                if (AllowValueRepeating || !EqualityComparer<T>.Default.Equals(_currentValue, value)) {
                    _currentValue = value;
#if REACTIVE_VARIABLE_RX_ENABLED
                    if (_publisher != null) {
                        _publisher.OnNext(value);
                    }
#else
                    if (OnValueChanged != null) {
                        OnValueChanged(value);
                    }
#endif
                }
            }
        }
        
#if REACTIVE_VARIABLE_RX_ENABLED
        [NonSerialized]
        private BehaviorSubject<T> _publisher;

        public IObservable<T> AsObservable() {
            if (_publisher == null) {
                _publisher = new BehaviorSubject<T>(_currentValue);
            }
            return _publisher;
        }
#else
        public Action<T> OnValueChanged;
#endif

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
