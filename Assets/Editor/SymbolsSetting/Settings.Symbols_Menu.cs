namespace YunSun.Editor.Settings
{
	using UnityEditor;

	public partial class Symbols
	{
		public class Menu
		{
			const int MenuProperty = 99000;
			const string MenuPath = "YunSunMenu/Settings/";
			const string MenuName = "Scripting Define Symbols Setting";
			const string SymbolsPath = "ProjectSettings/ProjectSymbols.txt";

			[MenuItem( MenuPath + MenuName, false, MenuProperty )]
			static private void Open()
			{
				Symbols.SetSymbolsFilePath( SymbolsPath );
				EditorWindow.GetWindow<Symbols.Window>( MenuName );
			}
			[MenuItem( MenuPath + MenuName, true )]
			static private bool Active()
			{
				return false == UnityEngine.Application.isPlaying;
			}
		}
	}
}
