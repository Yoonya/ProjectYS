namespace YunSun
{
	using System;
	using UnityEngine;
	using UnityEngine.AddressableAssets;

	public static partial class ResourceManager
	{
		public static void InstantiateObject( string key, Transform parent = null, Action<GameObject> callback = null )
		{
			Log.Output( "Instance Try. : {0}", $"<color=white>{key}</color>" );			
			Addressables.LoadResourceLocationsAsync( key ).Completed += ( handle ) =>
			{
				if( 0 < handle.Result.Count )
				{
					Addressables.InstantiateAsync( handle.Result[0], parent ).Completed += ( goHandle ) =>
					{
						Log.Output( "Instantiate. : {0}", $"<color=white>{key}</color>" );
						callback?.Invoke( goHandle.Result );
					};
				}
				else
				{
					Log.Error( "Resource isn't exists. : {0}", $"<color=white>{key}</color>" );
					callback?.Invoke( null );
				}
				Addressables.Release( handle );
			};
		}

		public static void InstantiateObject( string key, Vector3 position, Quaternion rotation, Transform parent = null, Action<GameObject> callback = null )
		{
			Log.Output( "Instance Try. : {0}", $"<color=white>{key}</color>" );
			Addressables.LoadResourceLocationsAsync( key ).Completed += ( handle ) =>
			{
				if( 0 < handle.Result.Count )
				{
					Addressables.InstantiateAsync( handle.Result[0], position, rotation, parent ).Completed += ( goHandle ) =>
					{
						Log.Output( "Instantiate. : {0}", $"<color=white>{key}</color>" );
						callback?.Invoke( goHandle.Result );
					};
				}
				else
				{
					Log.Error( "Resource isn't exists. : {0}", $"<color=white>{key}</color>" );
					callback?.Invoke( null );
				}
				Addressables.Release( handle );
			};
		}

		public static bool ReleaseInstance( GameObject obj )
		{
			return Addressables.ReleaseInstance( obj );
		}
	}
}

namespace YunSun
{
	using System.Diagnostics;

	public static partial class ResourceManager
	{
		static class Log
		{
			const string logChunk = "<color=orange>[[ Resource ]]</color>";

			[Conditional( "_LOG_RESC_" ), Conditional( "UNITY_EDITOR" )]
			static public void Output( string format, params object[] args )
			{
				UnityEngine.Debug.LogFormat( "{0}\t{1} {2}"
					, UnityEngine.Time.realtimeSinceStartup.ToString( "000.00000" )
					, logChunk
					, string.Format( format, args )
					);
			}

			[Conditional( "_LOG_RESC_" ), Conditional( "UNITY_EDITOR" )]
			static public void Warning( string format, params object[] args )
			{
				UnityEngine.Debug.LogWarningFormat( "{0}\t{1} <color=magenta>{2}</color>"
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
}
