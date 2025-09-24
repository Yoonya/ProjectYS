namespace YunSun
{
	using System.Collections;
	using UnityEngine;

	static public partial class AppConfig
	{
		public const float OnBackDelayTime = 0.3f;
		public const float UIActiveWaitTime = 0.3f;
		public const float UIReleaseWaitTime = 120f;
	}

	static public partial class AppConfig
	{
#if _QA_MODE_ || UNITY_EDITOR
		public const bool IsQAMode = true;
#else //!< _LIVE_MODE_
		public const bool IsQAMode = false;
#endif
	}

	static public partial class AppConfig
	{
#if UNITY_ANDROID && _ONE_STORE_
		public const ShopStoreType ShopStore = ShopStoreType.kShopStoreType_OneStore;
#elif UNITY_ANDROID && _GALAXY_STORE_
		public const ShopStoreType ShopStore = ShopStoreType.kShopStoreType_Galaxy;
#elif UNITY_ANDROID
		public const ShopStoreType ShopStore = ShopStoreType.kShopStoreType_Google;
#elif UNITY_IOS
		public const ShopStoreType ShopStore = ShopStoreType.kShopStoreType_Apple;
#else
		public const ShopStoreType ShopStore = ShopStoreType.kShopStoreType_None;
#endif
	}

	static public partial class AppConfig
	{
		static public OSType GetOSType()
		{
			return Application.platform switch
			{
				RuntimePlatform.Android => OSType.kOSType_AOS,
				RuntimePlatform.IPhonePlayer => OSType.kOSType_IOS,
				RuntimePlatform.WindowsPlayer => OSType.kOSType_WINDOWS,
				_ => OSType.kOSType_None,
			};
		}
	}

	static public partial class AppConfig
	{
		static public bool IsServiceInKorea()
			=> Application.systemLanguage == SystemLanguage.Korean;
	}
}
