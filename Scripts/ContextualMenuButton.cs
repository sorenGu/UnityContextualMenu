using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UnityContextualMenu.Scripts {
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

        public void SetValid(bool valid, InvalidOptionBehaviour invalidOptionBehaviour) {
            switch (invalidOptionBehaviour) {
                case InvalidOptionBehaviour.Disable:
                    button.interactable = valid;
                    break;
                case InvalidOptionBehaviour.Hide:
                    gameObject.SetActive(valid);
                    break;
            }
        }
    }
}