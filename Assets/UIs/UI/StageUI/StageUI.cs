namespace YunSun.UI
{
    using System.Collections.Generic;
    using UnityEngine;

    public partial class StageUI 
    {
    }
    public partial class StageUI : BaseUI
    {
        [SerializeField] List<Counter> counters;

        private StageManager StageManager;
		const int MaxCounter = 4;

        void Start()
        {
            StageManager = StageManager.Instance;

			for( int i = 0; i < MaxCounter; i++ )
			{
				var counter = counters[i];
				counter.Init();
				StageManager.Instance.InitCustomerPool( counter );
			}
        }
        public override void Initialize()
		{
			base.Initialize();
			{
				StageManager = StageManager.Instance;

                for( int i = 0; i < MaxCounter; i++ )
                {
                    var counter = counters[i];
					counter.Init();
                    StageManager.Instance.InitCustomerPool( counter );
                }
			}
		}
		public override void Localize()
		{
			base.Localize();
			{
			}
		}
		public override void Show()
		{
			base.Show();
			{
                
			}
		}
		public override void Hide( bool instant )
		{
			{
			}
			base.Hide( instant );
		}
		public override bool OnBack()
		{
			if( base.OnBack() )
				return true;

			return true;
		}
		public override bool OnRefresh( RefreshID id )
		{
			return base.OnRefresh( id );
		}
    }
}