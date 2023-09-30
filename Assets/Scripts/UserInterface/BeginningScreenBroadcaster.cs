using UI_Toolkit.Helpers;
using UnityEngine;
using UnityEngine.UIElements;

namespace UserInterface
{
    [RequireComponent(typeof(UIDocument))]
    public class BeginningScreenBroadcaster : MonoBehaviour
    {
        private VisualElement _mainMenuScreen;
        private VisualElement _settingsScreen;
        
        private void Start()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            _mainMenuScreen = root.Q("MainMenuUI");
            _settingsScreen = root.Q("SettingsMenuUI");

            MainMenuBroadcaster mainMenuBroadcaster = new (root);
            mainMenuBroadcaster.OpenSettings = () =>
            {
                _mainMenuScreen.Display(false);
                _settingsScreen.Display(true);
            };
            
            SettingsMenuBroadcaster settingsMenuBroadcaster = new (root);
            settingsMenuBroadcaster.MainMenuReturn = () =>
            {
                _mainMenuScreen.Display(true);
                _settingsScreen.Display(false);
            };
        }
    }
}
