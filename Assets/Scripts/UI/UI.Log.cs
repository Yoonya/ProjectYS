namespace YunSun.UI
{
	using System.Diagnostics;

	static public class Log
	{
		const string logChunk = "<color=#00cc00>[[ UI ]]</color>";

		[Conditional( "_LOG_UI_DETAIL_" )/*, Conditional( "UNITY_EDITOR" )*/]
		static public void Detail( string format, params object[] args )
		{
			const string logChunk = "<color=green>[[ UI ]]</color>";

			UnityEngine.Debug.LogFormat( "{0}\t{1} {2}"
				, UnityEngine.Time.realtimeSinceStartup.ToString( "000.00000" )
				, logChunk
				, string.Format( format, args )
				);
		}

		[Conditional( "_LOG_UI_" ), Conditional( "UNITY_EDITOR" )]
		static public void Output( string format, params object[] args )
		{
			UnityEngine.Debug.LogFormat( "{0}\t{1} {2}"
				, UnityEngine.Time.realtimeSinceStartup.ToString( "000.00000" )
				, logChunk
				, string.Format( format, args )
				);
		}

		static public void Break( string format, params object[] args )
		{
			var msg = string.Format( "{0}\t{1} {2}"
				, UnityEngine.Time.realtimeSinceStartup.ToString( "000.00000" )
				, logChunk
				, string.Format( format, args )
				);

			throw new System.Exception( msg );
		}

		static public void Warning( string format, params object[] args )
		{
			UnityEngine.Debug.LogWarningFormat( "{0}\t{1} {2}"
				, UnityEngine.Time.realtimeSinceStartup.ToString( "000.00000" )
				, logChunk
				, string.Format( format, args )
				);
		}

		static public void Error( string format, params object[] args )
		{
			UnityEngine.Debug.LogErrorFormat( "{0}\t{1} {2}"
				, UnityEngine.Time.realtimeSinceStartup.ToString( "000.00000" )
				, logChunk
				, string.Format( format, args )
				);
		}
	}
}
