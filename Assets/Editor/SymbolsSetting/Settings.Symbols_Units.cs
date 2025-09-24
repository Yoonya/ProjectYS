namespace YunSun.Editor.Settings
{
	using System;
	using System.IO;
	using System.Text;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEditor;

	public partial class Symbols
	{
		public enum State
		{
			Unused,
			Enable,
			Editor,
		}

		public class Symbol
		{
			public bool     group   = false;
			public State    state   = State.Enable;
			public string   name    = string.Empty;
			public string   desc    = string.Empty;
		}
		public class AllSymbols
		{
			public List<Symbol> list { private set; get; }

			public AllSymbols()
			{
				this.list = new List<Symbol>( 32 );
			}
			public void Reload( string path = null )
			{
				if( string.IsNullOrEmpty( path ) )
					return;

				list.Clear();
				{
					if( false == File.Exists( path ) )
						return;

					using( StreamReader sr = new StreamReader( path ) )
					{
						while( true != sr.EndOfStream )
						{
							string _text = sr.ReadLine();
							{
								int index = _text.IndexOf( "///" );
								if( -1 < index )
									_text = _text.Substring( 0, index );

								if( string.IsNullOrWhiteSpace( _text ) )
									continue;
							}

							string[] texts = _text.Split( ',', ':', '=' );
							if( 1 < texts.Length )
							{
								if( texts[ 0 ].Contains( "#.GROUP" ) )
								{
									var it = new Symbol();
									{
										it.group = true;
										it.name = texts[1].Trim( ' ', '\t' );
									}
									list.Add( it );
								}
								else
								{
									var it = new Symbol();
									{
										it.group = false;
										it.name = texts[0].Trim( ' ', '\t' );
										if( 0 < texts.Length )
										{
											var str = texts[1].Trim( ' ', '\t' );
											if( !string.IsNullOrWhiteSpace( str ) )
												it.state = (State)Enum.Parse( typeof( State ), str );
										}
										if( 1 < texts.Length )
											it.desc = texts[2].Trim( ' ', '\t' );
									}
									list.Add( it );
								}
							}
						}
						sr.Close();
					}
				}
			}
			public bool Contains( string symbol )
			{
				foreach( Symbol it in list )
				{
					if( symbol.Equals( it.name ) )
						return true;
				}
				return false;
			}
		}
		public class UseSymbols
		{
			public HashSet<string> list { private set; get; }

			public UseSymbols()
			{
				this.list = new HashSet<string>();
			}
			public void Add( string value )
			{
				if( string.IsNullOrEmpty( value ) )
					return;

				list.Add( value );
			}
			public void Remove( string value )
			{
				list.Remove( value );
			}
			public bool Contains( string value )
			{
				return list.Contains( value );
			}

			public void Reload()
			{
				list.Clear();

				string[] symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup( EditorUserBuildSettings.selectedBuildTargetGroup ).Split( ';' );
				{
					foreach( string it in symbols )
					{
						Add( it );
					}
				}
			}
			public void Accept()
			{
				StringBuilder sb = new StringBuilder();
				{
					foreach( string it in list )
					{
						sb.Append( it );
						sb.Append( ";" );
					}
				}
				PlayerSettings.SetScriptingDefineSymbolsForGroup( EditorUserBuildSettings.selectedBuildTargetGroup, sb.ToString() );
			}
		}
	}
}
