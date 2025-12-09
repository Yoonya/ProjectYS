namespace YunSun
{
    using System.Collections.Generic;
    using System.Linq;
	using UnityEditor.Sprites;
	using UnityEngine;
	using UnityEngine.SocialPlatforms.Impl;
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

		public int Brands => brands;
		public int Rating => rating;
		public int Day => day;
		public int Sales => sales;

		private StageManager()
		{
			brands = 0;
			rating = 100;
			day = 0;
			sales = 0;
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
    }
}

