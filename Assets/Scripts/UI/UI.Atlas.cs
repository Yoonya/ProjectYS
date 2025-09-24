namespace YunSun.UI
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.U2D;
	using UnityEngine.AddressableAssets;
	using UnityEngine.ResourceManagement.AsyncOperations;

	public enum AtlasType
	{
		BuiltIn,
		StartUI,
		MainUI,
		ItemIconUI,
		SkillIconUI,
		ShopItemUI,
		PetIconUI,
		TransIconUI,
		MonsterIconUI,
		GuildEmblemUI,
		Max,
	}

	static public partial class Atlas
	{
		static public bool IsInitialized() { return AtlasManager.Instance.IsInit; }
		static public void Initialize() { MainManager.Add( AtlasManager.Instance ); }
		static public void Destroy() { MainManager.Remove( typeof( AtlasManager ) ); }

		static public void Loading( Action<bool, float> onProgress )
			=> AtlasManager.Instance.OnLoading( onProgress );
		static public void Breaking()
			=> AtlasManager.Instance.OnBreaking();

		static public Sprite GetSprite( AtlasType type, string name )
			=> AtlasManager.Instance.GetSprite( type, name );
	}

	static public partial class Atlas
	{
		class AtlasManager : Singleton<AtlasManager>, IManager
		{
			class Unit
			{
				public AtlasType        Type;
				public SpriteAtlas      Object;
				public Dictionary<string, Sprite> Sprites;

				public Unit( AtlasType type, SpriteAtlas obj )
				{
					this.Type = type;
					this.Object = obj;
					this.Sprites = new( obj.spriteCount );
				}
			}

			private List<Unit>          _units = null;
			private Coroutine           _coLoading = null;
			private Action<bool, float> _onProgress = null;
			private int                 _loadCount = -1;

			public bool IsInit => _loadCount == 0;

			private AtlasManager()
			{
				_units = new List<Unit>( (int)AtlasType.Max );
				_loadCount = -1;
			}
			public bool Initialize()
			{
				SpriteAtlasManager.atlasRequested += RequestAtlas;

				_loadCount = 1;
				{
					LoadAtals( AtlasType.BuiltIn, obj => _loadCount-- );
				}
				return true;
			}
			public void Destroy()
			{
				SpriteAtlasManager.atlasRequested -= RequestAtlas;

				if( _coLoading != null )
				{
					AppMaster.StopCoroutine( _coLoading );
					_coLoading = null;
					_onProgress = null;
				}

				_loadCount = -1;
				_units.ForEach( it => Addressables.Release( it.Object ) );
				_units.Clear();
			}

			public void OnLoading( Action<bool, float> onProgress )
			{
				//!< Reloading ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
				if( _coLoading != null )
					AppMaster.StopCoroutine( _coLoading );

				_units.RemoveAll( it =>
				{
					if( it.Type == AtlasType.BuiltIn )
						return false;

					Addressables.Release( it.Object );
					return true;
				} );
				//!< ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

				_onProgress = onProgress;
				_coLoading = AppMaster.StartCoroutine( OnLoading() );
			}
			public void OnBreaking()
			{
				if( _coLoading != null )
				{
					AppMaster.StopCoroutine( _coLoading );
					_coLoading = null;
					_onProgress = null;
				}
			}

			private void AddAtlas( AtlasType type, SpriteAtlas obj )
			{
				if( obj == null )
					return;

				_units.Add( new Unit( type, obj ) );
				_onProgress?.Invoke( false, _units.Count / (float)AtlasType.Max );
				{
					Log.Output( $"Atlas Add : <color=yellow>{type}</color>" );
				}
			}
			private void LoadAtals( AtlasType type, Action<SpriteAtlas> onResult )
			{
				var key = $"{type.ToString()}.spriteatlas";
				{
					Log.Output( $"Atlas Loading Try : <color=white>{key}</color>" );
				}
				Addressables.LoadResourceLocationsAsync( key ).Completed += handle =>
				{
					if( 0 < handle.Result.Count )
					{
						Addressables.LoadAssetAsync<SpriteAtlas>( handle.Result[0] ).Completed += dataHandle =>
						{
							if( dataHandle.Status == AsyncOperationStatus.Succeeded )
							{
								Log.Output( $"Atlas load success :  <color=white>{key}</color>" );
								AddAtlas( type, dataHandle.Result );
								onResult?.Invoke( dataHandle.Result );
							}
							else
							{
								Log.Error( $"Atlas load failed : <color=magenta>{key}</color>" );
								onResult?.Invoke( null );
							}
						};
					}
					else
					{
						Log.Error( $"Atlas load failed : <color=magenta>{key}</color>" );
						onResult?.Invoke( null );
					}
					Addressables.Release( handle );
				};
			}
			private void RequestAtlas( string tag, Action<SpriteAtlas> onAction )
			{
				if( System.Enum.TryParse<AtlasType>( tag, out var type ) )
				{
					var atlas = _units.Find( it => it.Type == type );
					if( atlas != null )
					{
						onAction?.Invoke( atlas.Object );

						Log.Output( $"Atlas request "
							+ $": <color=magenta>{tag}</color>"
							+ $", <color=white>{atlas.Object}</color>"
							);
					}
					else
						LoadAtals( type, obj =>
						{
							onAction?.Invoke( obj );

							Log.Output( $"Atlas request "
								+ $": <color=magenta>{tag}</color>"
								+ $", <color=yellow>loaded obj( {obj} )</color>"
								);
						} );
				}
				else
				{
					Log.Error( $"Atlas request failed "
						+ $": <color=magenta>{tag}</color>"
						);
				}
			}
			private IEnumerator OnLoading()
			{
				var step = AtlasType.BuiltIn +1;
				{
					_onProgress?.Invoke( false, 0f );
				}
				for( var type = step; type < AtlasType.Max; ++type )
				{
					LoadAtals( type, obj => step++ );
					while( step == type )
						yield return null;
				}

				yield return null;
				_onProgress?.Invoke( true, 1f );
				_onProgress = null;
				{
					Log.Output( $"Atlas loaded count "
						+ $": <color=orange>{_units.Count}</color>"
						);
				}
			}

			public Sprite GetSprite( AtlasType type, string name )
			{
				if( string.IsNullOrEmpty( name ) )
					return null;

				var atlas = _units.Find( it => it.Type == type );
				if( atlas == null )
				{
					Log.Warning( $"Atlas isn't exists "
						+ $": Atlas(<color=magenta>{type}</color>)"
						+ $"'s <color=white>{name}</color>"
						);
					return null;
				}

				if( atlas.Sprites.TryGetValue( name, out var sprite ) == false )
					atlas.Sprites.Add( name, sprite = atlas.Object.GetSprite( name ) );

				if( sprite == null )
				{
					Log.Warning( "Sprite isn't exists "
						+ $": Atlas(<color=white>{type}</color>)"
						+ $"'s <color=magenta>{name}</color>"
						);
				}
				return sprite;
			}
		}
	}
}
