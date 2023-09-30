using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UserInterface
{
    public class SettingsMenuBroadcaster
    {
        private readonly Button _returnMainMenuButton;
        
        public Action MainMenuReturn
        {
            set => _returnMainMenuButton.clicked += value;
        }
        
        public SettingsMenuBroadcaster(VisualElement root)
        {
            _returnMainMenuButton = root.Q<Button>("ReturnMainMenuButton");
        }
    }
}
