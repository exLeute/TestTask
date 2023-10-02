using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace UserInterface
{
    public struct SettingsChanged
    {
        public static bool IsResolutionChanged;
        public static bool IsFullScreenChanged;
    }
    
    public class SettingsMenuBroadcaster
    {
        private readonly VisualElement _root;
        private readonly UiManager _uiManager;
        
        private Button _saveSettingsButton;
        private Button _returnMainMenuButton;
        
        private Toggle _fullscreenToggle;
        
        private DropdownField _resolutionDropdownField;
        
        private VisualElement _brightnessOverlay;
        
        private Slider _brightnessSlider;
        private Slider _musicSlider;
        private Slider _uiSlider;
        
        private Resolution _temporaryResolution;
        private bool _temporaryFullscreenEnabled;
        
        public Action MainMenuReturnActionSubscribe { set => _returnMainMenuButton.clicked += value; }
        public Action SaveSettingsActionSubscribe { set => _saveSettingsButton.clicked += value; }
        
        public SettingsMenuBroadcaster(VisualElement root, UiManager uiManager)
        {
            _root = root;
            _uiManager = uiManager;
            
            QueryForSettingsControls();
            SettingsConfiguration();
            UploadSettings();
        }

        private void QueryForSettingsControls()
        {
            _brightnessOverlay = 
                _root.Q<VisualElement>(AccessibleUIElements.BeginningScreen.BrightnessOverlay);           

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

        private void SettingsConfiguration()
        {
            ResolutionSettings.Resolutions = Screen.resolutions;

            ResolutionSettings.VerifiedResolutions = ResolutionSettings.Resolutions.Where(x => 
                Mathf.Approximately((float)x.width / x.height, 4.0f / 3.0f) ||
                Mathf.Approximately((float)x.width / x.height, 16.0f / 9.0f) ||
                Mathf.Approximately((float)x.width / x.height, 16.0f / 10.0f)).ToList();

            ResolutionSettings.VerifiedResolutions =
                ResolutionSettings.OrderByResolutionValue(ResolutionSettings.VerifiedResolutions);
        }

        private void SettingsControlManagement()
        {
            SaveSettingsActionSubscribe = HandleSaveSettingsEvent;
            MainMenuReturnActionSubscribe = ResetUnsavedSettings;

            _fullscreenToggle.RegisterValueChangedCallback(HandleFullscreenToggleEvent);
            
            _resolutionDropdownField.RegisterValueChangedCallback(HandleResolutionChangeEvent);
            
            _musicSlider.RegisterValueChangedCallback(HandleMusicVolumeChangeEvent);
            _uiSlider.RegisterValueChangedCallback(HandleUiVolumeChangeEvent);
            _brightnessSlider.RegisterValueChangedCallback(HandleBrightnessChangeEvent);
        }

        private void UploadSettings()
        {
            ToggleAllowSaveSettingsButton(false);
            _musicSlider.value = _uiManager.soundOrganizer.musicAudioSource.volume; 
            _uiSlider.value = _uiManager.soundOrganizer.uiAudioSource.volume;
            _brightnessSlider.value = 0.5f - _brightnessOverlay.style.opacity.value;

            _fullscreenToggle.SetValueWithoutNotify(Screen.fullScreen);
            _resolutionDropdownField.choices = ResolutionSettings.VerifiedResolutions
                .Select(ResolutionSettings.ResolutionToStringFormat).ToList();
            
            if (ResolutionSettings.DoVerifiedResolutionsContains(ResolutionSettings.RenderResolution))
            {
                _resolutionDropdownField.SetValueWithoutNotify(
                    ResolutionSettings.ResolutionToStringFormat(ResolutionSettings.RenderResolution));
            }
        }
        
        private void HandleSaveSettingsEvent()
        {
            if (SettingsChanged.IsFullScreenChanged)
            {
                Screen.SetResolution(ResolutionSettings.RenderResolution.x, 
                    ResolutionSettings.RenderResolution.y, _temporaryFullscreenEnabled);
                
                SettingsChanged.IsFullScreenChanged = false;
            }

            if (SettingsChanged.IsResolutionChanged)
            {
                Screen.SetResolution(_temporaryResolution.width, 
                    _temporaryResolution.height, Screen.fullScreen);
                    
                SettingsChanged.IsResolutionChanged = false;
            }
            
            ToggleAllowSaveSettingsButton(false);
        }

        private void HandleFullscreenToggleEvent(ChangeEvent<bool> evt)
        {
            SettingsChanged.IsFullScreenChanged = true;
            ToggleAllowSaveSettingsButton();
        }
        
        private void HandleResolutionChangeEvent(ChangeEvent<string> evt)
        {
            SettingsChanged.IsResolutionChanged = true;
            _temporaryResolution = ResolutionSettings.PullResolutionOutOfStringList(evt.newValue,
                _resolutionDropdownField.choices);
                
            ToggleAllowSaveSettingsButton();
        }

        private void ToggleAllowSaveSettingsButton(bool toggle = true) =>
            _saveSettingsButton.visible = toggle;

        private void ResetUnsavedSettings()
        {
            if (SettingsChanged.IsFullScreenChanged)
            {
                _fullscreenToggle.SetValueWithoutNotify(Screen.fullScreen);
                SettingsChanged.IsFullScreenChanged = false;
            }

            if (SettingsChanged.IsResolutionChanged)
            {
                _resolutionDropdownField.SetValueWithoutNotify(ResolutionSettings.
                ResolutionToStringFormat(ResolutionSettings.RenderResolution));
                SettingsChanged.IsResolutionChanged = false;
            }
            
            ToggleAllowSaveSettingsButton(false);
        }
        
        private void HandleMusicVolumeChangeEvent(ChangeEvent<float> evt)
        {
            _uiManager.soundOrganizer.musicAudioSource.volume = evt.newValue;
        }
        
        private void HandleUiVolumeChangeEvent(ChangeEvent<float> evt)
        {
            _uiManager.soundOrganizer.uiAudioSource.volume = evt.newValue;
        }
        
        private void HandleBrightnessChangeEvent(ChangeEvent<float> evt)
        { // only ui brightness 'workaround', because no there's no gameplay
            _brightnessOverlay.style.opacity = 1.0f - _brightnessSlider.value;
        }
    }
}
