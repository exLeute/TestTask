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
                public const string SaveSettings = "";
                public const string ReturnMainMenu = "ReturnMainMenuButton";
            }
        }
    }
}