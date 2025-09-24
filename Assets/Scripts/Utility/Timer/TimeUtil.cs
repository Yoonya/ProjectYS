namespace YunSun
{
	using System;
	using System.Globalization;
	using UnityEngine;

	static public class TimeUtil
	{
		const string DateTimeNone = "0000-00-00 00:00";

		static public long ParseDateTimeToTicks( string input, bool utc )
		{
			if( DateTimeNone.Equals( input ) )
				return 0;

			var dateTime = DateTime.ParseExact( input, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture );
			return utc
				? DateTime.SpecifyKind( dateTime, DateTimeKind.Utc ).Ticks
				: DateTime.SpecifyKind( dateTime, DateTimeKind.Unspecified ).Ticks
				;
		}

		//!< UTC
		static public DateTime ConvertLocalTimeFromUtc( DateTime utcTime )
			=> utcTime.ToLocalTime();
		static public DateTime ConvertLocalTimeFromUtc( long utcTicks )
			=> utcTicks <= 0
			? DateTime.MinValue
			: ConvertLocalTimeFromUtc( new DateTime( utcTicks, DateTimeKind.Utc ) )
			;

		static public bool IsRangeNowTimeFromUtcTime( long utcStartTicks, long utcEndTicks )
		{
			if( 0 < utcStartTicks )
			{
				var str = ConvertLocalTimeFromUtc( utcStartTicks ).Ticks;
				if( DateTime.Now.Ticks < str )
					return false;
			}
			if( 0 < utcEndTicks )
			{
				var end = ConvertLocalTimeFromUtc( utcEndTicks ).Ticks;
				if( DateTime.Now.Ticks > end )
					return false;
			}
			return true;
		}

		//!< KST
		static public DateTime ConvertLocalTimeFromKst( DateTime kstTime )
			=> kstTime.AddHours( -9 ).ToLocalTime();
		static public DateTime ConvertLocalTimeFromKst( long kstTicks )
			=> kstTicks <= 0
			? DateTime.MinValue
			: ConvertLocalTimeFromKst( new DateTime( kstTicks, DateTimeKind.Unspecified ) )
			;

		static public bool IsRangeNowTimeFromKstTime( long kstStartTicks, long kstEndTicks )
		{
			if( 0 < kstStartTicks )
			{
				var str = ConvertLocalTimeFromKst( kstStartTicks ).Ticks;
				if( DateTime.Now.Ticks < str )
					return false;
			}
			if( 0 < kstEndTicks )
			{
				var end = ConvertLocalTimeFromKst( kstEndTicks ).Ticks;
				if( DateTime.Now.Ticks > end )
					return false;
			}
			return true;
		}
	}
}
