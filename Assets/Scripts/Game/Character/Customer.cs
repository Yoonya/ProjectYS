namespace YunSun.Game.Character
{
    using System.Collections.Generic;
    using System.Data.Common;
    using UnityEngine;
    using System.Collections;

    public class Customer : MonoBehaviour
    {
        private RectTransform rect;
        private int id;
        private CustomerType customerType;
        private Counter counter; 
        private int orderNum;
        private Coroutine moveCoroutine;

        public bool isValid => id >= 0;
        public bool isSpecial => customerType != CustomerType.Normal;
        public bool isOrder => orderNum == 0;

        private const int MaxCuStomer_Line = 10; //GlobalTable

        private Customer()
        {
            Clean();
        }
        public void Clean()
        {
            id = -1;
            customerType = CustomerType.Normal;
            counter = null;
            orderNum = -1;
            moveCoroutine = null;
        }
        public void Apply( Customer customer )
        {
            if ( customer == null )
                return;

            this.rect = gameObject.GetComponent<RectTransform>();
            this.id = customer.id;
            this.customerType = customer.customerType;
        }
        public void ComesOrder()
        {
            orderNum--;
            if( orderNum < 0 )
                OutAnimation();
            else
                ComesAnimation();
        }
        public void SetOrder( int num )
        {   
            this.orderNum = num;
        }

        public void SetLocation( Counter counter )
        {
            this.counter = counter;

            float newX = counter.GetComponent<RectTransform>().anchoredPosition.x;
            float newY = ( orderNum + 1 ) * rect.rect.height + rect.rect.height * MaxCuStomer_Line;
            rect.anchoredPosition = new Vector2( newX, newY );
            rect.localScale = new Vector3( 1, 1, 1 ); 
        } 
        public void OnClick()
        {

        }
        private void ComesAnimation()
        {

        }
        private void OutAnimation()
        {

        }
        public void MovetoLocation()
        {
            if( moveCoroutine!= null )
                AppMaster.StopCoroutine( moveCoroutine );
           moveCoroutine = AppMaster.StartCoroutine( Move() );
        }

        private IEnumerator Move()
        {
            var start = rect.anchoredPosition;
            var target = new Vector2( start.x
            , counter.GetComponent<RectTransform>().anchoredPosition.y 
            + ( orderNum + 1 ) * rect.rect.height 
            );
            var time = 0f;
            var duration = 1f;
 
            while ( time < duration )
            {
                rect.anchoredPosition = Vector2.Lerp( start, target, time / duration );
                time += Time.deltaTime;
                yield return null;
            }
        }
    }         
}