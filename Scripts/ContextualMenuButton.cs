using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace sorenGu.UnityContextualMenu.Scripts {
    public enum DisplayOption {
        Show,
        Hide,
        Disable
    }
    
    public class ContextualMenuButton : MonoBehaviour {
        public Button button;
        [SerializeField] private TextMeshProUGUI buttonText;

        public void Init(ContextualOption option) {
            gameObject.SetActive(true);
            buttonText.text = option.buttonText;
            button.onClick.AddListener(() => { option.executeFunction(); });

            SetDisplay(option);
        }

        private void SetDisplay(ContextualOption option) {
            switch (option.displayOption) {
                case DisplayOption.Show:
                    button.interactable = true;
                    break;
                case DisplayOption.Hide:
                    gameObject.SetActive(false);
                    break;
                case DisplayOption.Disable:
                    button.interactable = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}