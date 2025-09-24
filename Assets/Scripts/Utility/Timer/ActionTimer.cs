namespace YunSun
{
	using System;
	using UnityEngine;

	public class ActionTimer
	{
		private float   _delayTime = 300;
		private float   _delayTimer = 0;
		private Action  _onAction = null;
		private bool    _isRepeat = false;

		public bool IsActive => _delayTimer > 0;
		public bool IsRepeat => _isRepeat;
		public bool IsExpire => _delayTimer <= Time.realtimeSinceStartup;

		public void Active( float delayTime, Action action, bool isRepeat = false )
		{
			SetupTimer( delayTime, action, isRepeat );
			ResetTimer();
		}
		public void Remove()
		{
			_delayTime = 0;
			_delayTimer = 0;
			_onAction = null;
			_isRepeat = false;
		}
		public void Update()
		{
			if( IsActive && IsExpire )
			{
				BreakTimer();

				_onAction?.Invoke();
				{
					if( IsActive )
						return;
				}

				if( IsRepeat )
				{
					ResetTimer();
				}
				else
				{
					Remove();
				}
			}
		}

		public void SetupTimer( float delayTime, Action onAction, bool isRepeat = false )
		{
			_delayTime = delayTime;
			_delayTimer = 0;
			_onAction = onAction;
			_isRepeat = isRepeat;
		}
		public void ResetTimer() => _delayTimer = Time.realtimeSinceStartup + _delayTime;
		public void BreakTimer() => _delayTimer = 0;
	}
}
