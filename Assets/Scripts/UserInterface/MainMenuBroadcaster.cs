using System;
using UnityEngine.UIElements;
namespace UserInterface
{
    public class MainMenuBroadcaster
    {
        private Button _playButton;
        private Button _settingsButton;
        private Button _quitButton;
        
        public Action PlayActionSubscribe { set => _playButton.clicked += value; }              
        public Action OpenSettingsActionSubscribe { set => _settingsButton.clicked += value; }        
        public Action QuitGameActionSubscribe { set => _quitButton.clicked += value; }
        
        
        public MainMenuBroadcaster(VisualElement root)
        {
            QueryForMainMenuControls(root);
        }

        private void QueryForMainMenuControls(VisualElement root)
        {
            _playButton = root.Q<Button>(AccessibleUIElements.MainMenu.Buttons.Play);
            _settingsButton = root.Q<Button>(AccessibleUIElements.MainMenu.Buttons.Settings);
            _quitButton = root.Q<Button>(AccessibleUIElements.MainMenu.Buttons.Quit);
        }
    }
}
