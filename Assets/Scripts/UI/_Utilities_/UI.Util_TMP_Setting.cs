namespace YunSun.UI
{
	using System.Reflection;
	using TMPro;
	using UnityEngine;
	using UnityEngine.AddressableAssets;

	static public partial class Util
	{
		const string TMPAddrFontFmtPath = "TMPro/Fonts & Materials/{0}.asset";
		const string TMPDefaultFontName = "NotoSansCJKtc-Regular SDF";
		static private TMP_FontAsset _loadedFontAsset = null;

		static public void OnTMProDefaultFontSetting()
			=> OnTMProDefaultFontSetting( TMPDefaultFontName );

		static public void OnTMProDefaultFontSetting( string fontName )
		{
			var settings = TMP_Settings.instance;
			if( settings == null )
			{
				Log.Warning( "<color=magenta>TMP_Settings is null. Make sure it is imported.</color>" );
				return;
			}

			var fontKey = string.Format( TMPAddrFontFmtPath, fontName );
			{
				Log.Output( $"TMPro Font Asset Load Try. : <color=white>{fontKey}</color>" );
			}
			Addressables.LoadResourceLocationsAsync( fontKey ).Completed += fontLocations =>
			{
				if( 0 < fontLocations.Result.Count )
				{
					Addressables.LoadAssetAsync<TMP_FontAsset>( fontLocations.Result[0].PrimaryKey ).Completed += ( dataHandle ) =>
					{
						var fieldName = "m_defaultFontAsset";
						var fieldInfo = typeof( TMPro.TMP_Settings ).GetField( fieldName, BindingFlags.Instance | BindingFlags.NonPublic );
						if( fieldInfo == null )
						{
							Log.Break( $"Unable to get '<color=white>{fieldName}</color>' of TMPro's settings." );
							return;
						}

						var fontAsset = dataHandle.Result;
						if( fontAsset == null )
						{
							Log.Error( $"TMPro Font Asset load failed. : <color=white>{fontKey}</color>" );
							return;
						}

						if( _loadedFontAsset != null )
							Addressables.Release( _loadedFontAsset );
						_loadedFontAsset = fontAsset;

						fieldInfo.SetValue( settings, _loadedFontAsset );
						{
							Log.Output( "TMPro Font Asset Default Setting : {0}"
								, $"<color=orange>{_loadedFontAsset}</color>"
								);
						}
					};
				}
				else
				{
					Log.Error( $"TMPro Font Asset isn't exists. : <color=white>{fontKey}</color>" );
				}
				Addressables.Release( fontLocations );
			};
		}
	}

	static public partial class Util
	{
		const string TMPAddrSpriteFmtPath = "TMPro/Sprite Assets/{0}.asset";
		const string TMPDefaultSpriteName = "EmojiOne";
		static private TMP_SpriteAsset _loadedSpriteAsset = null;

		static public void OnTMProDefaultSpriteSetting()
			=> OnTMProDefaultSpriteSetting( TMPDefaultSpriteName );

		static public void OnTMProDefaultSpriteSetting( string spriteName )
		{
			var settings = TMP_Settings.instance;
			if( settings == null )
			{
				Log.Warning( "<color=magenta>TMP_Settings is null. Make sure it is imported.</color>" );
				return;
			}

			var spriteKey = string.Format( TMPAddrSpriteFmtPath, spriteName );
			{
				Log.Output( $"TMPro Sprite Asset Load Try. : <color=white>{spriteKey}</color>" );
			}
			Addressables.LoadResourceLocationsAsync( spriteKey ).Completed += spriteLocations =>
			{
				if( 0 < spriteLocations.Result.Count )
				{
					Addressables.LoadAssetAsync<TMP_SpriteAsset>( spriteLocations.Result[0].PrimaryKey ).Completed += ( dataHandle ) =>
					{
						var fieldName = "m_defaultSpriteAsset";
						var fieldInfo = typeof( TMPro.TMP_Settings ).GetField( fieldName, BindingFlags.Instance | BindingFlags.NonPublic );
						if( fieldInfo == null )
						{
							Log.Break( $"Unable to get '<color=white>{fieldName}</color>' of TMPro's settings." );
							return;
						}

						var spriteAsset = dataHandle.Result;
						if( spriteAsset == null )
						{
							Log.Error( $"TMPro Sprite Asset load failed. : <color=white>{spriteKey}</color>" );
							return;
						}

						if( _loadedSpriteAsset != null )
							Addressables.Release( _loadedSpriteAsset );
						_loadedSpriteAsset = spriteAsset;

						fieldInfo.SetValue( settings, _loadedSpriteAsset );
						{
							Log.Output( "TMPro Sprite Asset Default Setting : {0}"
								, $"<color=orange>{_loadedSpriteAsset}</color>" 
								);
						}
					};
				}
				else
				{
					Log.Error( $"TMPro Sprite Asset isn't exists. : <color=white>{spriteKey}</color>" );
				}
				Addressables.Release( spriteLocations );
			};
		}
	}

	static public partial class Util
	{
		const string TMPAddrStyleFmtPath = "TMPro/Style Sheets/{0}.asset";
		const string TMPDefaultStyleName = "Default Style Sheet";
		static private TMP_StyleSheet _loadedStyleSheet = null;

		static public void OnTMProDefaultStyleSetting()
			=> OnTMProDefaultStyleSetting( TMPDefaultStyleName );

		static public void OnTMProDefaultStyleSetting( string styleName )
		{
			var settings = TMP_Settings.instance;
			if( settings == null )
			{
				Log.Warning( "<color=magenta>TMP_Settings is null. Make sure it is imported.</color>" );
				return;
			}

			var styleKey = string.Format( TMPAddrStyleFmtPath, styleName );
			{
				Log.Output( $"TMPro Style Sheep Load Try. : <color=white>{styleKey}</color>" );
			}
			Addressables.LoadResourceLocationsAsync( styleKey ).Completed += styleLocations =>
			{
				if( 0 < styleLocations.Result.Count )
				{
					Addressables.LoadAssetAsync<TMP_StyleSheet>( styleLocations.Result[0].PrimaryKey ).Completed += ( dataHandle ) =>
					{
						var fieldName = "m_defaultStyleSheet";
						var fieldInfo = typeof( TMPro.TMP_Settings ).GetField( fieldName, BindingFlags.Instance | BindingFlags.NonPublic );
						if( fieldInfo == null )
						{
							Log.Break( $"Unable to get '<color=white>{fieldName}</color>' of TMPro's settings." );
							return;
						}

						var styleSheet = dataHandle.Result;
						if( styleSheet == null )
						{
							Log.Error( $"TMPro Style Sheet Asset load failed. : <color=white>{styleSheet}</color>" );
							return;
						}

						if( _loadedStyleSheet != null )
							Addressables.Release( _loadedStyleSheet );
						_loadedStyleSheet = styleSheet;

						fieldInfo.SetValue( settings, _loadedStyleSheet );
						{
							Log.Output( "TMPro Style Sheet Default Setting : {0}"
								, $"<color=orange>{_loadedStyleSheet}</color>"
								);
						}
					};
				}
				else
				{
					Log.Error( "TMPro Style Sheep isn't exists. : {0}", $"<color=white>{styleKey}</color>" );
				}
				Addressables.Release( styleLocations );
			};
		}
	}
}
