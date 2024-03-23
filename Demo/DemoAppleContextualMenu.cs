﻿using System.Collections.Generic;
using UnityContextualMenu.Scripts;
using UnityEngine;

namespace UnityContextualMenu.Demo {
    public class DemoAppleContextualMenu : ContextualMenu<DemoApple> {
        private void Start() {
            var optionsData = new List<ContextualOption<DemoApple>>() {
                new ("Restore", ExecuteRestore, ValidateRestore),
                new ("Eat", DemoApple.ExecuteEat, DemoApple.ValidateEatable),
                new ("Jump", DemoApple.ExecuteJump, DemoApple.ValidateJump),
                new ("Randomize Color", 
                    apple => apple.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f),Random.Range(0f, 1f)),
                    apple => true
                    )
            };
            foreach (var option in optionsData) {
                // option.closeAfterMenuAfterClicking = false;
                AddOption(option);
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