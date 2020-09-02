using System;
using UnityEngine;

namespace UI
{
	public class UIPanel : MonoBehaviour
	{
		[HideInInspector] public PanelType panelType;
		public OverlayType[] usedOverlays;

		public virtual void OnLoad()
		{
			if (usedOverlays == null) return;
			foreach (OverlayType usedOverlay in usedOverlays)
			{
				UIManager.instance.SetOverlay(usedOverlay, true);
			}
		}

		public virtual void OnUnload()
		{
			if (usedOverlays == null) return;
			foreach (OverlayType usedOverlay in usedOverlays)
			{
				UIManager.instance.SetOverlay(usedOverlay, false);
			}
		}
	}
}