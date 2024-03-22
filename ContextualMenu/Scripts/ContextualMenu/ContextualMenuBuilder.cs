using System;
using TMPro;
using UnityEngine;

namespace Plugins.ContextualMenu.Scripts.ContextualMenu {
    public interface IContextualMenuObject { }

    public interface IContextualMenuData<in T> where T : IContextualMenuObject {
        public string buttonText {get;}
        public Func<T, bool> validationFunction {get;}
        public Action<T> executeFunction {get;}
    }

    public class ContextualMenuData<T>: IContextualMenuData<T> where T: IContextualMenuObject {
        public string buttonText { get; private set; }
        public Func<T, bool> validationFunction { get; private set; }
        public Action<T> executeFunction { get; private set; }

        public ContextualMenuData(string buttonText, Action<T> executeFunction, Func<T, bool> validationFunction) {
            this.buttonText = buttonText;
            this.validationFunction = validationFunction;
            this.executeFunction = executeFunction;
        }
    }
    
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

        internal ContextualMenuOption AddButton<T>(ContextualMenuData<T> menuData, IBaseContextualMenu contextualMenu) where T : IContextualMenuObject {
            ContextualMenuOption newOption = Instantiate(optionTemplate, optionsContainer);
            newOption.Init((IContextualMenuData<IContextualMenuObject>)menuData, contextualMenu);
            return newOption;
        }
    }
}