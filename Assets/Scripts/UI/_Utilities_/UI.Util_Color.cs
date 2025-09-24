namespace YunSun.UI
{
	using UnityEngine;
	using System.Collections.Generic;

	static public partial class Util
	{
		static public Color IntToColor( int val )
		{
			float inv = 1f / 255f;
			Color c = Color.black;
			c.r = inv * ((val >> 24) & 0xFF);
			c.g = inv * ((val >> 16) & 0xFF);
			c.b = inv * ((val >> 8) & 0xFF);
			c.a = inv * (val & 0xFF);
			return c;
		}
		static public Color HexToColor( uint val )
		{
			return IntToColor( (int)val );
		}
		public static Color HexToColor( string hexCode )
		{
			Color color;
			if( ColorUtility.TryParseHtmlString( hexCode, out color ) )
			{
				return color;
			}
			return Color.white;
		}
		static public Color GetRaidDifficultyTextColor( int difficulty )
		{
			Color col =  Color.white;
			switch( difficulty )
			{
				case 0:
					ColorUtility.TryParseHtmlString( "#FFF4E0", out col );
					break;
				case 1:
					ColorUtility.TryParseHtmlString( "#CEE065", out col );
					break;
				case 2:
					ColorUtility.TryParseHtmlString( "#FF9B45", out col );
					break;
				case 3:
					ColorUtility.TryParseHtmlString( "#FF4545", out col );
					break;
			}
			return col;
		}

		static public Color GetRaidDifficultyPanelColor( int difficulty )
		{
			Color col =  Color.white;
			switch( difficulty )
			{
				case 1:
					ColorUtility.TryParseHtmlString( "#DEFF60", out col );
					break;
				case 2:
					ColorUtility.TryParseHtmlString( "#FFC460", out col );
					break;
				case 3:
					ColorUtility.TryParseHtmlString( "#FF6060", out col );
					break;
			}
			return col;
		}
	}
}
