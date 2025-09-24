namespace YunSun.Game
{
	using System.Diagnostics;

	static public partial class Log
	{
		const string logChunk = "<color=green>[Game]</color>";

		[Conditional( "_LOG_GAME_" ), Conditional( "UNITY_EDITOR" )]
		static public void Output( string format, params object[] args )
		{
			UnityEngine.Debug.LogFormat( "{0}\t{1} {2}"
				, UnityEngine.Time.realtimeSinceStartup.ToString( "000.00000" )
				, logChunk
				, string.Format( format, args )
				);
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

	static public partial class Log
	{
		[Conditional( "_LOG_GAME_OPTION_" )]
		static public void Option( string format, params object[] args )
		{
			const string subChunk = "<color=cyan>[Option]</color>";
			UnityEngine.Debug.LogFormat( "{0}\t{1}{2} {3}"
				, UnityEngine.Time.realtimeSinceStartup.ToString( "000.00000" )
				, logChunk
				, subChunk
				, string.Format( format, args )
				);
		}

		[Conditional( "_LOG_GAME_QSLOT_" )]
		static public void QuickSlot( string format, params object[] args )
		{
			const string subChunk = "<color=cyan>[QuickSlot]</color>";
			UnityEngine.Debug.LogFormat( "{0}\t{1}{2} {3}"
				, UnityEngine.Time.realtimeSinceStartup.ToString( "000.00000" )
				, logChunk
				, subChunk
				, string.Format( format, args )
				);
		}

		[Conditional( "_LOG_GAME_TITLE_" ), Conditional( "UNITY_EDITOR" )]
		static public void Title( string format, params object[] args )
		{
			const string subChunk = "<color=cyan>[Title]</color>";
			UnityEngine.Debug.LogFormat( "{0}\t{1}{2} {3}"
				, UnityEngine.Time.realtimeSinceStartup.ToString( "000.00000" )
				, logChunk
				, subChunk
				, string.Format( format, args )
				);
		}
	}
}
