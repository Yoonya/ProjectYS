namespace YunSun.Game.Character
{
    using UnityEngine;
	using UnityEngine.UI;
    using System.Collections;
    using UnityEditor.ShaderGraph;
	using System.Collections.Generic;
	using Microsoft.Unity.VisualStudio.Editor;
	using YunSun.UI;
	using System;

	public enum MenuIngredient
	{
		//후에 GlobalEnum으로, 메뉴 재료 목록들
		None,
		Meat,
		Egg,
		Bread,
		Rice,
		Vegetable,
	}
	public enum CustomerSign
	{
		//후에 GlobalEnum으로, 말풍선에 기호 종류들
		Normal,
		Like,
		DisLike,
	}
	public class Customer : MonoBehaviour
	{
		[Serializable]
		private class Unit
		{
			[SerializeField] UnityEngine.UI.Image img;
			private MenuIngredient ingredient;
			private CustomerSign sign;
			public void SetImage( MenuIngredient ingredient, CustomerSign sign )
			{
				this.ingredient = ingredient;
				this.sign = sign;

				img.SetSprite( AtlasType.MainUI, ingredient.ToString() );

				if( sign == CustomerSign.Normal )
				{
					img.color = Color.black;
				}
				else if( sign == CustomerSign.Like )
				{
					img.color = Color.blue;
				}
				else if( sign == CustomerSign.DisLike )
				{
					img.color = Color.red;
				}
			}
		}

		private RectTransform rect;
		private int uid;
		//후에 CustomerTableData 추가
		private CustomerType customerType;
		private Counter counter;
		private int orderNum;
		[SerializeField] List<Unit> units;

		private Coroutine Cor_MoveComes;
		private Coroutine Cor_MoveOut;

		public bool isValid => uid >= 0;
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
			uid = -1;
			customerType = CustomerType.Normal;
			units = new List<Unit>();
			counter = null;
			orderNum = -1;
			Cor_MoveComes = null;
			Cor_MoveOut = null;
		}
		public void Apply()
		{
			this.rect = gameObject.GetComponent<RectTransform>();
			this.uid = 0;
			this.customerType = CustomerType.Normal;
			//temp
			units[0].SetImage( MenuIngredient.Meat, CustomerSign.Normal );
			units[1].SetImage( MenuIngredient.Egg, CustomerSign.Like );
			units[2].SetImage( MenuIngredient.Bread, CustomerSign.DisLike );
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

			gameObject.transform.SetSiblingIndex( 0 ); // 순서대로 가장 뒤로 말풍선 겹침
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
			rect.anchoredPosition = target;
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
				rect.anchoredPosition = target;
			}

			var InitLocation = rect.anchoredPosition;
			yield return MoveLerp( InitLocation, new Vector2( InitLocation.x + rect.rect.width, InitLocation.y ), MoveSide_Time ); //move right
			gameObject.transform.SetSiblingIndex( 0 ); // 오른쪽으로 빠진 후에 하이라키 조정(오른쪽으로 빠질 때는 뒷사람을 가리게 안겹치게 뒤로 나갈때는 말풍선 아래로 오도록)
			yield return MoveLerp( rect.anchoredPosition, GetTargetLocation( InitLocation.x + rect.rect.width ), Move_Time ); //move finalLine
			yield return MoveLerp( rect.anchoredPosition, new Vector2( InitLocation.x, rect.anchoredPosition.y ), MoveSide_Time ); //move left

			Cor_MoveOut = null;
		}
		private Vector2 GetTargetLocation( float startX )
		{
			var counterRect = counter.GetComponent<RectTransform>();

			return new Vector2( startX
			, counterRect.anchoredPosition.y
			+ (counterRect.rect.height / 2)
			+ (orderNum + 1) * rect.rect.height
			);
		}
	}         
}
