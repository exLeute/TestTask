using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UserInterface
{
    public class SettingsMenuBroadcaster
    {
        private readonly VisualElement _root;
        private readonly UiManager _uiManager;
        
        private Button _saveSettingsButton;
        private Button _returnMainMenuButton;
        
        private Toggle _fullscreenToggle;
        
        private DropdownField _resolutionDropdownField;
        
        private Slider _brightnessSlider;
        private Slider _musicSlider;
        private Slider _uiSlider;
        
        public Action MainMenuReturnActionSubscribe { set => _returnMainMenuButton.clicked += value; }
        public Action SaveSettingsActionSubscribe { set => _saveSettingsButton.clicked += value; }
        
        public SettingsMenuBroadcaster(VisualElement root, UiManager uiManager)
        {
            _root = root;
            _uiManager = uiManager;
            
            QueryForSettingsControls();
        }

        private void QueryForSettingsControls()
        {
            _saveSettingsButton = 
                _root.Q<Button>(AccessibleUIElements.SettingsMenu.Buttons.SaveSettings);
            _returnMainMenuButton = 
                _root.Q<Button>(AccessibleUIElements.SettingsMenu.Buttons.ReturnMainMenu);
            
            _resolutionDropdownField = 
                _root.Q<DropdownField>(AccessibleUIElements.SettingsMenu.DropDownFields.Resolution);       
            
            _brightnessSlider = 
                _root.Q<Slider>(AccessibleUIElements.SettingsMenu.Sliders.Brightness);     
            _musicSlider = 
                _root.Q<Slider>(AccessibleUIElements.SettingsMenu.Sliders.Music);            
            _uiSlider = 
                _root.Q<Slider>(AccessibleUIElements.SettingsMenu.Sliders.UserInterface);      
            
            _fullscreenToggle = 
                _root.Q<Toggle>(AccessibleUIElements.SettingsMenu.Toggles.Fullscreen);
            
            SettingsControlManagement();
        }

        private void SettingsControlManagement()
        {
            SaveSettingsActionSubscribe = HandleSaveSettings;

            _fullscreenToggle.RegisterValueChangedCallback(HandleFullscreenToggle);
            
            _resolutionDropdownField.RegisterValueChangedCallback(HandleResolutionChange);
            
            _musicSlider.RegisterValueChangedCallback(HandleMusicVolumeChange);
            _uiSlider.RegisterValueChangedCallback(HandleUiVolumeChange);
            _brightnessSlider.RegisterValueChangedCallback(HandleBrightnessChange);
        }
        
        private void HandleSaveSettings()
        {
            
        }

        private void HandleFullscreenToggle(ChangeEvent<bool> evt)
        {
            
        }
        
        private void HandleResolutionChange(ChangeEvent<string> evt)
        {
            
        }        
        
        private void HandleMusicVolumeChange(ChangeEvent<float> evt)
        {
            _uiManager.soundOrganizer.musicAudioSource.volume = evt.newValue;
        }
        
        private void HandleUiVolumeChange(ChangeEvent<float> evt)
        {
            _uiManager.soundOrganizer.uiAudioSource.volume = evt.newValue;
        }
        
        private void HandleBrightnessChange(ChangeEvent<float> evt)
        {
            //RenderSettings.ambientLight = new Color(evt.newValue, evt.newValue, evt.newValue, 1);
        }
    }
}
