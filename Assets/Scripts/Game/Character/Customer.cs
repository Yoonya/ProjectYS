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

        private Coroutine Cor_MoveComes;
        private Coroutine Cor_MoveOut;


        public bool isValid => id >= 0;
        public bool isSpecial => customerType != CustomerType.Normal;
        public bool isOrder => orderNum == 0;

        private const int MaxCuStomer_Line = 10; //GlobalTable
        private const float Move_Time = 1f;
        private const float MoveSide_Time = 0.5f;

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
            Cor_MoveComes = null;
            Cor_MoveOut = null;
        }
        public void Apply()
        {
            this.rect = gameObject.GetComponent<RectTransform>();
            this.id = 0;
            this.customerType = CustomerType.Normal;
        }
        public void SetOrder( int num )
        {   
            this.orderNum = num;

            if( orderNum == 10 - 1 ) // 후에 globaltable에 max연결
                MovetoOut();
            else
                MovetoComes();
        }

        public void InitLocation( Counter counter, int num )
        {
            this.counter = counter;
            this.orderNum = num;
            var counterRect = counter.GetComponent<RectTransform>();

            float newX = 0;
            float newY = counterRect.anchoredPosition.y //Start에서 시작해서 그런가? 값이 바뀜
            + ( counterRect.rect.height / 2 ) 
            + ( orderNum + 1 ) * rect.rect.height
            + rect.rect.height * MaxCuStomer_Line;

            rect.anchoredPosition = new Vector2( newX, newY );
            rect.localScale = new Vector3( 1, 1, 1 ); 

            MovetoComes();
        }
        private void MovetoComes()
        {
			if( Cor_MoveOut != null ) //rect.anchoredPosition 사용 겹침
				return;
            if( Cor_MoveComes != null )
				AppMaster.StopCoroutine( Cor_MoveComes );
            Cor_MoveComes = AppMaster.StartCoroutine( MoveComes() );
        }
        private void MovetoOut()
        {
            if( Cor_MoveOut != null )
                AppMaster.StopCoroutine( Cor_MoveOut );
            Cor_MoveOut = AppMaster.StartCoroutine( MoveOut() );
        }
        private IEnumerator MoveComes()
        {
            var start = rect.anchoredPosition;
            var target = GetTargetLocation( start.x );

            var time = 0f;
			while( time < Move_Time )
			{
				rect.anchoredPosition = Vector2.Lerp( start, target, time / Move_Time );
				time += Time.deltaTime;
				yield return null;
			}
        }
		private IEnumerator MoveOut()
		{
			IEnumerator MoveLerp( Vector2 start, Vector2 target, float duration )
			{
				var time = 0f;
				while( time < duration )
				{
					rect.anchoredPosition = Vector2.Lerp( start, target, time / duration );
					time += Time.deltaTime;
					yield return null;
				}
			}

			var InitLocation = rect.anchoredPosition;
			yield return MoveLerp( InitLocation, new Vector2( InitLocation.x + rect.rect.width, InitLocation.y ), MoveSide_Time ); //move right
			yield return MoveLerp( rect.anchoredPosition, GetTargetLocation( InitLocation.x + rect.rect.width ), Move_Time ); //move finalLine
			yield return MoveLerp( rect.anchoredPosition, new Vector2( InitLocation.x, rect.anchoredPosition.y ), MoveSide_Time ); //move left

			Cor_MoveOut = null;
		}
        private Vector2 GetTargetLocation( float startX )
        {
            var counterRect = counter.GetComponent<RectTransform>();

            return new Vector2( startX
            ,  counterRect.anchoredPosition.y 
            + ( counterRect.rect.height / 2 )
            + ( orderNum + 1 ) * rect.rect.height 
            );
        }
    }         
}
