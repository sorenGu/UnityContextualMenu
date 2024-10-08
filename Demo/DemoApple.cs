using System.Collections;
using sorenGu.UnityContextualMenu.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace sorenGu.UnityContextualMenu.Demo {
    public enum OnClickAction {
        Eat,
        Jump,
        OpenContextualMenu,
    }

    public class DemoApple : MonoBehaviour, IPointerClickHandler, IContextualMenuObject {
        public bool eaten { get; private set; }
        public bool isJumping { get; private set; }

        [SerializeField] private OnClickAction onClickAction;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private Sprite eatenSprite;

        [FormerlySerializedAs("contextualMenu")] [SerializeField] private DemoAppleContextualMenuAnchor contextualMenuAnchor;

        public void OnPointerClick(PointerEventData eventData) {
            switch (onClickAction) {
                case OnClickAction.Eat:
                    if (ValidateEatable(this)) {
                        ExecuteEat();
                    }
                    break;
                case OnClickAction.Jump:
                    if (!isJumping) {
                        StartJump();
                    }
                    break;
                case OnClickAction.OpenContextualMenu:
                    contextualMenuAnchor.Open(this);
                    break;
            }
        }

        public void ExecuteEat() {
            Debug.Log("Yumi!");
            spriteRenderer.sprite = eatenSprite;
            eaten = true;
        }

        private static bool ValidateEatable(DemoApple apple) {
            return !apple.eaten;
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