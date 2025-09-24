namespace YunSun
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class StageManager 
        : Singleton<StageManager>
        , IManager
    {
        private List<Tile> tiles;

        public bool Initialize()
		{
            tiles = new List<Tile>();
			return true;
		}
		public void Destroy()
		{
            tiles = null;
		}

        public void ApplyTiles( List<Tile> _tiles )
        {
            tiles.Clear();
            tiles.AddRange( _tiles );
        }
    }
}

