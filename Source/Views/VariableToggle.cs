using UnityEngine;
using UnityEngine.UI;
#if REACTIVE_VARIABLE_RX_ENABLED
using UniRx;
#endif

namespace WolarGames.Variables.Views
{
    [RequireComponent(typeof(Toggle))]
    public class VariableToggle : MonoBehaviour
    {
        public BoolVariable Variable;

        void Start() {
            var toggle = GetComponent<Toggle>();
            
#if REACTIVE_VARIABLE_RX_ENABLED
            toggle.OnValueChangedAsObservable().Subscribe(value =>
            {
                Variable.CurrentValue = value;
            }).AddTo(this);

            Variable.AsObservable().Subscribe(value =>
            {
                toggle.isOn = value;
            }).AddTo(this);
#endif
        }
    }
}