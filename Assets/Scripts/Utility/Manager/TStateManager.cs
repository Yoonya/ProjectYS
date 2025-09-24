namespace YunSun
{
	using System;
	using System.Collections.Generic;

	public interface IState
	{
		void Enter();
		void Execute();
		void Exit();
	}

	public class TStateManager<EState> where EState : struct, IComparable, IConvertible, IFormattable
	{
		private Dictionary<EState, IState> _dicState = new Dictionary<EState, IState>();
		public EState CurrentState { private set; get; }
		public IState CurrentObject { private set; get; }
		public EState PreviousState { private set; get; }
		public IState PreviousObject { private set; get; }

		public void Add( EState eState, IState oState ) => _dicState.Add( eState, oState );
		public void Remove( EState eState ) => _dicState.Remove( eState );
		public void Clear() => _dicState.Clear();

		public IState Find( EState eState )
		{
			if( true != _dicState.ContainsKey( eState ) )
				return null;

			return _dicState[eState];
		}
		public T Find<T>( EState eState ) where T : class, IState
		{
			return Find( eState ) as T;
		}

		public bool IsState( EState eState )
		{
			return CurrentState.Equals( eState );
		}
		public bool Change( EState eState, bool forced = false, Action onPreEnter = null )
		{
			if( forced != true )
			{
				if( IsState( eState ) )
				{
					return false;
				}
			}

			if( CurrentObject != null )
			{
				CurrentObject.Exit();
			}

			PreviousState = CurrentState;
			PreviousObject = CurrentObject;

			CurrentState = eState;
			CurrentObject = Find( eState );

			onPreEnter?.Invoke();

			if( CurrentObject != null )
			{
				CurrentObject.Enter();
			}
			return true;
		}
		public void Update()
		{
			if( CurrentObject != null )
				CurrentObject.Execute();
		}
		public void Break()
		{
			if( CurrentObject != null )
			{
				CurrentObject.Exit();
				CurrentObject = null;
			}
			if( PreviousObject != null )
			{
				PreviousObject = null;
			}
		}
	}
}
