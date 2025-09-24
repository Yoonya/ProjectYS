namespace YunSun.UI
{
	using UnityEngine;
	using UnityEngine.SceneManagement;

	static public partial class Util
	{
		static private int getUILayer => LayerMask.NameToLayer( "UI" );

		static public void SetExcludeUILayer( Scene scene )
		{
			if( scene == null )
				return;

			int excludedLayerMask = 1 << getUILayer;
			int andMaskValue = ~excludedLayerMask;

			foreach( var go in scene.GetRootGameObjects() )
			{
				var its = go.GetComponentsInChildren<Light>( true );
				foreach( var it in its )
				{
					it.cullingMask &= andMaskValue;
				}
			}
		}

		static public void SetObjectAllUILayer( GameObject go )
			=> SetObjectAllUILayer( go?.transform ?? null );
		static public void SetObjectAllUILayer( Transform trans )
			=> ChangeLayers( trans, getUILayer );

		static private void ChangeLayers( Transform trans, int setLayer )
		{
			trans.gameObject.layer = setLayer;
			foreach( Transform child in trans )
			{
				ChangeLayers( child, setLayer );
			}
		}
	}
}
