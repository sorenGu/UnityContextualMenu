using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace sorenGu.UnityContextualMenu.Scripts {
    public class HideContextMenu : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler {
        [Header("Delays in Seconds")]
        [SerializeField] private float delayAfterMouseLeft = 0.5f;
        
        [SerializeField] private bool hideWhenUserDoesntEnter = true;
        [SerializeField] private float delayWhenUserDoesntEnter = 2f;
        private Coroutine currentCoroutine;

        private void OnEnable() {
            StopCurrentCoroutine();
            if (hideWhenUserDoesntEnter) {
                currentCoroutine = StartCoroutine(Hide(delayWhenUserDoesntEnter));
            }
        }


        public void OnPointerExit(PointerEventData eventData) {
            StopCurrentCoroutine();
            currentCoroutine = StartCoroutine(Hide(delayAfterMouseLeft));
        }

        public void OnPointerEnter(PointerEventData eventData) {
            StopCurrentCoroutine();
        }

        private void StopCurrentCoroutine() {
            if (currentCoroutine == null) return;

            StopCoroutine(currentCoroutine);
        }

        private IEnumerator Hide(float delayInSeconds) {
            yield return new WaitForSeconds(delayInSeconds);
            yield return null;
            gameObject.SetActive(false);
        }
    }
}