using TMPro;
using UnityEngine;

namespace sorenGu.UnityContextualMenu.Scripts {
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

        internal ContextualMenuButton AddButton(ContextualOption option) {
            ContextualMenuButton newButton = Instantiate(buttonTemplate, optionsContainer);
            newButton.Init(option);
            if (option.closeAfterMenuAfterClicking) {
                newButton.button.onClick.AddListener(() => gameObject.SetActive(false));
            }
            return newButton;
        }

        public void RemoveButtons() {
            foreach(Transform child in optionsContainer) {
                if (child == buttonTemplate.transform) continue;
                
                Destroy(child.gameObject);
            }
        }
    }
}