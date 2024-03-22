using System.Collections;
using Plugins.ContextualMenu.Scripts.ContextualMenu;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Plugins.ContextualMenu.Demo {
    public enum OnClickAction {
        Eat,
        Jump,
        OpenContextualMenu,
    }

    public class DemoApple : MonoBehaviour, IPointerClickHandler, IContextualMenuObject {
        public bool eaten { get; private set; }
        private bool isJumping;

        [SerializeField] private OnClickAction onClickAction;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private Sprite eatenSprite;

        [SerializeField] private DemoAppleContextualMenu contextualMenu;

        public void OnPointerClick(PointerEventData eventData) {
            switch (onClickAction) {
                case OnClickAction.Eat:
                    if (ValidateEatable(this)) {
                        ExecuteEat(this);
                    }
                    break;
                case OnClickAction.Jump:
                    if (ValidateJump(this)) {
                        ExecuteJump(this);
                    }
                    break;
                case OnClickAction.OpenContextualMenu:
                    contextualMenu.Open(transform.name, this, transform);
                    break;
            }
        }

        public static void ExecuteEat(DemoApple apple) {
            Debug.Log("Yumi!");
            apple.spriteRenderer.sprite = apple.eatenSprite;
            apple.eaten = true;
        }

        public static bool ValidateEatable(DemoApple apple) {
            return !apple.eaten;
        }

        public static void ExecuteJump(DemoApple apple) {
            apple.StartJump();
        }

        public static bool ValidateJump(DemoApple apple) {
            return !apple.isJumping;
        }

        public void StartJump() {
            Debug.Log("Hop!");
            StartCoroutine(Jump());
        }

        private IEnumerator Jump() {
            isJumping = true;
            Vector3 originalPosition = transform.position;
            for (float i = 0; i < 2; i += 0.1f) {
                yield return null;
                transform.position = originalPosition + new Vector3(0, i, 0);
            }
            for (float i = 0; i < 2; i += 0.1f) {
                yield return null;
                transform.position = originalPosition + new Vector3(0, 2-i, 0);
            }

            transform.position = originalPosition;
            isJumping = false;
        }

        public void Restore() {
            Debug.Log("Restore!");
            eaten = false;
            spriteRenderer.sprite = defaultSprite;
        }
    }
}