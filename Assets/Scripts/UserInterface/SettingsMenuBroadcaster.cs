using System;
using System.Linq;
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
        
        private VisualElement _brightnessOverlay;
        
        private Slider _brightnessSlider;
        private Slider _musicSlider;
        private Slider _uiSlider;
        
        private ResolutionSetting _savedResolution;
        private ResolutionSetting _unsavedResolution;
        
        public Action MainMenuReturnActionSubscribe { set => _returnMainMenuButton.clicked += value; }
        public Action SaveSettingsActionSubscribe { set => _saveSettingsButton.clicked += value; }
        
        public SettingsMenuBroadcaster(VisualElement root, UiManager uiManager)
        {
            _root = root;
            _uiManager = uiManager;
            
            QueryForSettingsControls();
            SettingsConfiguration();
            HandleUploadSettings();
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
            ResolutionSetting.Resolutions = Screen.resolutions;

            ResolutionSetting.VerifiedResolutions = ResolutionSetting.Resolutions.Where(x => 
                Mathf.Approximately((float)x.width / x.height, 4.0f / 3.0f) ||
                Mathf.Approximately((float)x.width / x.height, 16.0f / 9.0f) ||
                Mathf.Approximately((float)x.width / x.height, 16.0f / 10.0f)).ToList();

            ResolutionSetting.VerifiedResolutions =
                ResolutionSetting.OrderByResolutionValue(ResolutionSetting.VerifiedResolutions);
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

        private void HandleUploadSettings()
        {
            _unsavedResolution = new ResolutionSetting(false);
            _savedResolution = new ResolutionSetting(true);
            
            TriggerSettingsSaved();
            _uiManager.soundOrganizer.musicAudioSource.volume = _musicSlider.value;
            _uiManager.soundOrganizer.uiAudioSource.volume = _uiSlider.value;
            _brightnessOverlay.style.opacity = 1.0f - _brightnessSlider.value;
            _brightnessOverlay.pickingMode = PickingMode.Ignore;
            
            _resolutionDropdownField.choices.Clear();
            _resolutionDropdownField.choices.AddRange(ResolutionSetting.VerifiedResolutions
                .Select(ResolutionSetting.ResolutionToStringFormat).ToList());
            
            Vector2Int currentScreenRes = new (Display.main.systemWidth, Display.main.systemHeight);
            
            if (ResolutionSetting.VerifiedResolutionsContains(currentScreenRes))
            {
                _resolutionDropdownField.SetValueWithoutNotify(ResolutionSetting.ResolutionToStringFormat(currentScreenRes));
                _fullscreenToggle.SetValueWithoutNotify(Screen.fullScreen);
                _savedResolution.Save(currentScreenRes.x, currentScreenRes.y, Screen.fullScreen);
            }
        }
        
        private void HandleSaveSettingsEvent()
        {
            if (_unsavedResolution.IsSaved)
            {
                ResolutionSetting.SetResolutionAndDropdownField(_savedResolution, _resolutionDropdownField,
                    _unsavedResolution.MainResolution, _unsavedResolution.IsFullscreen);
                _unsavedResolution.UnSave();
                _saveSettingsButton.visible = false;
            }
        }

        private void HandleFullscreenToggleEvent(ChangeEvent<bool> evt)
        {
            TriggerSettingsUnsaved();
            _unsavedResolution.IsFullscreen = evt.newValue;
        }
        
        private void HandleResolutionChangeEvent(ChangeEvent<string> evt)
        {
            if (!_unsavedResolution.IsSaved)
            {
                _unsavedResolution.Save(ResolutionSetting.PullResolutionOutOfStringList(evt.newValue,
                    _resolutionDropdownField.choices), _unsavedResolution.IsFullscreen);
            }
            
            TriggerSettingsUnsaved();
        }

        private void TriggerSettingsUnsaved()
        {
            _saveSettingsButton.visible = true;
        }
        
        private void TriggerSettingsSaved()
        {
            _saveSettingsButton.visible = false;
        }

        private void ResetUnsavedSettings()
        {
            _unsavedResolution.UnSave();
            _resolutionDropdownField.SetValueWithoutNotify(ResolutionSetting.
                ResolutionToStringFormat(_savedResolution.MainResolution));
            _fullscreenToggle.SetValueWithoutNotify(_savedResolution.IsFullscreen);
            TriggerSettingsSaved();
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
