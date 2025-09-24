namespace YunSun.UI
{
	using System;
	using UnityEngine;
	using UnityEngine.Pool;

	public class UIPool<T> where T : MonoBehaviour
	{
		public readonly GameObject      group = null;
		public readonly string          uiName = null;
		public readonly ObjectPool<T>   uiPool = null;
		public readonly Action<T>       onInit = null;
		public readonly GameObject      goPool = null;

		public UIPool( GameObject group, Action<T> onInit )
		{
			this.group = group;
			this.uiName = typeof( T ).Name;
			this.uiPool = new ObjectPool<T>( CreateObject, OnGet, OnRelease, OnDestroy );
			this.onInit = onInit;
			this.goPool = new GameObject( $"<< Pool >> {this.uiName}" );
			this.goPool.SetParent( group );
			this.goPool.transform.localScale = Vector3.one; //!< #3659
			this.goPool.transform.SetAsFirstSibling();
		}

		private T CreateObject()
		{
			T ui = null;
			{
				if( Util.Instantiate( uiName, goPool.transform, out var obj ) )
				{
					if( obj != null )
					{
						ui = obj.GetComponent<T>();
						if( ui == null )
							ui = obj.AddComponent<T>();
						if( ui != null )
						{
							ui.name = uiName;
							onInit?.Invoke( ui );
						}
					}
				}
			}
			return ui;
		}
		private void OnGet( T ui )
		{
			ui.SetParent( group );
			ui.SetActiveEx( true );
		}
		private void OnRelease( T ui )
		{
			ui.SetActiveEx( false );
			ui.SetParent( goPool );
		}
		private void OnDestroy( T ui )
			=> Util.Release( ui );

		public T Get()
			=> uiPool.Get();
		public void Release( T ui )
			=> uiPool.Release( ui );
		public void Clear()
			=> uiPool.Clear();
		public void Dispose()
		{
			uiPool.Clear();
			uiPool.Dispose();

			GameObject.Destroy( goPool );
		}

		public override string ToString()
		{
			return $"UIPool: {uiName}, Count( {uiPool.CountAll} )";
		}
	}
}
