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
        public BoolVariable variable;

        void Start() {
            var toggle = GetComponent<Toggle>();
            toggle.OnValueChangedAsObservable().Subscribe(value =>
            {
                variable.CurrentValue = value;
            }).AddTo(this);

            variable.AsObservable().Subscribe(value =>
            {
                toggle.isOn = value;
            }).AddTo(this);
        }
    }
}