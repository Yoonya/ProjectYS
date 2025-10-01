namespace YunSun.Game.Character
{
    using UnityEngine;
    using System.Collections;
    using UnityEditor.ShaderGraph;

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
        public void Apply()
        {
            this.rect = gameObject.GetComponent<RectTransform>();
            this.id = 0;
            this.customerType = CustomerType.Normal;
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

        public void InitLocation( Counter counter )
        {
            this.counter = counter;
            var counterRect = counter.GetComponent<RectTransform>();

            float newX = 0;
            float newY = counterRect.anchoredPosition.y //Start에서 시작해서 그런가? 값이 바뀜
            + ( counterRect.rect.height / 2 ) 
            + ( orderNum + 1 ) * rect.rect.height
            + rect.rect.height * MaxCuStomer_Line;

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
            var counterRect = counter.GetComponent<RectTransform>();

            var start = rect.anchoredPosition;
            var target = new Vector2( start.x
            ,  counterRect.anchoredPosition.y 
            + ( counterRect.rect.height / 2 )
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