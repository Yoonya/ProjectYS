namespace YunSun
{
    using UnityEngine;

    public partial class GameManager : IManager
    {
	    public bool Initialize()
		{
			InitializeData();
			return true;
		}
		public void Destroy()
		{
			DestroyData();
		}    
    }

    public partial class GameManager
    {
        private void InitializeData()
        {
            MainManager.Add( StageManager.Instance );
        }
        private void DestroyData()
        {
            MainManager.Remove<StageManager>();            
        }
    }
}

