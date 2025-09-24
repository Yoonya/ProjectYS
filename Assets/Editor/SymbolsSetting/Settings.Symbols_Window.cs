namespace YunSun.Editor.Settings
{
	using UnityEngine;
	using UnityEditor;

	public partial class Symbols
	{
		static string symbolsFilePath = string.Empty;
		static public void SetSymbolsFilePath( string path )
		{
			symbolsFilePath = path;
		}

		public class Window : EditorWindow
		{
			private AllSymbols	allSymbols	= new AllSymbols();
			private UseSymbols	useSymbols	= new UseSymbols();
			private Vector2		scroll		= new Vector2();

			public Window()
			{
				allSymbols = new AllSymbols();
				useSymbols = new UseSymbols();
			}

			private void OnEnable()
			{
				allSymbols.Reload( symbolsFilePath );
				useSymbols.Reload();
			}
			private void OnGUI()
			{
				GUILayoutOption hButton = GUILayout.Height( 21f );

				EditorGUILayout.BeginVertical();
				{
					EditorGUI.BeginDisabledGroup( Application.isPlaying );
					EditorGUILayout.BeginVertical();
					{
						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField( "" );
						if( GUILayout.Button( "Reload", GUILayout.Width( 100f ), hButton ) )
						{
							allSymbols.Reload( symbolsFilePath );
							useSymbols.Reload();
						}
						if( GUILayout.Button( "Accept", GUILayout.Width( 100f ), hButton ) )
						{
							useSymbols.Accept();
							Close();
						}
						EditorGUILayout.EndHorizontal();
					}
					EditorGUILayout.EndVertical();

					EditorGUILayout.BeginVertical( "Box" );
					{
						OnSymbolsGUI( useSymbols, allSymbols, ref scroll );
					}
					EditorGUILayout.EndVertical();
					EditorGUI.EndDisabledGroup();
				}
				EditorGUILayout.EndVertical();
			}
		}

		static public void OnSymbolsGUI( UseSymbols useSymbols, AllSymbols allSymbols, ref Vector2 scroll )
		{
			GUILayoutOption wSymbol = GUILayout.Width( 260f );
			GUILayoutOption hSymbol	= GUILayout.Height( 21f );
			GUILayoutOption hGroup = GUILayout.Height( 20f );

			EditorGUILayout.BeginHorizontal();
			{
				GUI.enabled = false;
				GUI.contentColor = Color.green;
				GUI.backgroundColor = Color.black;
				GUILayout.Button( "SYMBOL", wSymbol, hSymbol );
				GUILayout.Button( "DESCRIPTION", hSymbol );
				GUI.contentColor = Color.white;
				GUI.backgroundColor = Color.white;
				GUI.enabled = true;
			}
			EditorGUILayout.EndHorizontal();

			scroll = EditorGUILayout.BeginScrollView( scroll );
			{
				foreach( string it in useSymbols.list )
				{
					if( true == allSymbols.Contains( it ) )
						continue;

					EditorGUILayout.BeginHorizontal();
					{
						GUI.enabled = false;
						GUILayout.Toggle( true, string.Format( " {0}", it ), wSymbol, hSymbol );
						EditorGUILayout.LabelField( "...", hSymbol );
						GUI.enabled = true;
					}
					EditorGUILayout.EndHorizontal();
				}
				foreach( Symbol it in allSymbols.list )
				{
					if( it.state == State.Unused )
						continue;

					if( it.group )
					{
						EditorGUILayout.Separator();
						EditorGUILayout.BeginHorizontal();
						GUI.enabled = false;
						GUI.contentColor = Color.green;
						GUILayout.Button( it.name, hGroup, wSymbol );
						GUILayout.Button( it.desc, hGroup );
						GUI.contentColor = Color.white;
						GUI.enabled = true;
						EditorGUILayout.EndHorizontal();
						continue;
					}

					EditorGUILayout.BeginHorizontal();
					{
						bool isUse = useSymbols.Contains( it.name );
						GUI.color = isUse ? Color.yellow : Color.white;
						GUI.enabled = it.state == State.Enable;
						{
							bool isCheck = GUILayout.Toggle( isUse, string.Format( " {0}", it.name ), wSymbol, hSymbol );
							if( isCheck != isUse )
							{
								if( isCheck )
									useSymbols.Add( it.name );
								else
									useSymbols.Remove( it.name );
							}
							EditorGUILayout.LabelField( it.desc, hSymbol );
						}
						GUI.enabled = true;
						GUI.color = Color.white;
					}
					EditorGUILayout.EndHorizontal();
				}
			}
			EditorGUILayout.EndScrollView();
		}
	}
}
