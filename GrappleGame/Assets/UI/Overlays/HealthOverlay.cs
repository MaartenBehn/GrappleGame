using UI.Panles;
using UnityEngine;

namespace UI.Overlays
{
	public class HealthOverlay : UIOverlay
	{
		public static HealthOverlay instance;
		
		private void Awake()
		{
			if (instance == null)
			{
				instance = this;
			}
			else if (instance != this)
			{
				Debug.Log("Instance already exists, destroying object!");
				Destroy(this);
			}

			overlayType = OverlayType.healthOverlay;
		}

		public override void OnLoad()
		{
			base.OnLoad();
		}

		public override void OnUnload()
		{
			base.OnUnload();
		}

		private void Update()
		{
			
		}
	}
}