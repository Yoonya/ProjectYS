namespace YunSun.UI
{
	using System;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.AddressableAssets;
	using Game;

	static public partial class Util
	{
		const string                AddrFmtPath = "UI/{0}/{1}.prefab";
		static readonly Quaternion  InitRotation = new Quaternion();
		static readonly Vector3     InitPosition = new Vector3( 0f, 0f, 0f );

		static public void Instantiate( string uiName, Transform parent, Action<GameObject> callback = null )
		{
			Log.Output( "Instance Try. : {0}", $"<color=white>{uiName}</color>" );
			var uiKey = GetLoadUIKey( uiName );
			Addressables.LoadResourceLocationsAsync( uiKey ).Completed += ( handle ) =>
			{
				if( 0 < handle.Result.Count )
				{
					var op = Addressables.InstantiateAsync( handle.Result[0], /*InitPosition, InitRotation,*/ parent );
					{
						op.Completed += ( goHandle ) =>
						{
							Log.Output( "Instantiate. : {0}", $"<color=white>{uiName}</color>" );
							callback?.Invoke( goHandle.Result );
						};
					}
				}
				else
				{
					Log.Error( "Resource isn't exists. : {0}", $"<color=white>{uiKey}</color>" );
					callback?.Invoke( null );
				}
				Addressables.Release( handle );
			};
		}
		static public bool Instantiate( string uiName, Transform parent, out GameObject uiObject )
		{
			Log.Output( "Instance Try. : {0}", $"<color=white>{uiName}</color>" );
			var uiKey = GetLoadUIKey( uiName );
			var hLocation = Addressables.LoadResourceLocationsAsync( uiKey );
			var Locations = hLocation.WaitForCompletion();
			if( Locations.Count == 0 )
			{
				Log.Error( $"Resource isn't exists. : <color=white>{uiKey}</color>" );
				Addressables.Release( hLocation );
				uiObject = null;
				return false;
			}

			var hObject = Addressables.InstantiateAsync( Locations[0].PrimaryKey, InitPosition, InitRotation, parent );
			var objResc = hObject.WaitForCompletion();
			if( objResc == null )
			{
				Log.Error( $"UIObject load failed. : <color=white>{uiName}</color>" );
				Addressables.Release( hLocation );
				Addressables.Release( hObject );
				uiObject = null;
				return false;
			}

			Log.Output( $"UIObject load completed. : <color=white>{uiName}</color>" );
			Addressables.Release( hLocation );
			uiObject = hObject.Result;
			return true;
		}
		static public void Release( GameObject obj )
		{
			Log.Output( "Release : {0}", $"<color=white>{obj.name}</color>" );

			Addressables.ReleaseInstance( obj );
		}
		static public void Release<T>( T ui ) where T : MonoBehaviour
		{
			if( ui != null )
			{
				Release( ui.gameObject );
			}
		}
	}

	static public partial class Util
	{
		static Dictionary<string, string> _PathTable_ = null;
		static private string GetLoadUIKey( string uiName )
		{
			string uiPath = uiName;
			{
				if( _PathTable_ == null )
				{
					_PathTable_ = new( 128 );
					{
						//_PathTable_.Add( typeof( LoginPopupUI ).Name, "ServerSelectUI" );
						//_PathTable_.Add( typeof( ServerListPopupUI ).Name, "ServerSelectUI" );
						//_PathTable_.Add( typeof( CharacterListPopupUI ).Name, "ServerSelectUI" );
					}
				}
				if( _PathTable_.TryGetValue( uiName, out var path ) )
				{
					uiPath = path;
					{
						Log.Output( $"<color=magenta>Custom load path </color>"
							+ $": <color=yellow>{uiPath}</color>"
							+ $"/ <color=white>{uiName}</color>"
							);
					}
				}
			}
			return string.Format( AddrFmtPath, uiPath, uiName );
		}
	}
}
