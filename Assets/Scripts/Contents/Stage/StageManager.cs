namespace YunSun
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using YunSun.Game.Character;

    public class StageManager 
        : Singleton<StageManager>
        , IManager
    {
        private Game.Pool customerPool;

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
    }
}

