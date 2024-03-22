using System;
using System.Collections.Generic;
using UnityEngine;

namespace Plugins.ContextualMenu.Scripts.ContextualMenu {
    public interface IContextualMenuObject { }

    public class ContextualMenuData<T> where T : IContextualMenuObject {
        public string buttonText;
        public Func<T, bool> validationFunction;
        public Action<T> executeFunction;

        public ContextualMenuData(string buttonText, Action<T> executeFunction, Func<T, bool> validationFunction) {
            this.buttonText = buttonText;
            this.validationFunction = validationFunction;
            this.executeFunction = executeFunction;
        }
    }

    public abstract class BaseContextualMenu<T> : MonoBehaviour where T : IContextualMenuObject {
        [SerializeField] private ContextualMenuBuilder builder;

        public List<ContextualMenuOption> options { get; } = new();

        public ContextualMenuOption AddButton(ContextualMenuData<T> menuData) {
            ContextualMenuOption contextualMenuOption = builder.AddButton(menuData, this);
            options.Add(contextualMenuOption);
            return contextualMenuOption;
        }

        public void Open(string titleText, T contextualMenuObject, Transform attachTransform) {
            transform.SetParent(attachTransform, false);
            Open(titleText, contextualMenuObject);
        }

        public void Open(string titleText, T contextualMenuObject, Vector3 position) {
            transform.position = position;
            Open(titleText, contextualMenuObject);
        }

        public void Open(string titleText, T contextualMenuObject) {
            builder.SetText(titleText);
            gameObject.SetActive(true);
            Validate(contextualMenuObject);
        }

        private void Validate(T contextualMenuObject) {
            foreach (ContextualMenuOption button in options) {
                button.Validate(contextualMenuObject);
            }
        }

        public void Close() {
            gameObject.SetActive(false);
        }
    }
}