using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UserInterface
{
    public class MainMenuBroadcaster
    {
        private readonly VisualElement _root;
        
        private Button _playButton;
        private Button _settingsButton;
        private Button _quitButton;
        
        public Action PlayActionSubscribe { set => _playButton.clicked += value; }              
        public Action OpenSettingsActionSubscribe { set => _settingsButton.clicked += value; }        
        public Action QuitGameActionSubscribe { set => _quitButton.clicked += value; }
        
        
        public MainMenuBroadcaster(VisualElement root)
        {
            _root = root;
            QueryForMainMenuControls();
            SettingsControlManagement();
        }

        private void QueryForMainMenuControls()
        {
            _playButton = _root.Q<Button>(AccessibleUIElements.MainMenu.Buttons.Play);
            _settingsButton = _root.Q<Button>(AccessibleUIElements.MainMenu.Buttons.Settings);
            _quitButton = _root.Q<Button>(AccessibleUIElements.MainMenu.Buttons.Quit);
        }

        private void SettingsControlManagement()
        {
            QuitGameActionSubscribe = HandleQuitEvent;
        }
        
        private static void HandleQuitEvent()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            
            #endif
            Application.Quit();
        }
    }
}
