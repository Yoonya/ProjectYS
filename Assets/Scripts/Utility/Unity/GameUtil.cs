namespace YunSun
{
	using System.Collections.Generic;
	using UnityEngine;

	public static partial class GameUtil
	{
		static public bool IsActive( this GameObject obj )
			=> obj == null ? false : obj.activeSelf;
		static public bool IsActive( this Component obj )
			=> obj == null ? false : IsActive( obj.gameObject );
		static public bool IsActiveInHierarchy( this GameObject obj )
			=> obj == null ? false : obj.activeInHierarchy;
		static public bool IsActiveInHierarchy( this Component obj )
			=> obj == null ? false : IsActiveInHierarchy( obj.gameObject );

		static public void SetActiveEx( this GameObject obj, bool value )
		{
			if( obj != null )
				obj.SetActive( value );
		}
		static public void SetActiveEx( this Component obj, bool value )
		{
			if( obj != null &&
				obj.gameObject != null )
				obj.gameObject.SetActive( value );
		}

		static public void SetParent( this GameObject obj, Transform parent )
		{
			if( obj != null &&
				obj.transform != null )
				obj.transform.SetParent( parent );
		}
		static public void SetParent( this GameObject obj, GameObject parent )
			=> SetParent( obj, parent == null ? null : parent.transform );
		static public void SetParent( this Component obj, Transform parent )
		{
			if( obj != null &&
				obj.transform != null )
				obj.transform.SetParent( parent );
		}
		static public void SetParent( this Component obj, GameObject parent )
			=> SetParent( obj, parent == null ? null : parent.transform );

		static public void SetEnabled( this Behaviour obj, bool value )
		{
			if( obj != null )
				obj.enabled = value;
		}
	}

	public static partial class GameUtil
	{
		static public T AddMissingComponent<T>( this GameObject obj ) where T : Component
		{
			var gc = obj.GetComponent<T>();
			if( gc == null )
				gc = obj.AddComponent<T>();
			return gc;
		}

		static public GameObject FindChild( this Transform obj, string name )
		{
			if( obj != null )
			{
				if( obj.name.Equals( name ) )
					return obj.gameObject;

				foreach( Transform child in obj )
				{
					var found = FindChild( child, name );
					if( found != null )
						return found;
				}
			}
			return null;
		}
		static public GameObject FindChild( this Component obj, string name )
			=> obj == null ? null : FindChild( obj.transform, name );
		static public GameObject FindChild( this GameObject obj, string name )
			=> obj == null ? null : FindChild( obj.transform, name );

		static public T FindChildComponent<T>( this Transform obj ) where T : Component
			=> obj == null ? null : obj.GetComponentInChildren<T>( true );
		static public T FindChildComponent<T>( this Component obj ) where T : Component
			=> obj == null ? null : FindChildComponent<T>( obj.transform );
		static public T FindChildComponent<T>( this GameObject obj ) where T : Component
			=> obj == null ? null : FindChildComponent<T>( obj.transform );

		static public List<T> FindChildComponents<T>( this Transform obj ) where T : Component
			=> obj == null ? new List<T>() : new List<T>( obj.GetComponentsInChildren<T>( true ) );
		static public List<T> FindChildComponents<T>( this Component obj ) where T : Component
			=> obj == null ? new List<T>() : FindChildComponents<T>( obj.transform );
		static public List<T> FindChildComponents<T>( this GameObject obj ) where T : Component
			=> obj == null ? new List<T>() : FindChildComponents<T>( obj.transform );

		static public void SetLayerRecursively( this GameObject obj, int layer )
		{
			if( obj != null )
			{
				obj.layer = layer;

				if( obj.transform != null )
				{
					foreach( Transform child in obj.transform )
						SetLayerRecursively( child.gameObject, layer );
				}
			}
		}
		static public void SetLayerRecursively( this Component obj, int layer )
			=> SetLayerRecursively( obj == null ? null : obj.gameObject, layer );

		static public T[] GetEnumValues<T>() where T : System.Enum
			=> (T[])System.Enum.GetValues( typeof( T ) );
	}

	public static partial class GameUtil
	{
		static public void SetAniSpeed( this Animation ani, float speed )
		{
			if( ani != null )
			{
				foreach( AnimationState it in ani )
				{
					it.speed = speed;
				}
			}
		}
		static public void SetAniSpeed( this Animation ani, string name, float speed )
		{
			if( ani != null )
			{
				AnimationState it = ani[ name ];
				if( it != null )
					it.speed = speed;
			}
		}
	}

	public static partial class GameUtil
	{
		static public bool AddIfNotNull<T>( this List<T> list, T item )
		{
			if( item == null )
				return false;
			list.Add( item );
			return true;
		}
		static public bool AddIfNotExists<T>( this List<T> list, T item )
		{
			if( list.Contains( item ) )
				return false;
			list.Add( item );
			return true;
		}
	}
}
