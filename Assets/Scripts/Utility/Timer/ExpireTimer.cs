namespace YunSun
{
	using System;
	using UnityEngine;

	public class ExpireTimer
	{
		private double _expireTime;

		public bool IsExpire => _expireTime <= Time.realtimeSinceStartupAsDouble;
		public TimeSpan RemainTime => TimeSpan.FromSeconds( IsExpire ? 0 : _expireTime - Time.realtimeSinceStartupAsDouble );
		public DateTime ExpireTime => DateTime.Now + RemainTime;

		public int Days => IsExpire ? 0 : RemainTime.Days;
		public int Hours => IsExpire ? 0 : RemainTime.Hours;
		public int Minutes => IsExpire ? 0 : RemainTime.Minutes;
		public int Seconds => IsExpire ? 0 : RemainTime.Seconds;
		public double TotalDays => IsExpire ? 0 : RemainTime.TotalDays;
		public double TotalHours => IsExpire ? 0 : RemainTime.TotalHours;
		public double TotalMinutes => IsExpire ? 0 : RemainTime.TotalMinutes;
		public double TotalSeconds => IsExpire ? 0 : RemainTime.TotalSeconds;

		public ExpireTimer()
			=> _expireTime = 0;
		public ExpireTimer( int hours, int minutes, int seconds )
			=> this.Active( hours, minutes, seconds );
		public ExpireTimer( TimeSpan time )
			=> this.Active( time.TotalSeconds );
		public ExpireTimer( double seconds )
			=> this.Active( seconds );

		public void Active( int hours, int minutes, int seconds )
			=> Active( new TimeSpan( hours, minutes, seconds ).TotalSeconds );
		public void Active( TimeSpan time )
			=> Active( time.TotalSeconds );
		public void Active( double time )
			=> _expireTime = Time.realtimeSinceStartupAsDouble + time;

		public override string ToString()
		{
			var sb = new System.Text.StringBuilder();
			{
				sb.Append( IsExpire
					? "<color=orange>Expire</color>"
					: "<color=green>Remain</color>"
					);
				sb.Append( ", " );
				sb.Append( this.ExpireTime.ToString( "yyyy-MM-dd HH:mm:ss" ) );

				if( !IsExpire )
				{
					var remainTime = this.RemainTime;
					sb.Append( ", <color=white>(" );
					sb.AppendFormat( " {0}d", remainTime.Days );
					sb.AppendFormat( " {0}h", remainTime.Hours );
					sb.AppendFormat( " {0}m", remainTime.Minutes );
					sb.AppendFormat( " {0}s", remainTime.Seconds );
					sb.Append( " )</color>" );
				}
			}
			return sb.ToString();
		}
	}
}
