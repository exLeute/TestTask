using UnityEngine;
using UnityEngine.UIElements;

namespace UserInterface
{
    public class UiManager : MonoBehaviour
    {
        public SoundOrganizer soundOrganizer;

        private VisualElement _root;

        private UQueryBuilder<Toggle> _toggleElements;
        private UQueryBuilder<Button> _buttonElements;
        private UQueryBuilder<DropdownField> _dropDownElements;
        private UQueryBuilder<VisualElement> _dropDownItemElements;
        
        public void Init(VisualElement root)
        {
            _root = root;
            
            QueryForUiElements();
            BindSoundsToUi();
        }

        private void QueryForUiElements()
        {
            _toggleElements = _root.Query<Toggle>();
            _buttonElements = _root.Query<Button>();
            _dropDownElements = _root.Query<DropdownField>();
            _dropDownItemElements =
                _root.parent.Query(className: AccessibleUIElements.BuiltIn.DropDownField.DropdownItem);
        }
        
        private void BindSoundsToUi()
        {
            _toggleElements.ForEach(toggle =>
                soundOrganizer.AssignSoundToUI<MouseEnterEvent>(soundOrganizer.controls.hoverClick, toggle));
            
            _buttonElements.ForEach(button =>
                soundOrganizer.AssignSoundToUI<MouseEnterEvent>(soundOrganizer.controls.hoverClick, button));

            _dropDownElements.ForEach(dropdown =>
            {
                soundOrganizer.AssignSoundToUI<MouseEnterEvent>(soundOrganizer.controls.hoverClick, dropdown);
                dropdown.RegisterCallback<MouseDownEvent>(_ =>
                {
                    _dropDownItemElements.ForEach(dropdownItem =>
                        soundOrganizer.AssignSoundToUI<MouseEnterEvent>(soundOrganizer.controls.hoverClick, dropdownItem));
                });
            });
        }
    }
}