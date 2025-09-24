namespace YunSun.UI
{
	using TMPro;

	public enum TextStyle
	{
		Normal          = 0,
		Main            = 1,
		MainGolden      = 5,

		Good_01         = 50,
		Good_02         = 51,

		Btn_On          = 52,
		Btn_Off         = 53,

		Warning_01      = 55,
		Warning_02      = 56,
		Warning_03      = 57,

		Point           = 60,
	}

	static public partial class Util
	{
		static public void SetTextStyle( this TMP_Text obj, string name )
		{
			if( obj != null && TMP_Settings.defaultStyleSheet != null )
				obj.textStyle = TMP_Settings.defaultStyleSheet.GetStyle( name );
		}
		static public void SetTextStyle( this TMP_Text obj, TextStyle style )
		{
			if( obj != null && TMP_Settings.defaultStyleSheet != null )
				obj.textStyle = TMP_Settings.defaultStyleSheet.GetStyle( GetTextStyleName( style ) );
		}
		static private string GetTextStyleName( TextStyle style )
		{
			switch( style )
			{
				case TextStyle.Main: return "Main";
				case TextStyle.MainGolden: return "MainGolden";
				case TextStyle.Good_01: return "good_01";
				case TextStyle.Good_02: return "good_02";
				case TextStyle.Btn_On: return "Btn_On";
				case TextStyle.Btn_Off: return "Btn_Off";
				case TextStyle.Warning_01: return "warning_01";
				case TextStyle.Warning_02: return "warning_02";
				case TextStyle.Warning_03: return "warning_03";
				case TextStyle.Point: return "Point";
				case TextStyle.Normal: return "Normal";
				default: return "Normal";
			}
		}
	}
}
