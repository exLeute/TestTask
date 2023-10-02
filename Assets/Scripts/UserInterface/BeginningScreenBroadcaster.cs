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

            _debugLabelResolution1 = _root.Q<Label>("Resolution");
            _debugLabelResolution2 = _root.Q<Label>("Resolution2");
            _debugLabelFullscreen = _root.Q<Label>("Fullscreen");
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

        private Label _debugLabelResolution1;
        private Label _debugLabelResolution2;
        private Label _debugLabelFullscreen;
        
        private void Update()
        {
            _debugLabelResolution1.text = $"{Screen.width}x{Screen.height}";
            _debugLabelResolution2.text = $"{Screen.currentResolution.width}x{Screen.currentResolution.height}";
            _debugLabelFullscreen.text = $"{Screen.fullScreen}";
        }
    }
}
