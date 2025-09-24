namespace YunSun.UI
{
	using UnityEngine;
	using UnityEngine.UI;
	using Game;

	//!< Image : Sprite
	static public partial class Util
	{
		static public void SetSprite( this Image obj, AtlasType type, string spriteName )
		{
			if( obj != null )
				obj.sprite = Atlas.GetSprite( type, spriteName );
		}
		static public void SetFillAmount( this Image obj, float value )
		{
			if( obj != null )
				obj.fillAmount = value;
		}
		static public void SetFillAmount( this Image obj, float value, float maxValue )
		{
			if( obj != null )
				obj.fillAmount = Mathf.Clamp01( value / maxValue );
		}
	}

	//!< RawImage : Texture
	static public partial class Util
	{
		/*
		static public void SetTexture( this RawImage obj, string imageName, TextureType type, bool activeAfterLoad = false, bool clearAfterUse = false )
		{
			if( obj != null )
			{
				var helper = obj.GetComponent<RawImageHelperUI>();
				if( helper == null )
					helper = obj.gameObject.AddComponent<RawImageHelperUI>();
				if( helper != null )
					helper.SetImage( imageName, type, activeAfterLoad, clearAfterUse );
			}
		}
		*/
	}
}
