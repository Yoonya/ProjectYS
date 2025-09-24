namespace YunSun
{
	using System;
	using System.Reflection;

	public class Singleton<T> where T : class
	{
		static private object _syncobj = new object();
		static private volatile T _instance = null;

		static public T Instance
		{
			get
			{
				lock( _syncobj ) //!< Thread safe code.
				{
					if( _instance == null )
					{
						Type t = typeof( T );

						//!< Ensure there are no public constructors
						ConstructorInfo[] ctors = t.GetConstructors();
						if( ctors.Length > 0 )
						{
							throw new InvalidOperationException( String.Format( "{0} has at least one accesible Constructor making it impossible to enforce singleton behaviour", t.Name ) );
						}

						//!< Create an instance via the private constructor
						_instance = (T)Activator.CreateInstance( t, true );
					}
				}

				return _instance;
			}
		}
	}
}
