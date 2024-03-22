using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace sorenGu.ContextualMenu.Scripts.ContextualMenu {
    public enum InvalidOptionBehaviour {
        Hide,
        Disable
    }

    public class ContextualMenuButton : MonoBehaviour {
        public Button button;
        [SerializeField] private TextMeshProUGUI buttonText;

        public void Init(string buttonText) {
            this.buttonText.text = buttonText;
        }

        private void Test() {
            UnityEngine.Debug.Log("b");
        }

        public bool SetValid(bool valid, InvalidOptionBehaviour invalidOptionBehaviour) {
            switch (invalidOptionBehaviour) {
                case InvalidOptionBehaviour.Disable:
                    button.interactable = valid;
                    break;
                case InvalidOptionBehaviour.Hide:
                    gameObject.SetActive(valid);
                    break;
            }

            return valid;
        }
    }
}