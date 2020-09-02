using UnityEngine;

namespace UI
{
	public class UIOverlay : MonoBehaviour
	{
		[HideInInspector] public OverlayType overlayType;
		public virtual void OnLoad() { }
		public virtual void OnUnload() { }
	}
}