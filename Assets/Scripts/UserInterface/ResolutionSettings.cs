using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UserInterface
{
    public static class ResolutionSettings
    {
        public static Vector2Int RenderResolution =>
            new (Screen.width, Screen.height);
        public static Resolution[] Resolutions;
        public static List<Resolution> VerifiedResolutions;
   

        public static List<Resolution> OrderByResolutionValue(IEnumerable<Resolution> resolutionsList)
        {
            return resolutionsList.OrderBy(resolution => resolution.width).ToList();
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

        public static bool DoVerifiedResolutionsContains(Vector2Int resolution) => 
            VerifiedResolutions.Any(res =>
                res.width == resolution.x && res.height == resolution.y);
    }
}