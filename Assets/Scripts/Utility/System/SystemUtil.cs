namespace YunSun
{
	using UnityEngine;
	using UnityEngine.EventSystems;
	using Game;
	using System.Diagnostics;
	using System.Runtime.Serialization.Formatters.Binary;
	using System.Runtime.InteropServices;

	static public partial class SystemUtil
	{
		[Conditional( "_QA_MODE_" )]
		static public void LoadLogReporter()
		{
			var gc = Object.FindObjectOfType<Reporter>( true );
			if( gc != null )
				return;

			const string PrefabName = "LogReporter";
			const string PrefabRescPath = "System/LogReporter";

			var res = Resources.Load<GameObject>( PrefabRescPath );
			if( res != null )
			{
				var obj = GameObject.Instantiate( res );
				{
					obj.name = PrefabName;
					GameObject.DontDestroyOnLoad( obj );
				}
				var sys = obj.GetComponent<Reporter>();
				if( sys != null )
				{
					Log.Output( "System load success. : {0}"
						, $"<color=yellow>{PrefabName}</color>"
						);
				}
				else
				{
					Log.Error( "{0} not exists. : {1}", PrefabName, PrefabRescPath );
				}
			}
			else
			{
				Log.Error( "(0) load failed!", PrefabRescPath );
			}
		}

		static public void LoadEventSystem()
		{
			if( null != EventSystem.current )
				return;

			const string SystemName = "EventSystem";
			const string SystemRescPath = "System/EventSystem";

			var res = Resources.Load<GameObject>( SystemRescPath );
			if( res != null )
			{
				var obj = GameObject.Instantiate( res );
				{
					obj.name = SystemName;
					GameObject.DontDestroyOnLoad( obj );
				}
				var sys = obj.GetComponent<EventSystem>();
				if( sys != null )
				{
					Log.Output( "System load success. : {0}"
						, $"<color=yellow>{SystemName}</color>"
						);
				}
				else
				{
					Log.Error( "{0} not exists. : {1}", SystemName, SystemRescPath );
				}
			}
			else
			{
				Log.Error( "(0) load failed!", SystemRescPath );
			}
		}
	}

	static public partial class SystemUtil
	{
		static public byte[] ClassToByteArray<T>( T structure ) where T : class
		{
			byte[] bb = new byte[Marshal.SizeOf(typeof(T))];
			GCHandle gch = GCHandle.Alloc(bb, GCHandleType.Pinned);
			Marshal.StructureToPtr( structure, gch.AddrOfPinnedObject(), false );
			gch.Free();
			return bb;
		}

		static public byte[] StructToByteArray<T>( T structure ) where T : struct
		{
			byte[] bb = new byte[Marshal.SizeOf(typeof(T))];
			GCHandle gch = GCHandle.Alloc(bb, GCHandleType.Pinned);
			Marshal.StructureToPtr( structure, gch.AddrOfPinnedObject(), false );
			gch.Free();
			return bb;
		}
	}
}

namespace YunSun
{
	using System.Diagnostics;

	static public partial class SystemUtil
	{
		static class Log
		{
			const string logChunk = "<color=cyan>[[ System ]]</color>";

			[Conditional( "UNITY_EDITOR" )]
			static public void Output( string format, params object[] args )
			{
				UnityEngine.Debug.LogFormat( "{0}\t{1} {2}"
					, UnityEngine.Time.realtimeSinceStartup.ToString( "000.00000" )
					, logChunk
					, string.Format( format, args )
					);
			}

			static public void Error( string format, params object[] args )
			{
				UnityEngine.Debug.LogWarningFormat( "{0}\t{1} {2}"
					, UnityEngine.Time.realtimeSinceStartup.ToString( "000.00000" )
					, logChunk
					, string.Format( format, args )
					);
			}
		}
	}
}
