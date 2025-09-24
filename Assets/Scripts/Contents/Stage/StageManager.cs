namespace YunSun
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class StageManager 
        : Singleton<StageManager>
        , IManager
    {
        public bool Initialize()
		{
			return true;
		}
		public void Destroy()
		{
		}
    }
}

