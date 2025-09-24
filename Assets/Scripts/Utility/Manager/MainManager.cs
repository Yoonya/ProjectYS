namespace YunSun
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;

	public interface IManager
	{
		bool Initialize();
		void Destroy();
	}

	static class MainManager
	{
		static Dictionary<Type, IManager> _dic = new Dictionary<Type, IManager>();

		static public T Add<T>( T mgr ) where T : class, IManager
		{
			if( mgr == null )
			{
				Log.Warning( $"{typeof( T )} object is null." );
				return mgr;
			}

			Type type = mgr.GetType();
			if( _dic.ContainsKey( type ) )
			{
				Log.Warning( $"{mgr} is already exists." );
				return _dic[type] as T;
			}

			_dic.Add( type, mgr );
			{
				Log.Output( $"Manage : {mgr}" );

				if( mgr.Initialize() )
				{
					Log.Output( $"{mgr} is valid." );
				}
				else
				{
					Log.Warning( $"{mgr} is invalid." );
				}
			}
			return mgr;
		}
		static public T Get<T>( Type type ) where T : class, IManager
		{
			if( _dic.TryGetValue( type, out var mgr ) )
				return mgr as T;

			return null;
		}
		static public T Get<T>() where T : class, IManager
		{
			return Get<T>( typeof( T ) );
		}
		static public void Remove( Type type )
		{
			if( _dic.TryGetValue( type, out var mgr ) )
			{
				if( mgr != null )
				{
					Log.Output( $"Remove "
						+ $": Mgr( <color=green>{mgr}</color> )"
						+ $", Type( <color=white>{type}</color> )"
						);
					mgr.Destroy();
				}
				_dic.Remove( type );
			}
		}
		static public void Remove<T>() where T : class, IManager
		{
			Remove( typeof( T ) );
		}
		static public void RemoveAll()
		{
			Log.Output( $"Remove all : {_dic.Count}" );

			foreach( var mgr in _dic.Values )
			{
				if( mgr != null )
				{
					Log.Output( $"Destroy :: {mgr}" );
					mgr.Destroy();
				}
			}
			_dic.Clear();
		}

		static class Log
		{
			const string logChunk = "<color=orange>[[ Manager ]]</color>";

			[Conditional( "UNITY_EDITOR" )]
			static public void Output( string format, params object[] args )
			{
				UnityEngine.Debug.LogFormat( "{0}\t{1} {2}"
					, UnityEngine.Time.realtimeSinceStartup.ToString( "000.00000" )
					, logChunk
					, string.Format( format, args )
					);
			}

			[Conditional( "UNITY_EDITOR" )]
			static public void Warning( string format, params object[] args )
			{
				UnityEngine.Debug.LogWarningFormat( "{0}\t{1} <color=magenta>{2}</color>"
					, UnityEngine.Time.realtimeSinceStartup.ToString( "000.00000" )
					, logChunk
					, string.Format( format, args )
					);
			}
		}
	}
}
