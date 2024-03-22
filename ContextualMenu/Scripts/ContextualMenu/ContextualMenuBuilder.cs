using TMPro;
using UnityEngine;

namespace Plugins.ContextualMenu.Scripts.ContextualMenu {
    public class ContextualMenuBuilder : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Transform optionsContainer;
        [SerializeField] private ContextualMenuOption optionTemplate;
        
        private void Awake() {
            optionTemplate.gameObject.SetActive(false);
        }

        public void SetText(string titleText) {
            title.text = titleText;
        }

        internal ContextualMenuOption AddButton<T>(ContextualMenuData<T> menuData, BaseContextualMenu<T> contextualMenu) where T : IContextualMenuObject {
            ContextualMenuOption newOption = Instantiate(optionTemplate, optionsContainer);
            newOption.Init(menuData, contextualMenu);
            return newOption;
        }
    }
}