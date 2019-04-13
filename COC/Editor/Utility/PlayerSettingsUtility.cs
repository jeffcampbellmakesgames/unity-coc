﻿/*
MIT License

Copyright (c) 2019 Jeff Campbell

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
using UnityEditor;

namespace JCMG.COC.Editor.Utility
{
	public static class PlayerSettingsUtility
	{
		public static bool IsScriptingSymbolDefined(string symbol)
		{
			var currentBuildGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
			var scriptingSymbolStr = PlayerSettings.GetScriptingDefineSymbolsForGroup(currentBuildGroup);
			var scriptingSymbols = scriptingSymbolStr.Split(';');

			foreach (var scriptingSymbol in scriptingSymbols)
				if (symbol == scriptingSymbol)
					return true;

			return false;
		}

		public static void AddScriptingSymbolIfNotDefined(string symbol)
		{
			if (IsScriptingSymbolDefined(symbol)) return;

			var currentBuildGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
			var scriptingSymbolStr = PlayerSettings.GetScriptingDefineSymbolsForGroup(currentBuildGroup);

			// Add the new symbol to the list of symbols, always ensuring there is a semi-colon separating all symbols
			if (string.IsNullOrEmpty(scriptingSymbolStr))
				scriptingSymbolStr = string.Format("{0}", symbol);
			else if (scriptingSymbolStr[scriptingSymbolStr.Length - 1] == ';')
				scriptingSymbolStr += string.Format("{0}", symbol);
			else
				scriptingSymbolStr += string.Format(";{0}", symbol);

			PlayerSettings.SetScriptingDefineSymbolsForGroup(currentBuildGroup, scriptingSymbolStr);
		}
	}
}
