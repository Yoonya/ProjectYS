namespace YunSun.UI
{
    public class StageUI : BaseUI
    {
        private StageManager StageManager;

        void Start()
        {
            StageManager = StageManager.Instance;

            StageManager.Instance.pool.GetCustomerPool();
        }
    }
}