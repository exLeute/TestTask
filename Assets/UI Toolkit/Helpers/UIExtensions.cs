using UnityEngine;
using UnityEngine.UIElements;

namespace UI_Toolkit.Helpers
{
    public static class UIExtensions
    {
        public static void Display(this VisualElement element, bool toggleDisplay)
        {
            if(element == null) return;
            element.style.display = toggleDisplay ? DisplayStyle.Flex : DisplayStyle.None;
        }        
        
        public static bool IsDisplayed(this VisualElement element)
        {
            if (element == null) return new bool();
            return element.style.display == DisplayStyle.Flex;
        }
    }
}