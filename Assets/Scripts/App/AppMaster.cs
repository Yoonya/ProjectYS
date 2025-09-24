namespace YunSun
{
	using System.Collections;
	using UnityEngine;

	static public partial class AppMaster
	{
		static public AppManager appMgr { get { return AppManager.Instance; } }

		static public Coroutine StartCoroutine( IEnumerator enumerator )
			=> appMgr.StartCoroutine( enumerator );
		static public void StopCoroutine( IEnumerator enumerator )
			=> appMgr.StopCoroutine( enumerator );
		static public void StopCoroutine( Coroutine coroutine )
			=> appMgr.StopCoroutine( coroutine );
	}
}
