namespace YunSun
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UI;
	using UnityEngine.Pool;

	static public partial class GameUI
	{
		static public bool IsInitialized()
		{
			if( !UIManager.Instance.IsInit )
				return false;
			if( !Atlas.IsInitialized() )
				return false;
			return true;
		}
		static public void Initialize()
		{
			MainManager.Add( UIManager.Instance );
			Atlas.Initialize();
			//cache = null;
			//cacheHud = null;
		}
		static public void Destroy()
		{
			MainManager.Remove( typeof( UIManager ) );
			Atlas.Destroy();
			//cache = null;
			//cacheHud = null;
		}
		static public void Update()
			=> UIManager.Instance.Update();

		static public void SetUIScale( float scale )
			=> UIManager.Instance.SetUIScale( scale );

		static public bool Add<T>( GroupID gID, Action<T> callback = null ) where T : BaseUI
			=> UIManager.Instance.Add<T>( gID, callback );
		static public void Show<T>( GroupID gID, Action<T> callback = null ) where T : BaseUI
			=> UIManager.Instance.Show<T>( gID, callback );
		static public void ShowSeqUI( SequenceShowUI.IUnit unit, Action onDone = null )
			=> UIManager.Instance.ShowSeqUI( unit, onDone );
		static public void Hide<T>( bool instant = false ) where T : BaseUI
			=> Hide( typeof( T ).Name, instant );
		static public void Hide( string uiName, bool instant = false )
			=> UIManager.Instance.Hide( uiName, instant );
		static public void HideAll( GroupID gID, bool instant = false )
			=> UIManager.Instance.HideAll( gID, instant );
		static public void HideAll( bool instant = false )
			=> UIManager.Instance.HideAll( instant );
		static public void Release<T>( float waitTime, bool forced = false ) where T : BaseUI
			=> Release( typeof( T ).Name, waitTime, forced );
		static public void Release( string uiName, float waitTime, bool forced = false )
			=> UIManager.Instance.Release( uiName, waitTime, forced );
		static public void ReleaseAll( GroupID gID, float waitTime, bool forced = false )
			=> UIManager.Instance.ReleaseAll( gID, waitTime, forced );
		static public void ReleaseAll( float waitTime )
			=> UIManager.Instance.ReleaseAll( waitTime );
		static public UIxT GetUI<UIxT>() where UIxT : BaseUI
			=> UIManager.Instance.GetUI<UIxT>();
		static public BaseUI GetUI( string uiName )
			=> UIManager.Instance.GetUI( uiName );
		static public bool IsActive<UIxT>() where UIxT : BaseUI
			=> UIManager.Instance.GetUI<UIxT>()?.IsActive ?? false;
		static public bool OnBack()
			=> UIManager.Instance.OnBack();
		static public bool OnRefresh( RefreshID id )
			=> UIManager.Instance.OnRefresh( id );
		static public void OnRefresh()
			=> UIManager.Instance.OnRefresh();
		static public void OnLocalize()
			=> UIManager.Instance.OnLocalize();
		static public void OnActiveGroupUI( bool value, GroupID gID )
			=> UIManager.Instance.OnActiveGroupUI( value, gID );
		static public void OnExitGameScene()
			=> UIManager.Instance.OnExitGameScene();
		static public bool HasTopViewUI
			=> UIManager.Instance.HasTopViewUI;
	}

	static public partial class GameUI
	{
		partial class UIManager
			: Singleton<UIManager>
			, IManager
		{
			private GameObject      UIRoot = null;
			private List<GroupUI>   UIGroups = null;
			private List<BaseUI>    UIObjects = null;
			private Dictionary<string, BaseUI>
									HashTable = null;
			private bool            SortDirty = false;
			private bool            RefreshDirty = false;
			private int             LoadCount = -1;

			public bool IsInit => LoadCount == 0;

			private UIManager()
			{
#if !UNITY_EDITOR
				//!< ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
				Util.OnTMProDefaultFontSetting();
				Util.OnTMProDefaultSpriteSetting();
				Util.OnTMProDefaultStyleSetting();
				//!< ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
#endif

				var res = Resources.Load<GameObject>( "UI/Canvas/Canvas" );
				if( res == null )
					return;

				UIRoot = GameObject.Instantiate( res );
				{
					GameObject.DontDestroyOnLoad( UIRoot );
					UIRoot.name = "UIManager";
					UIGroups = new List<GroupUI>( UIRoot.GetComponentsInChildren<GroupUI>( true ) );
					UIObjects = new List<BaseUI>( 128 );
					HashTable = new( 128 );
				}
				SystemUtil.LoadEventSystem();
			}
			private GroupUI GetGroupUI( GroupID id )
				=> UIGroups.Find( it => it.GroupID == id );

			public bool Initialize()
			{
				/*
				var group = GetGroupUI( GroupID.Network );
				if( group != null )
				{
					//!< ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
					Action<BaseUI, int> onSetup = ( ui, index ) =>
					{
						LoadCount--;
						if( ui != null )
						{
							ui.SetStatic( true );
							ui.transform.SetSiblingIndex( index );
						}
					};
					//!< ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
					LoadCount = 3;
					{
						Add<WaitingUI>( GroupID.Network, ui => { WaitingUI.Inst = ui; onSetup( ui, 0 ); } );
						//!< ServerWaitingUI : SetSiblingIndex( 1 );
						Add<ConfirmUI>( GroupID.Network, ui => { ConfirmUI.Inst = ui; onSetup( ui, 1 ); } );
						Add<MessageUI>( GroupID.Network, ui => { MessageUI.Inst = ui; onSetup( ui, 2 ); } );
					}
				}
				*/
				return true;
			}
			public void Destroy()
			{
				UIObjects.ForEach( ui => Util.Release( ui ) );
				UIObjects.Clear();
				HashTable.Clear();
				ClearSeqShowUI();
			}
			public void Update()
			{
				if( RefreshDirty )
				{
					RefreshDirty = false;
					//ActiveUI.RefreshAll();
				}
			}

			public void SetUIScale( float scale )
			{
				UIGroups.ForEach( ui => ui.ChangeScale( scale ) );
			}

			public bool Add<T>( GroupID gID, Action<T> callback ) where T : BaseUI
			{
				//!< exists.
				var uiName = typeof( T ).Name;
				{
					if( HashTable.ContainsKey( uiName ) )
						return false;

					HashTable.Add( uiName, null );
				}

				//!< instance.
				var gUI = GetGroupUI( gID );
				if( gUI == null )
				{
					Log.Break( $"GroupUI isn't exists : {gID}" );
				}

				Util.Instantiate( uiName
					, gUI.transform
					, ( obj ) =>
					{
						var ui = obj.FindChildComponent<T>();
						if( ui == null )
						{
							Log.Error( $"UI is null : {uiName}" );
						}
						else
						{
							HashTable[uiName] = ui;
							{
								Log.Output( $"UI add in HashTable "
									+ $": <color=yellow>{uiName}</color>"
									+ $", Storage Count( <color=cyan>{HashTable.Count}</color> )"
									);
							}
							ui.name = uiName;
							ui.Initialize();
							ui.Localize();
							UIObjects.Add( ui );
							callback?.Invoke( ui );
						}
					}
					);
				return true;
			}
			public bool Show<T>( GroupID gID, Action<T> callback ) where T : BaseUI
			{
				var uiName = typeof( T ).Name;

				//!< exists.
				if( HashTable.TryGetValue( uiName, out var ui ) )
				{
					if( Show( ui as T, callback ) )
						return true;

					Log.Warning( $"UI is null in HashTable "
						+ $": <color=magenta>{uiName}</color>"
						);
					return false;
				}

				//!< instance.
				return Add<T>( gID, ui => Show( ui, callback ) );
			}
			private bool Show<T>( T ui, Action<T> callback ) where T : BaseUI
			{
				if( ui != null )
				{
					//!< ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
					//if( ui.IsTopView )
					//	Util.HideMainPopupUIs(); //!< #118448
					//!< ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
					if( ui.IsMenuClose )
						GameUI.OnRefresh( RefreshID.MenuClose );
					//!< ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

					if( ui.GroupID == GroupID.Loading )
					{
						UIObjects.ForEach( ui =>
						{
							switch( ui.GroupID )
							{
								case GroupID.Main:
								case GroupID.Loading:
									break;
								case GroupID.Popup:
								default:
									ui.Hide( true );
									break;
							}
						} );
					}
					//!< ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

					ui.Show();
					SortDirty = true;
					callback?.Invoke( ui );
					return true;
				}
				return false;
			}

			public void Hide( string uiName, bool instant )
			{
				if( HashTable.TryGetValue( uiName, out var ui ) )
				{
					if( ui != null )
					{
						ui.Hide( instant );
					}
					else
					{
						HashTable.Remove( uiName );
						{
							Log.Warning( $"Remove from HashTable "
								+ $"; <color=white>UI is null</color> "
								+ $": <color=magenta>{uiName}</color>"
								);
						}
					}
				}
			}
			public void HideAll( GroupID gID, bool instant )
			{
				var gUIs = UIObjects.FindAll( ui => ui.GroupID == gID );
				{
					gUIs.ForEach( ui => ui.Hide( instant ) );
				}
			}
			public void HideAll( bool instant )
			{
				UIObjects.ForEach( ui => ui.Hide( instant ) );
			}

			public void Release( string uiName, float waitTime, bool forced = false )
			{
				if( true != HashTable.TryGetValue( uiName, out var ui ) )
					return;

				if( ui == null )
					return;
				if( true != forced &&
					true != ui.IsReleasable( waitTime ) )
					return;

				Log.Detail( "Release : {0}, {1}, {2}, {3}"
					, $"<color=white>{ui.name}</color>"
					, $"<color=orange>{ui.GroupID}</color>"
					, $"<color=white>WaitTime( {waitTime} sec )</color>"
					, $"<color=white>Forced( {forced} )</color>"
					);
				UIObjects.Remove( ui );
				HashTable.Remove( ui.name );
				Util.Release( ui.gameObject );
			}
			public void ReleaseAll( GroupID gID, float waitTime, bool forced = false )
			{
				int count = UIObjects.RemoveAll( ui =>
				{
					if( ui.GroupID != gID )
						return false;
					return Release( ui, waitTime, forced );
				} );
				{
					Log.Output( "ReleaseAll : {0}, {1}, {2}, {3}"
						, $"<color=orange>{gID}</color>"
						, $"<color=yellow>Count( {count} )</color>"
						, $"<color=white>WaitTime( {waitTime} sec )</color>"
						, $"<color=white>Forced( {forced} )</color>"
						);
				}
			}
			public void ReleaseAll( float waitTime )
			{
				int count = UIObjects.RemoveAll( ui => Release( ui, waitTime, false ) );
				{
					Log.Output( "ReleaseAll : {0}, {1}"
						, $"<color=yellow>Count( {count} )</color>"
						, $"<color=white>WaitTime( {waitTime} sec )</color>"
						);
				}
			}
			private bool Release<T>( T ui, float waitTime, bool forced ) where T : BaseUI
			{
				if( null == ui )
					return false;
				if( true != forced &&
					true != ui.IsReleasable( waitTime ) )
					return false;

				//!< ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
				if( ui.IsActive() && forced )
					ui.Hide( true );
				//!< ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

				Log.Detail( "Release : {0}, {1}, {2}, {3}"
					, $"<color=white>{ui.name}</color>"
					, $"<color=orange>{ui.GroupID}</color>"
					, $"<color=white>WaitTime( {waitTime} sec )</color>"
					, $"<color=white>Forced( {forced} )</color>"
					);
				HashTable.Remove( ui.name );
				Util.Release( ui.gameObject );
				return true;
			}

			public UIxT GetUI<UIxT>() where UIxT : BaseUI
			{
				var uiName = typeof( UIxT ).Name;
				if( HashTable.TryGetValue( uiName, out var ui ) )
					return ui as UIxT;
				return null;
			}
			public BaseUI GetUI( string uiName )
			{
				if( HashTable.TryGetValue( uiName, out var ui ) )
					return ui;
				return null;
			}

			public bool OnBack()
			{
				if( SortDirty )
				{
					System.Comparison<BaseUI> Compare = ( lhs, rhs ) =>
					{
						if( lhs.GroupID < rhs.GroupID ) return +1;
						if( lhs.GroupID > rhs.GroupID ) return -1;
						if( lhs.transform.GetSiblingIndex() < rhs.transform.GetSiblingIndex() ) return +1;
						if( lhs.transform.GetSiblingIndex() > rhs.transform.GetSiblingIndex() ) return -1;
						return 0;
					};

					SortDirty = false;
					UIObjects.Sort( Compare );
				}

				foreach( var ui in UIObjects )
				{
					if( ui != null
						&& ui.IsActiveInHierarchy
						&& ui.OnBack()
						)
					{
						Log.Detail( "OnBack process : {0}"
							, $"<color=white>{ui.name}</color>"
							);
						return true;
					}
				}
				return false;
			}
			public bool OnRefresh( RefreshID id )
			{
				int count = 0;
				{
					foreach( var ui in UIObjects )
					{
						if( ui != null
							&& ui.CanRefresh
							&& ui.OnRefresh( id ) )
						{
							count++;
							Log.Output( $"Refresh process "
								+ $": <color=yellow>{id}</color>"
								+ $", <color=white>{ui.name}</color>"
								);
						}
					}

					RefreshDirty = true;
				}
				return 0 < count;
			}
			public void OnRefresh() => RefreshDirty = true;
			public void OnLocalize()
			{
				UIObjects.ForEach( ui => ui?.Localize() );
			}
			public void OnActiveGroupUI( bool value, GroupID gID )
			{
				var gUI = UIGroups.Find( ui => ui.GroupID == gID );
				if( gUI != null )
					gUI.Active( value );
			}
			public void OnExitGameScene()
			{
				ClearSeqShowUI(); //!< #123614
			}

			public bool HasTopViewUI
				=> null != UIObjects.Find( it => it.IsActive() && it.IsTopView );
		}
	}
/*
	public partial class GameUI
	{
		static MainGameUI cache = null;
		static public MainGameUI MainGameUI
		{
			get
			{
				if( cache == null )
				{
					cache = GameUI.GetUI<MainGameUI>();
				}
				return cache;
			}
		}

		static MainHudUI cacheHud = null;
		static public MainHudUI MainHudUI
		{
			get
			{
				if( cacheHud == null )
				{
					cacheHud = GameUI.GetUI<MainHudUI>();
				}
				return cacheHud;
			}
		}
	}
*/
}
