namespace YunSun
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;
    using YunSun.UI;

    public class StageManager 
        : Singleton<StageManager>
        , IManager
    {
        public Game.Pool customerPool;
        public int brand;
        public int hpRate;
        public int day;
        public int sales;

        private StageManager()
		{
		}

        public bool Initialize()
		{
            customerPool = new Game.Pool();
			return true;
		}
		public void Destroy()
		{
		}
        public void SetCustomerPool( Counter counter )
        {
            for( int i = 0; i < 10; i++ )
            {
                var customer = customerPool.GetCustomerPool();
                customer.SetParent( counter.GetComponent<Transform>() );
                customer.SetOrder( i );
                customer.SetLocation( counter );
                customer.MovetoLocation();
            }
        }
    }
}

