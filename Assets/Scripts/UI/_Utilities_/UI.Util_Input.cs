namespace YunSun.UI
{
	using UnityEngine;
	using UnityEngine.EventSystems;

	static public partial class Util
	{
		static public bool IsClickDownEvent()
		{
			if( Input.GetMouseButtonDown( 0 ) )
			{
				return true;
			}

			if( 0 < Input.touchCount )
			{
				var touch = Input.GetTouch(0);
				if( touch.phase == TouchPhase.Began )
				{
					return true;
				}
			}

			return false;
		}
		static public bool IsClickUpEvent()
		{
			if( Input.GetMouseButtonUp( 0 ) )
			{
				return true;
			}

			if( 0 < Input.touchCount )
			{
				var touch = Input.GetTouch(0);
				if( touch.phase == TouchPhase.Ended )
				{
					return true;
				}
			}

			return false;
		}

		static public bool GetClickScreenPoint( out Vector2 point )
		{
			if( Input.GetMouseButtonUp( 0 ) &&
				EventSystem.current.IsPointerOverGameObject() )
			{
				point = Input.mousePosition;
				return true;
			}

			if( 0 < Input.touchCount )
			{
				var touch = Input.GetTouch(0);
				if( touch.phase == TouchPhase.Ended &&
					EventSystem.current.IsPointerOverGameObject( touch.fingerId ) )
				{
					point = touch.position;
					return true;
				}
			}

			point = Vector2.zero;
			return false;
		}
	}
}
