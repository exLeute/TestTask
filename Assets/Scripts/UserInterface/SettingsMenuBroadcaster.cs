using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UserInterface
{
    public class SettingsMenuBroadcaster
    {
        private VisualElement _root;
        private readonly Button _saveSettingsButton;
        private readonly Button _returnMainMenuButton;
        
        public Action MainMenuReturnActionSubscribe { set => _returnMainMenuButton.clicked += value; }
        public Action SaveSettingsActionSubscribe { set => _saveSettingsButton.clicked += value; }
        
        public SettingsMenuBroadcaster(VisualElement root)
        {
            _root = root;
            _saveSettingsButton = root.Q<Button>(AccessibleUIElements.SettingsMenu.Buttons.SaveSettings);
            _returnMainMenuButton = root.Q<Button>(AccessibleUIElements.SettingsMenu.Buttons.ReturnMainMenu);
            SettingsControlManagement();
            
            SaveSettingsActionSubscribe = () =>
            {
                Debug.Log("settings almost saved");
            };
        }

        private void QueryForSettingsControls()
        {
            
        }

        private void SettingsControlManagement()
        {
            
        }
    }
}
