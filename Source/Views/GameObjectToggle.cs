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
            Variable.AsObservable().Subscribe((bool value) =>
            {
                gameObject.SetActive(!HideOnToggle && value || HideOnToggle && !value);
            }).AddTo(this);
#endif
        }
    }
}