namespace YunSun
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class StageManager 
        : Singleton<StageManager>
        , IManager
    {
        public Game.Pool pool;

        private StageManager()
		{
		}

        public bool Initialize()
		{
            pool = new Game.Pool();
			return true;
		}
		public void Destroy()
		{
		}
    }
}

