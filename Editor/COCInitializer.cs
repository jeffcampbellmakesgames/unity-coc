/*
MIT License

Copyright (c) 2018 Jeff Campbell

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using JCMG.COC.Editor.Utility;
using UnityEditor;

// ReSharper disable InconsistentNaming

namespace JCMG.COC.Editor
{
	[InitializeOnLoad]
    public static class COCInitializer
    {
	    private const string GAME_ASSET_ROOT = ASSET_ROOT + "/" + GAME_ASSET_FOLDERNAME;
	    private const string ASSET_ROOT = "Assets";
	    private const string GAME_ASSET_FOLDERNAME = "Game";

		static COCInitializer()
        {
            var allCOCDomains = (COCDomain[])Enum.GetValues(typeof(COCDomain));

            if (!AssetDatabase.IsValidFolder(GAME_ASSET_ROOT))
                AssetDatabase.CreateFolder(ASSET_ROOT, GAME_ASSET_FOLDERNAME);

            foreach (var cocDomain in allCOCDomains)
            {
                if(AssetDatabase.IsValidFolder(GetConventionFolderRoot(cocDomain))) continue;

                AssetDatabase.CreateFolder("Assets/Game", Enum.GetName(typeof(COCDomain), cocDomain));
            }

	        PlayerSettingsUtility.AddScriptingSymbolIfNotDefined("JCMG_COC");
		}

        public static void AddConvention(COCDomain domain, string area)
        {
            if (!AssetDatabase.IsValidFolder(GetConventionFolder(domain, area)))
                AssetDatabase.CreateFolder(GetConventionFolderRoot(domain), area);
        }

        public static void AddConvention(COCDomain domain, string area, string subfolder)
        {
            if (!AssetDatabase.IsValidFolder(GetConventionFolder(domain, area, subfolder)))
                AssetDatabase.CreateFolder(GetConventionFolder(domain, area), subfolder);
        }

	    public static string GetConventionFolderRoot(COCDomain domain)
	    {
		    return string.Format("Assets/Game/{0}", domain);
	    }

	    public static string GetConventionFolder(COCDomain domain, string area)
	    {
		    return string.Format("Assets/Game/{0}/{1}", domain, area);
	    }

	    public static string GetConventionFolder(COCDomain domain, string area, string subfolder)
	    {
		    return string.Format("Assets/Game/{0}/{1}/{2}", domain, area, subfolder);
	    }
	}
}