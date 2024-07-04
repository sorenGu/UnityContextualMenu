using System.Collections.Generic;
using sorenGu.UnityContextualMenu.Scripts;
using UnityEngine;

namespace sorenGu.UnityContextualMenu.Demo {
    public class DemoAppleContextualMenuAnchor : MonoBehaviour {
        private DemoApple apple;
        public void Open(DemoApple apple) {
            this.apple = apple;
            
            
            var optionsData = new List<ContextualOption>() {
                new ("Restore", ExecuteRestore, apple.eaten ? DisplayOption.Show : DisplayOption.Hide),
                new ("Eat", apple.ExecuteEat, apple.eaten ? DisplayOption.Disable : DisplayOption.Show),
                new ("Jump", apple.StartJump, apple.isJumping ? DisplayOption.Hide : DisplayOption.Show),
                new ("Randomize Color", 
                    () => apple.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f),Random.Range(0f, 1f)),
                    closeAfterMenuAfterClicking: false
                    )
            };
            ContextualMenu.Instance.Open("Apple", optionsData, transform.position);
        }
        
        private void ExecuteRestore() {
            apple.Restore();
        }
    }
}