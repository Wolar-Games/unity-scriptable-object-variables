using UnityEngine;
using UniRx;
using WolarGames.Variables;

namespace WolarGames.Variables.Views
{
    public class GameObjectToggle : MonoBehaviour
    {
        public BoolReference variable;

        /// Should this be hidden when toggle is on
        public bool hideOnToggle;

        private void Awake() {
            variable.Value.Subscribe((bool value) =>
            {
                gameObject.SetActive(!hideOnToggle && value || hideOnToggle && !value);
            }).AddTo(this);
        }
    }
}