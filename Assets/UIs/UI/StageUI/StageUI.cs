namespace YunSun.UI
{
    using System.Collections.Generic;
	using TMPro;
	using UnityEditor.SceneManagement;
	using UnityEngine;
	using UnityEngine.UI;

	public partial class StageUI
	{
		private void RefreshUI()
		{
			Text_Day.SetTextEx( StageManager.Day.ToString() );
			Text_Sales.SetTextEx( StageManager.Sales.ToString() );
			Text_CloseTime.SetTextEx( StageManager.Close.ToString() );
			Img_Rating.SetFillAmount( StageManager.Rating / 100f );
		}
	}
    public partial class StageUI : BaseUI
    {
		[SerializeField] TMP_Text Text_Day;
		[SerializeField] TMP_Text Text_Sales;
		[SerializeField] TMP_Text Text_CloseTime;
		[SerializeField] Image Img_Rating;
        [SerializeField] List<Counter> counters;

        private StageManager StageManager;
		const int MaxCounter = 3;

        public override void Initialize()
		{
			base.Initialize();
			{
				StageManager = StageManager.Instance;
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
				for( int i = 0; i < MaxCounter; i++ )
				{
					var counter = counters[i];
					counter.Init();
					StageManager.InitCustomerPool( counter );
				}
				StageManager.StageStart();
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
			switch( id )
			{
				case RefreshID.Stage:
				{
					RefreshUI();
					return true;
				}
			}
			return base.OnRefresh( id );
		}
    }
}
