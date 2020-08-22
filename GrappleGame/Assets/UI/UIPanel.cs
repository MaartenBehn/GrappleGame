using UnityEngine;

namespace UI
{
	public class UIPanel : MonoBehaviour
	{
		[HideInInspector] public PanelType panelType;
		public virtual void OnLoad() { }
		public virtual void OnUnload() { }
	}
}