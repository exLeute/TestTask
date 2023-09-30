using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UserInterface
{
    public class MainMenuBroadcaster
    {
        private readonly Button _startButton;
        private readonly Button _settingsButton;
        private readonly Button _quitButton;
        
        public Action OpenSettings
        {
            set => _settingsButton.clicked += value;
        }
        
        public MainMenuBroadcaster(VisualElement root)
        {
            _startButton = root.Q<Button>("PlayButton");
            _settingsButton = root.Q<Button>("SettingsButton");
            _quitButton = root.Q<Button>("QuitButton");
            
            _startButton.clicked += () => Debug.Log("start clicked");
            _settingsButton.clicked += () => Debug.Log("settings clicked");
            _quitButton.clicked += () => Debug.Log("quit clicked");
        }
    }
}
