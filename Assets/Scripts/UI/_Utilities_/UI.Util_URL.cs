namespace YunSun.UI
{
	using System;
	using UnityEngine;
	//using Toast.Gamebase;

	static public partial class Util
	{
		static public float _UrlOpenTime_ = 0f;
		static public float _UrlOpenInterval_ = 3f;

		static public void OpenURL( string url )
		{
			if( string.IsNullOrWhiteSpace( url ) )
			{
				//MessageUI.ActiveKey( "Common_ContentsReady" );
				return;
			}

			if( _UrlOpenTime_ > Time.realtimeSinceStartup )
				return;
			_UrlOpenTime_ = Time.realtimeSinceStartup + _UrlOpenInterval_;

#if UNITY_EDITOR
			Application.OpenURL( url );
#else
			//Gamebase.Webview.ShowWebView( url, null, error => _UrlOpenTime_ = Time.realtimeSinceStartup );
#endif
		}
		static public void OpenURL_Browser( string url )
		{
			if( string.IsNullOrWhiteSpace( url ) )
			{
				//MessageUI.ActiveKey( "Common_ContentsReady" );
				return;
			}

#if UNITY_EDITOR
			Application.OpenURL( url );
#else
			//Gamebase.Webview.OpenWebBrowser( url );
#endif
		}
		static public void OpenUrlByKey( string key )
		{
			//OpenURL( GuideAddressTable.GetAddressByName( key ) );
		}
		static public void OpenUrlBrowserByKey( string key )
		{
			//OpenURL_Browser( GuideAddressTable.GetAddressByName( key ) );
		}
	}
}
