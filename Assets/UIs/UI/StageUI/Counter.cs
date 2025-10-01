namespace YunSun
{
    using UnityEngine;
    using Doozy.Runtime.UIManager.Components;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UI;
    using Game.Character;

    public class Counter : MonoBehaviour
    {
        [SerializeField] UIButton Btn_Click;

        private List<Customer> customers;
        private float cooldownDuration = 1f; // customer duration 쿨타임 시간
        private bool isCooldown = false;

        public void Init()
        {
            this.SetActiveEx( true );
            Btn_Click.SetClickEvent( OnClick );

            customers = new List<Customer>();
        }
        public void AddCustomer( Customer customer )
        {
            customer.Apply();
            customer.SetParent( GetComponent<Transform>() );
            customer.SetOrder( customers.Count );
            customer.InitLocation( this );
            customer.MovetoLocation();
            customers.Add( customer );
        }
        public void OutCustomer()
        {
            var customer = customers.First();
            customers.Remove( customer );
            customers.Add( customer );

            for( int i = 0; i < customers.Count; i++ )
            {
                customers[i].SetOrder( i );
                customers[i].MovetoLocation();
            }
        }
        private void OnClick()
        {
            if( !isCooldown ) 
            {
                OutCustomer();
                AppMaster.StartCoroutine( CooldownCoroutine() );
            }           
        }
        private IEnumerator CooldownCoroutine()
        {
            isCooldown = true;             
            yield return new WaitForSeconds( cooldownDuration );
            isCooldown = false;
        }
    }
}