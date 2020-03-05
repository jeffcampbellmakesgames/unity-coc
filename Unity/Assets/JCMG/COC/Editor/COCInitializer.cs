/*
MIT License

Copyright (c) 2020 Jeff Campbell

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
using System.IO;
using UnityEditor;
using UnityEngine;

// ReSharper disable InconsistentNaming

namespace JCMG.COC.Editor
{
	public static class COCInitializer
	{
		// ScriptingSymbol
		private const string SCRIPTING_SYMBOL = "JCMG_COC";

		/// <summary>
		///     EnsureSetup will rerun the AddConvention initialization process to create the necessary folder
		///     desired by the project, tools, and frameworks reliant on it.
		/// </summary>
		[InitializeOnLoadMethod]
		public static void EnsureSetup()
		{
			if (EditorApplication.isPlayingOrWillChangePlaymode)
			{
				return;
			}

			// Initialize any root domain folders and paths
			// TODO Find and execute all eligible graphs

			// Set the scripting symbol for COC
			PlayerSettingsTools.AddScriptingSymbolIfNotDefined(SCRIPTING_SYMBOL);

			AssetDatabase.Refresh();
		}
	}
}
