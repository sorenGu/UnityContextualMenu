using System.Collections.Generic;
using Plugins.ContextualMenu.Scripts.ContextualMenu;
using UnityEngine;

namespace Plugins.ContextualMenu.Demo {
    public class DemoAppleContextualMenu : BaseContextualMenu<DemoApple> {
        private void Awake() {
            AddButton(new("Eat", DemoApple.ExecuteEat, DemoApple.ValidateEatable));
            
            var optionsData = new List<ContextualMenuData<DemoApple>>() {
                new ("Restore", ExecuteRestore, ValidateRestore),
                new ("Eat", DemoApple.ExecuteEat, DemoApple.ValidateEatable),
                new ("Jump", DemoApple.ExecuteJump, DemoApple.ValidateJump),
                new ("Randomize Color", 
                    apple => apple.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f),Random.Range(0f, 1f)),
                    apple => true
                    )
            };
            foreach (var option in optionsData) {
                AddButton(option);
            }
        }

        private bool ValidateRestore(DemoApple apple) {
            return apple.eaten;
        }

        private void ExecuteRestore(DemoApple apple) {
            apple.Restore();
        }

    }
}