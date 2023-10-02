using System;
using UI_Toolkit.Helpers;
using UnityEngine;
using UnityEngine.UIElements;

namespace UserInterface
{
    [RequireComponent(typeof(UIDocument))]

    public class BeginningScreenBroadcaster : MonoBehaviour
    {
        [SerializeField] private UiManager uiManager;
        private VisualElement _root;
        
        private VisualElement _mainMenuScreen;
        private MainMenuBroadcaster _mainMenuBroadcaster;
        
        private VisualElement _settingsScreen;
        private SettingsMenuBroadcaster _settingsMenuBroadcaster;

        public void Start()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            
            QueryForScreens();
            SetupScreens();
            
            uiManager.Init(_root);
        }

        private void QueryForScreens()
        {
            _mainMenuScreen = _root.Q(AccessibleUIElements.BeginningScreen.MainMenuScreen);
            _settingsScreen = _root.Q(AccessibleUIElements.BeginningScreen.SettingsMenuScreen);
        }

        private void SetupScreens()
        {
            _mainMenuBroadcaster = new MainMenuBroadcaster(_root)
            {
                OpenSettingsActionSubscribe = () => SwapScreenWith(_mainMenuScreen, _settingsScreen)
            };

            _settingsMenuBroadcaster = new SettingsMenuBroadcaster(_root, uiManager)
            {
                MainMenuReturnActionSubscribe = () => SwapScreenWith(_settingsScreen, _mainMenuScreen)
            };
        }

        private static void SwapScreenWith(VisualElement swapFrom, VisualElement swapTo)
        {
            swapFrom.Display(false);
            swapTo.Display(true);
        }
    }
}
