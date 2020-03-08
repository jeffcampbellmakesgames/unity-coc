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

namespace JCMG.COC.Editor
{
	/// <summary>
	/// Constant fields for this library.
	/// </summary>
	public static class COCEditorConstants
	{
		// Paths
		public const string ASSET_ROOT = "Assets";

		// Search Filters
		public const string FIND_ALL_GRAPHS_FILTER = "t:COCNodeGraph";
		public const string FIND_ALL_SETTINGS_FILTER = "t:COCSettings";

		// Logs
		public const string FOLDER_CREATED_LOG = "[COC] Folder created at \"{0}\"";

		// Namespace
		internal const string NAMESPACE = "JCMG.COC.Editor";

		// Ports
		public const string INPUT_FOLDERS_PORT_NAME = "_childFolders";
		public const string OUTPUT_FOLDERS_PORT_NAME = "_outputFolders";

		// Settings
		public const string DEFAULT_COC_CODE_GEN_PATH = "Generated";

		// UI
		// General UI
		public const float LABEL_WIDTH = 200f;
		public const string BOX_STYLE = "GroupBox";

		// Preferences and Settings
		public const string CODE_GEN_OUTPUT_PATH_LABEL = "Code Generation Output Path";
		public const string CODE_GEN_OUTPUT_PATH_TOOLTIP = "The location at which code will be generated for this library.";

		// Warnings
		public const string INVALID_EMPTY_FOLDER_NAME = "A folder name cannot be empty, please enter a name";

		// Graph Inspector
		public const string SETTINGS_TITLE = "Settings";

		public const string INCLUDE_AUTOMATIC_GENERATION_PROPERTY_NAME = "_includeInAutomaticGeneration";
		public const string INCLUDE_AUTOMATIC_GENERATION_TOOLTIP = "If enabled, this graph's content will be automatically" +
		                                                           "generated when the Unity Editor is first opened as " +
		                                                           "well as on Assembly Recompile.";

		public const string ACTIONS_TITLE = "Actions";

		public const string EDIT_GRAPH_BUTTON_LABEL = "Edit graph";
		public const string EDIT_GRAPH_BUTTON_TOOLTIP = "Launches the node graph editor for this graph";

		public const string GENERATE_FOLDERS_BUTTON_LABEL = "Generate Folders";
		public const string GENERATE_FOLDERS_BUTTON_TOOLTIP = "Generates all folders for this graph based on the node " +
		                                                      "hierarchy connected to the AssetRoot node.";

		public const string GENERATE_CODE_LABEL = "Should Generate Code";
		public const string GENERATE_CODE_TOOLTIP = "If enabled, methods will be generated for retrieving the relative " +
		                                            "and absolute path to this folder.";

		public const string GENERATE_CODE_ID_LABEL = "Unique Name";

		public const string GENERATE_CODE_ID_TOOLTIP = "This name will be used during code generation to help form the " +
		                                               "methods for retrieving this folder. This should be distinct from " +
		                                               "other folders set to use code generation.";
	}
}
