using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace WolarGames.Variables.Views
{
    [RequireComponent(typeof(Toggle))]
    public class VariableToggle : MonoBehaviour
    {
        public BoolVariable Variable;

        void Start() {
            var toggle = GetComponent<Toggle>();
            toggle.OnValueChangedAsObservable().Subscribe(value =>
            {
                Variable.CurrentValue = value;
            }).AddTo(this);

            Variable.AsObservable().Subscribe(value =>
            {
                toggle.isOn = value;
            }).AddTo(this);
        }
    }
}