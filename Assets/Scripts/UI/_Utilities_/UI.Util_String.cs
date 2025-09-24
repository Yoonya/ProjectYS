namespace YunSun.UI
{
	using System;
	static public partial class Util
	{
		static public string ToMillionNotationString( this long value, int decimalPlaces )
		{
			const long million = 1_000_000;
			if( value >= million )
			{
				double millions = (double)value / million;
				string formattedMillions = millions.ToString($"N{decimalPlaces}");
				return $"{formattedMillions}M";
			}
			return value.ToString( "N0" );
		}
	}
}
