using System;
using UnityEngine;
using Unity.VisualScripting;

namespace YunSun.UI
{
    public class StageUI : BaseUI
    {
        [SerializeField] Counter counter;

        private StageManager StageManager;

        void Start()
        {
            Init();
        }

        public void Init()
        {
            StageManager = StageManager.Instance;

            StageManager.Instance.SetCustomerPool( counter );
        }
    }
}