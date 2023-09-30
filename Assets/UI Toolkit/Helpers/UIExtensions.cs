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
    }
}