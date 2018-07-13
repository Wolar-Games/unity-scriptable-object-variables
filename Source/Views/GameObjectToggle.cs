using UnityEngine;
#if REACTIVE_VARIABLE_RX_ENABLED
using UniRx;
#endif

namespace WolarGames.Variables.Views
{
    public class GameObjectToggle : MonoBehaviour
    {
        public BoolReference Variable;

        /// Should this be hidden when toggle is on
        public bool HideOnToggle;

        private void Awake() {
            
#if REACTIVE_VARIABLE_RX_ENABLED
            Variable.AsObservable()
                .Subscribe(HandleVariableChange)
                .AddTo(this);
#else
            Variable.OnValueChanged += HandleVariableChange;
            HandleVariableChange(Variable.Value);
#endif
        }

#if !REACTIVE_VARIABLE_RX_ENABLED
        private void OnDestroy()
        {
            Variable.OnValueChanged -= HandleVariableChange;
        }
#endif        

        private void HandleVariableChange(bool value)
        {
            gameObject.SetActive(!HideOnToggle && value || HideOnToggle && !value);
        }
    }
}
