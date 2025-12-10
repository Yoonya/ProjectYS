namespace YunSun
{
	using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
	using UnityEngine;
	using YunSun.Game.Character;

	public class StageManager
		: Singleton<StageManager>
		, IManager
	{
		private Game.Pool customerPool;

		private int brands;
		private int rating;
		private int day;
		private int sales;
		private int close;
		private Coroutine closeTime;

		public int Brands => brands;
		public int Rating => rating;
		public int Day => day;
		public int Sales => sales;
		public int Close => close;

		private StageManager()
		{
			brands = 0;
			rating = 100;
			day = 0;
			sales = 0;
			close = 60;
			closeTime = null;
		}

		public bool Initialize()
		{
			customerPool = new Game.Pool();
			return true;
		}
		public void Destroy()
		{
			customerPool = null;
		}
		public void InitCustomerPool( Counter counter )
		{
			for( int i = 0; i < 10; i++ )
			{
				var customer = customerPool.GetCustomerPool();
				counter.AddCustomer( customer );
			}
		}
		public void SetScore( Customer customer )
		{
			sales += 100;
			int ran = Random.Range( -5, 5 );
			rating += ran;

			if( rating > 100 ) rating = 100;
			if( rating < 0 ) rating = 0;

			GameUI.OnRefresh( RefreshID.Stage );
		}
		public void StageStart()
		{
			close = 60;
			closeTime = AppMaster.StartCoroutine( CloseTime() );
		}
		public void StageEnd()
		{
			if( closeTime != null )
				AppMaster.StopCoroutine( closeTime );
			closeTime = null;
		}
		private IEnumerator CloseTime()
		{
			while( close > 0 )
			{
				yield return new WaitForSeconds( 1f );
				close--;
				GameUI.OnRefresh( RefreshID.Stage );
			}

			StageEnd();
			yield break;
		}
    }
}

