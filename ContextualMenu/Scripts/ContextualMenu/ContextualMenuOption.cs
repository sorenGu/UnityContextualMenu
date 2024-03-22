using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.ContextualMenu.Scripts.ContextualMenu {
    public enum InvalidOptionBehaviour {
        Hide,
        Disable
    }

    public class ContextualMenuOption : MonoBehaviour {
        public Action<IContextualMenuObject, IContextualMenuData<IContextualMenuObject>> onInvalidExecution;
        
        public InvalidOptionBehaviour invalidOptionBehaviour;
        public bool validateAgainBeforeExecution = true;
        public bool closeAfterMenuAfterClicking = true;
        
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI buttonText;
        
        private IContextualMenuData<IContextualMenuObject> menuData;
        private IContextualMenuObject contextualMenuObject;
        private IBaseContextualMenu parentMenu;
        
        public void Init(IContextualMenuData<IContextualMenuObject> menuData, IBaseContextualMenu parentMenu) {
            this.menuData = menuData;
            this.parentMenu = parentMenu;
            buttonText.text = this.menuData.buttonText;
            button.onClick.AddListener(Execute);
        }
        
        private void Execute() {
            if (closeAfterMenuAfterClicking) {
                parentMenu.Close();
            }

            if (validateAgainBeforeExecution && !Validate(contextualMenuObject)) {
                onInvalidExecution?.Invoke(contextualMenuObject, menuData);
                return;
            }

            menuData.executeFunction(contextualMenuObject);
        }

        internal bool Validate(IContextualMenuObject contextualMenuObject) {
            this.contextualMenuObject = contextualMenuObject;
            bool valid = menuData.validationFunction(contextualMenuObject);
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