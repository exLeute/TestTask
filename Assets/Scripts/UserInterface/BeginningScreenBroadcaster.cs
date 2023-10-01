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
            _mainMenuScreen = _root.Q("MainMenuUI");
            _settingsScreen = _root.Q("SettingsMenuUI");
            
            _mainMenuBroadcaster = new MainMenuBroadcaster(_root)
            {
                OpenSettingsActionSubscribe = () => SwapScreenWith(_mainMenuScreen, _settingsScreen)
            };

            _settingsMenuBroadcaster = new SettingsMenuBroadcaster(_root, uiManager)
            {
                MainMenuReturnActionSubscribe = () => SwapScreenWith(_settingsScreen, _mainMenuScreen)
            };
            
            uiManager.Init(_root);
        }

        private static void SwapScreenWith(VisualElement swapFrom, VisualElement swapTo)
        {
            swapFrom.Display(false);
            swapTo.Display(true);
        }
    }
}
