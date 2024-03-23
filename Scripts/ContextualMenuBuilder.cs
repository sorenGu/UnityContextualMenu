using TMPro;
using UnityEngine;

namespace UnityContextualMenu.Scripts {
    public class ContextualMenuBuilder : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Transform optionsContainer;
        [SerializeField] private ContextualMenuButton buttonTemplate;

        private void Start() {
            buttonTemplate.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        public void SetText(string titleText) {
            title.text = titleText;
        }

        internal ContextualMenuButton AddButton(string buttonText) {
            ContextualMenuButton newButton = Instantiate(buttonTemplate, optionsContainer);
            newButton.Init(buttonText);
            return newButton;
        }
    }
}