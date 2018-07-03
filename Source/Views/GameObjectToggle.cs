using UnityEngine;
using UniRx;
using WolarGames.Variables;

namespace WolarGames.Variables.Views
{
    public class GameObjectToggle : MonoBehaviour
    {
        public BoolReference Variable;

        /// Should this be hidden when toggle is on
        public bool HideOnToggle;

        private void Awake() {
            Variable.AsObservable().Subscribe((bool value) =>
            {
                gameObject.SetActive(!HideOnToggle && value || HideOnToggle && !value);
            }).AddTo(this);
        }
    }
}