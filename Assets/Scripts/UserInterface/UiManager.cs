using UnityEngine;
using UnityEngine.UIElements;

namespace UserInterface
{
    public class UiManager : MonoBehaviour
    {
        public SoundOrganizer soundOrganizer;

        private VisualElement _root;

        public void Init(VisualElement root)
        {
            _root = root;
            BindSoundsToUi();
        }

        private void BindSoundsToUi()
        {
            UQueryBuilder<Toggle> toggleElements = _root.Query<Toggle>();
            UQueryBuilder<Button> buttonElements = _root.Query<Button>();
            UQueryBuilder<DropdownField> dropDownElements = _root.Query<DropdownField>();
            UQueryBuilder<VisualElement> dropDownItemElements =
                _root.parent.Query(className: AccessibleUIElements.BuiltIn.DropDownField.DropdownItem);
            
            toggleElements.ForEach(toggle =>
                soundOrganizer.AssignSoundToUI<MouseEnterEvent>(soundOrganizer.controls.hoverClick, toggle));
            
            buttonElements.ForEach(button =>
                soundOrganizer.AssignSoundToUI<MouseEnterEvent>(soundOrganizer.controls.hoverClick, button));

            dropDownElements.ForEach(dropdown =>
            {
                soundOrganizer.AssignSoundToUI<MouseEnterEvent>(soundOrganizer.controls.hoverClick, dropdown);
                dropdown.RegisterCallback<MouseDownEvent>(_ =>
                {
                    dropDownItemElements.ForEach(dropdownItem =>
                        soundOrganizer.AssignSoundToUI<MouseEnterEvent>(soundOrganizer.controls.hoverClick, dropdownItem));
                });
            });
        }
    }
}