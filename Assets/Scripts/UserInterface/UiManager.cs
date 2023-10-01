using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace UserInterface
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private UiSoundOrganizer uiSoundOrganizer;
        [SerializeField] private AudioClip audioClip;
        private static AudioSource _audioSource;

        private VisualElement _root;

        public void Init(VisualElement root)
        {
            _root = root;
            ConfigureSounds();
        }

        private void ConfigureSounds()
        {
            uiSoundOrganizer.Init();
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
                uiSoundOrganizer.AssignSoundTo<MouseEnterEvent>(toggle));
            
            buttonElements.ForEach(button =>
                uiSoundOrganizer.AssignSoundTo<MouseEnterEvent>(button));

            dropDownElements.ForEach(dropdown =>
            {
                uiSoundOrganizer.AssignSoundTo<MouseEnterEvent>(dropdown);
                dropdown.RegisterCallback<MouseDownEvent>(_ =>
                {
                    dropDownItemElements.ForEach(dropdownItem =>
                        uiSoundOrganizer.AssignSoundTo<MouseEnterEvent>(dropdownItem));
                });
            });
        }
    }
}