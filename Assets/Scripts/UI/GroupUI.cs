namespace YunSun.UI
{
	using UnityEngine;
	using UnityEngine.UI;

	public enum GroupID
	{
		Main,           //!< Depth : 10
		Popup,          //!< Depth : 100
		Loading,        //!< Depth : 1000
	}

	public partial class GroupUI : MonoBehaviour
	{
		[SerializeField] GroupID            groupID;
		[SerializeField] Canvas             canvas;
		[SerializeField] GraphicRaycaster   caster;

		public GroupID GroupID { get { return groupID; } }

		public void Active( bool value )
		{
			if( canvas == null )
				canvas = GetComponent<Canvas>();
			if( canvas != null )
				canvas.enabled = value;

			if( caster == null )
				caster = GetComponent<GraphicRaycaster>();
			if( caster != null )
				caster.enabled = value;

			var casters = GetComponentsInChildren<GraphicRaycaster>( true );
			if( casters != null )
			{
				foreach( var it in casters )
				{
					it.enabled = value;
				}
			}
		}
		public override string ToString()
		{
			return $"GroupUI ({GroupID})";
		}

		static public GroupUI Find( GroupID gID )
		{
			foreach( var ui in FindObjectsOfType<GroupUI>( true ) )
			{
				if( ui.GroupID == gID )
					return ui;
			}
			return null;
		}
	}

	public partial class GroupUI
	{
		[Header("UIScale( #2692 )")]
		[SerializeField] bool               isNonScale;
		[SerializeField] CanvasScaler       canvasScaler;
		[SerializeField] Vector2            refResolution;

		public bool ChangeScale( float scale )
		{
			if( isNonScale )
				return true;

			if( canvasScaler == null )
			{
				canvasScaler = this.GetComponent<CanvasScaler>();
				if( canvasScaler != null )
					refResolution = canvasScaler.referenceResolution;
				if( canvasScaler == null )
					return false;
				if( canvasScaler.uiScaleMode != CanvasScaler.ScaleMode.ScaleWithScreenSize )
					return false;
			}

			var w = refResolution.x / scale;
			var h = refResolution.y / scale;
			canvasScaler.referenceResolution = new( w, h );
			return true;
		}
	}
}