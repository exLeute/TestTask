namespace UserInterface
{
    public struct AccessibleUIElements
    {
        public struct BuiltIn
        {
            public struct DropDownField
            {
                public const string DropdownItem = "unity-base-dropdown__item";
            }
        }
        
        public struct MainMenu
        {
            public struct Buttons
            {
                public const string Play = "PlayButton";
                public const string Settings = "SettingsButton";
                public const string Quit = "QuitButton";
            }
        }        
        
        public struct SettingsMenu
        {
            public struct Buttons
            {
                public const string SaveSettings = "SaveSettingsButton";
                public const string ReturnMainMenu = "ReturnMainMenuButton";
            }                 
            
            public struct DropDownFields
            {
                public const string Resolution = "ResolutionDropdownField";
            }         
            
            public struct Toggles
            {
                public const string Fullscreen = "FullscreenToggle";
            }            
            
            public struct Sliders
            {
                public const string Brightness = "BrightnessControlSlider";
                public const string Music = "MusicVolumeSlider";
                public const string UserInterface = "UiVolumeSlider";
            }
        }
        
        public struct BeginningScreen
        {
            public const string BrightnessOverlay = "BrightnessOverlay";
            public const string MainMenuScreen = "MainMenuUI";
            public const string SettingsMenuScreen = "SettingsMenuUI";
        }
    }
}