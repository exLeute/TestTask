using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace UserInterface
{
    public class ResolutionSetting
    {
        public Resolution MainResolution;
        public static Resolution[] Resolutions;
        public static List<Resolution> VerifiedResolutions;
        public static List<Vector2Int> VerifiedResolutionsDimensions = new();
        public bool IsSaved;
        public bool isFullscreen;

        public ResolutionSetting(bool saved) { IsSaved = saved; }

        public static List<Resolution> OrderByResolutionValue(IEnumerable<Resolution> resolutionsList)
        {
            return resolutionsList.OrderBy(resolution => resolution.width).ToList();
        }

        public static void SetResolutionAndDropdownField(ResolutionSetting resolutionSetting, DropdownField dropdownField, 
            Resolution resolution, bool fullScreen)
        {
            resolutionSetting.MainResolution.width = resolution.width; 
            resolutionSetting.MainResolution.height = resolution.height;
            resolutionSetting.isFullscreen = fullScreen;
            Screen.SetResolution(resolution.width, resolution.height, fullScreen);
            dropdownField.SetValueWithoutNotify(ResolutionToStringFormat(resolution));
        }
        
        public static void SetResolutionAndDropdownField(ResolutionSetting resolutionSetting, DropdownField dropdownField, 
            Vector2Int resolution, bool fullScreen)
        {
            resolutionSetting.MainResolution.width = resolution.x;
            resolutionSetting.MainResolution.height = resolution.y;
            resolutionSetting.isFullscreen = fullScreen;
            Screen.SetResolution(resolution.x, resolution.y, fullScreen);
            dropdownField.SetValueWithoutNotify(ResolutionToStringFormat(resolution));
        }
        
        public static string ResolutionToStringFormat(Vector2Int resolution) =>
            $"{resolution.x}x{resolution.y}";     
        
        public static string ResolutionToStringFormat(Resolution resolution) =>
            $"{resolution.width}x{resolution.height}";

        public static Resolution PullResolutionOutOfStringList(string resolution, List<string> resolutionsList)
        {
            return resolutionsList.Contains(resolution) ? 
                VerifiedResolutions[resolutionsList.IndexOf(resolution)] 
                : new Resolution {width = -1, height = -1};
        }

        public static bool VerifiedResolutionsContains(Vector2Int resolution) => 
            VerifiedResolutions.Any(res =>
                res.width == resolution.x && res.height == resolution.y);

        public void UnSave()
        {
            IsSaved = false;
            isFullscreen = false;
            MainResolution = new Resolution {width = -1, height = -1};
        }
        
        public void Save(Resolution resolution, bool fullScreen)
        {
            IsSaved = true;
            MainResolution = resolution;
            isFullscreen = fullScreen;
        }        
        
        public void Save(int width, int height, bool fullScreen)
        {
            IsSaved = true;
            MainResolution = Screen.currentResolution;
            MainResolution.width = width;
            MainResolution.height = height;
            isFullscreen = fullScreen;
        }
    }
}