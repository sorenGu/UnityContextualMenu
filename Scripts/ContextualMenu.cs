using System;
using System.Collections.Generic;
using UnityEngine;

namespace sorenGu.UnityContextualMenu.Scripts {
    public interface IContextualMenuObject { }

    public class ContextualOption {
        public string buttonText { get; }
        public Action executeFunction { get; }
        public DisplayOption displayOption { get; }
        public bool closeAfterMenuAfterClicking { get; }

        public ContextualOption(
                string buttonText,
                Action executeFunction,
                DisplayOption displayOption = DisplayOption.Show,
                bool closeAfterMenuAfterClicking = true
            ) {
            this.buttonText = buttonText;
            this.executeFunction = executeFunction;
            this.displayOption = displayOption;
            this.closeAfterMenuAfterClicking = closeAfterMenuAfterClicking;
        }
    }

    [RequireComponent(typeof(ContextualMenuBuilder))]
    public class ContextualMenu : MonoBehaviour {
        [SerializeField] private ContextualMenuBuilder builder;
        public static ContextualMenu Instance { get; protected set; }

        protected virtual void Awake() {
            if (Instance && Instance != this) {
                Destroy(gameObject);
            } else {
                Instance = this;
            }
        }

        public virtual void Open(string titleText, List<ContextualOption> options, Vector3 position,
            Transform attachTransform = null) {
            if (attachTransform) {
                transform.SetParent(attachTransform, false);
                transform.localPosition = position; 
            } else {
                transform.position = position;
            }
            
            builder.SetText(titleText);
            gameObject.SetActive(true);

            RefreshOptions(options);
        }

        public void RefreshOptions(List<ContextualOption> options) {
            builder.RemoveButtons();
            foreach (ContextualOption option in options) {
                builder.AddButton(option);
            }
        }
    }
}