namespace YunSun
{
	using System.Collections;
	using UnityEngine;

	public partial class AppManager : MonoBehaviour
	{
		static private AppManager _inst = null;
		static public AppManager Instance
		{
			get
			{
				if( _inst != null )
					return _inst;

				_inst = FindAnyObjectByType<AppManager>();
				if( _inst != null )
					return _inst;

				GameObject obj = new GameObject( "AppManager" );
				_inst = obj.AddComponent<AppManager>();
				return _inst;
			}
		}

		void Awake()
		{
			_inst = GetComponent<AppManager>();
			DontDestroyOnLoad( gameObject );
			Initialize();
		}
		
	}
	public partial class AppManager
	{
		public GameManager GameManager;

		private void Initialize()
		{
			GameManager = new GameManager();
			GameManager.Initialize();
			GameUI.Initialize();

			GameManager.GameStart();
		}
    }

	public partial class AppManager
	{
		static public System.Action _update = null;
		static public bool IsEmulator;
	}
}
