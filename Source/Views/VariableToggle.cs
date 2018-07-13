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

        private Toggle _toggle;

        void Start() {
            _toggle = GetComponent<Toggle>();
            
#if REACTIVE_VARIABLE_RX_ENABLED
            _toggle.OnValueChangedAsObservable()
                .Subscribe(HandleToggleValueChanged)
                .AddTo(this);

            Variable.AsObservable()
                .Subscribe(HandleValueChanged)
                .AddTo(this);
#else
            _toggle.onValueChanged.AddListener(HandleToggleValueChanged);
            Variable.OnValueChanged += HandleValueChanged;
            HandleValueChanged(Variable.CurrentValue);
#endif
        }
        
#if !REACTIVE_VARIABLE_RX_ENABLED

        private void OnDestroy()
        {
            _toggle.onValueChanged.RemoveListener(HandleToggleValueChanged);
            Variable.OnValueChanged -= HandleValueChanged;
        }

#endif

        private void HandleToggleValueChanged(bool value)
        {
            Variable.CurrentValue = value;
        }
        
        private void HandleValueChanged(bool value)
        {
            _toggle.isOn = value;
        }
    }
}
