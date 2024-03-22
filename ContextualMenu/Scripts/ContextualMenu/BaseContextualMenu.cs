using System;
using System.Collections.Generic;
using UnityEngine;

namespace Plugins.ContextualMenu.Scripts.ContextualMenu {
    public interface IContextualMenuObject { }

    public class ContextualOption<T> where T : IContextualMenuObject {
        public InvalidOptionBehaviour invalidOptionBehaviour;
        public bool validateAgainBeforeExecution;
        public bool closeAfterMenuAfterClicking;
        public string buttonText { get; }
        public Func<T, bool> validationFunction { get; }
        public Action<T> executeFunction { get; }

        internal ContextualMenuButton button;

        public ContextualOption(
            string buttonText,
            Action<T> executeFunction,
            Func<T, bool> validationFunction,
            InvalidOptionBehaviour invalidOptionBehaviour = InvalidOptionBehaviour.Hide,
            bool validateAgainBeforeExecution = true,
            bool closeAfterMenuAfterClicking = true) {
            
            this.buttonText = buttonText;
            this.validationFunction = validationFunction;
            this.executeFunction = executeFunction;
            this.invalidOptionBehaviour = invalidOptionBehaviour;
            this.validateAgainBeforeExecution = validateAgainBeforeExecution;
            this.closeAfterMenuAfterClicking = closeAfterMenuAfterClicking;
        }
    }

    public abstract class BaseContextualMenu<T> : MonoBehaviour where T : IContextualMenuObject {
        public Action<ContextualOption<T>> onInvalidExecution;

        [SerializeField] private ContextualMenuBuilder builder;

        protected T currentObject;

        public List<ContextualOption<T>> options { get; } = new();

        public void AddButton(ContextualOption<T> option) {
            ContextualMenuButton contextualMenuButton = builder.AddButton(option.buttonText);
            option.button = contextualMenuButton;
            contextualMenuButton.button.onClick.AddListener(delegate { Execute(option); });
            options.Add(option);
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
            currentObject = contextualMenuObject;
            builder.SetText(titleText);
            gameObject.SetActive(true);
            Validate(contextualMenuObject);
        }

        private void Validate(T contextualMenuObject) {
            foreach (var option in options) {
                option.button.SetValid(option.validationFunction(contextualMenuObject), option.invalidOptionBehaviour);
            }
        }

        public void Close() {
            gameObject.SetActive(false);
        }

        private void Execute(ContextualOption<T> option) {
            if (option.closeAfterMenuAfterClicking) {
                Close();
            }

            if (option.validateAgainBeforeExecution && !option.button.SetValid(option.validationFunction(currentObject),
                    option.invalidOptionBehaviour)) {
                onInvalidExecution?.Invoke(option);
                return;
            }

            option.executeFunction(currentObject);
        }
    }
}