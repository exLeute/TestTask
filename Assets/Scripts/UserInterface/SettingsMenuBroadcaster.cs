using System;
using UnityEngine.UIElements;

namespace UserInterface
{
    public class SettingsMenuBroadcaster
    {
        private readonly Button _returnMainMenuButton;
        
        public Action MainMenuReturnActionSubscribe { set => _returnMainMenuButton.clicked += value; }
        
        public SettingsMenuBroadcaster(VisualElement root)
        {
            _returnMainMenuButton = root.Q<Button>(AccessibleUIElements.SettingsMenu.Buttons.ReturnMainMenu);
        }
    }
}
