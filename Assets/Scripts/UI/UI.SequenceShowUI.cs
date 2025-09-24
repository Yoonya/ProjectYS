namespace YunSun.UI
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class SequenceShowUI
	{
		public interface IUnit
		{
			void ShowUI( System.Action onHidden );
		}
		public class EmptyUnit : IUnit
		{
			public void ShowUI( System.Action onHidden )
				=> onHidden?.Invoke();
		}

		private readonly string logTag = string.Empty;
		private bool            isPlaying = false;
		private Queue<IUnit>    QueueUnits = new();
		private System.Action   onDone = null;

		public SequenceShowUI( string logTag )
		{
			this.logTag = $"<color=green>[{logTag}]</color>";
			this.onDone = null;
		}

		public void Active( IUnit unit, System.Action onDone = null )
		{
			if( unit == null )
				return;

			if( onDone != null )
				this.onDone += onDone;

			QueueUnits.Enqueue( unit );
			{
				UI.Log.Output( $"{logTag}"
					+ $" <color=orange>Add:</color>"
					+ $" <color=white>{unit}</color>"
					);
			}
			if( isPlaying )
				return;

			ShowUI();
		}
		public void Clear()
		{
			if( QueueUnits.Count > 0 )
			{
				UI.Log.Warning( $"{logTag}"
					+ $" <color=orange>Clear!!!</color>"
					);
			}
			QueueUnits.Clear();
			onDone = null;
		}

		private void ShowUI()
		{
			this.isPlaying = 0 < QueueUnits.Count;
			if( isPlaying )
			{
				var unit = QueueUnits.Dequeue();
				{
					UI.Log.Output( $"{logTag}"
						+ $" <color=orange>Show:</color>"
						+ $" <color=white>{unit}</color>"
						);

					IEnumerator _OnShow_() //!< #114333
					{
						yield return new WaitForSeconds( 0.36f );
						ShowUI();
					}
					unit.ShowUI( () => AppMaster.StartCoroutine( _OnShow_() ) );
				}
			}
			else
			{
				UI.Log.Output( $"{logTag}"
					+ $" <color=orange>Ending!!!</color>"
					);

				var done = onDone;
				{
					onDone = null;
					done?.Invoke();
				}
			}
		}
	}

	public class SequenceNoneUI : SequenceShowUI.IUnit
	{
		public void ShowUI( System.Action onHidden )
			=> onHidden?.Invoke();
	}
}

namespace YunSun
{
	using UI;
	using System;

	static public partial class GameUI
	{
		partial class UIManager
		{
			private SequenceShowUI SeqShowUI = new( "SeqShowUI" );

			public void ShowSeqUI( SequenceShowUI.IUnit unit, Action onDone )
				=> SeqShowUI.Active( unit, onDone );
			public void ClearSeqShowUI()
				=> SeqShowUI.Clear();
		}
	}
}
