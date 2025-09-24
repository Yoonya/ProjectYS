namespace YunSun.UI
{
	using System;
	using UnityEngine;
	using Doozy.Runtime.UIManager.Containers;
	using Doozy.Runtime.UIManager.Animators;

	public partial class BaseUI : MonoBehaviour
	{
		public  GroupID                 GroupID   { get; private set; }
		public  RectTransform           RectTr    { get; private set; }
		private UIContainer             Container = null;
		private UIContainerUIAnimator   ContainerAnim = null;
		private bool                    IsHide = false;
		private bool                    IsStatic = false;

		private float                   ActiveTime = 0f;
		private float                   HiddenTime = 0f;

		[SerializeField] public bool    IsTopView = false;
		[SerializeField] public bool    IsMenuClose = false;

		public bool IsVisible           { get { return IsActive && !IsHide; } }
		public bool IsActive            { get { return this.gameObject.activeSelf; } }
		public bool IsActiveInHierarchy { get { return this.gameObject.activeInHierarchy; } }
		public bool CanRefresh          { get { return IsStatic || IsVisible; } }

		public virtual void Initialize()
		{
			this.name = name.Replace( "(Clone)", "" );
			this.GroupID = GroupID.Main;
			{
				if( transform.parent != null )
				{
					var groupUI = transform.parent.GetComponent<GroupUI>();
					if( groupUI != null )
						this.GroupID = groupUI.GroupID;
				}

				Log.Detail( "Initialize : {0}, {1}"
					, $"<color=white>{name}</color>"
					, $"<color=orange>{GroupID}</color>"
					);
			}
			this.RectTr = gameObject.GetComponent<RectTransform>();
			if( RectTr != null )
			{
				RectTr.anchoredPosition = Vector2.zero;
				RectTr.localScale = Vector3.one;
				RectTr.sizeDelta = Vector2.zero;
			}
			this.Container = GetComponentInChildren<UIContainer>();
			if( Container != null )
			{
				Container.OnHiddenCallback.Event.AddListener( () =>
				{
					this.IsHide = false;
					this.HiddenTime = Time.realtimeSinceStartup;
				} );
				Container.InstantHide();
			}
			this.ContainerAnim = GetComponentInChildren<UIContainerUIAnimator>();
		}
		public virtual void Localize()
		{
		}
		public virtual void Show()
		{
			this.IsHide = false;
			this.ActiveTime = Time.realtimeSinceStartup;
			this.HiddenTime = Time.realtimeSinceStartup;
			{
				//!< ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
				switch( GroupID )
				{
					case GroupID.Main:
					case GroupID.Popup:
						this.transform.SetAsLastSibling();
						break;

					case GroupID.Loading:
					default:
						break;
				}
				//!< ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

				Log.Detail( "Show : {0}, {1}, {2}"
					, $"<color=white>{name}</color>"
					, $"<color=orange>{GroupID}</color>"
					, $"Index( <color=yellow>{transform.GetSiblingIndex()}</color> )"
					);
			}

			if( IsTopView )
			{
				//GameUI.Show<MainTopUI>( GroupID.MainEx );
			}

			if( Container != null )
				Container.Show();
			else
				gameObject.SetActive( true );
		}
		public virtual void Hide( bool instant )
		{
			if( IsHide )
				return;

			this.IsHide = true;
			this.HiddenTime = Time.realtimeSinceStartup;
			{
				Log.Detail( "Hide : {0}, {1}, {2}"
					, $"<color=white>{name}</color>"
					, $"<color=orange>{GroupID}</color>"
					, $"Index( <color=yellow>{transform.GetSiblingIndex()}</color> )"
					);
			}

			if( Container != null )
			{
				if( false == instant
					&& null != ContainerAnim
					&& null != ContainerAnim.hideAnimation
					)
				{
					Container.Hide();
				}
				else
				{
					Container.InstantHide();
				}
			}
			else
			{
				gameObject.SetActive( false );
			}
		}

		public virtual bool OnBack()
		{
			//if( ActiveTime + AppConfig.UIActiveWaitTime < Time.realtimeSinceStartup )
			//	return false;

			return true;
		}
		public virtual bool OnRefresh( RefreshID id )
		{
			if( id == RefreshID.Tutorial_ResetUIs )
			{
				switch( GroupID )
				{
					case GroupID.Main:
					{
						Hide( false );
						return true;
					}
				}
			}
			return false;
		}

		public void SetStatic( bool value )
		{
			this.IsStatic = value;
		}
		public bool IsReleasable( float waitTime )
		{
			if( IsStatic )
				return false;
			if( IsActive )
				return false;
			if( Time.realtimeSinceStartup < HiddenTime + waitTime )
				return false;
			return true;
		}

		protected void ShowAnimation()
		{
			if( ContainerAnim != null )
				ContainerAnim.Show();
		}
	}
}
