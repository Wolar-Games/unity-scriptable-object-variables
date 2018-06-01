using System;
using UnityEngine;
using UnityEngine.Assertions;
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

        /// Current value of the variable
        [NonSerialized]
        public ReactiveProperty<T> CurrentValue = new ReactiveProperty<T>();

        public void SetValue(T value) {
            CurrentValue.Value = value;
        }

        public void SetValue(Variable<T> value) {
            CurrentValue.Value = value.CurrentValue.Value;
        }

        private void OnEnable() {
            CurrentValue = new ReactiveProperty<T>(DefaultValue);
        }

        public static implicit operator T(Variable<T> variable) {
            Assert.IsNotNull(variable);
            return variable.CurrentValue.Value;
        }
    }
}
