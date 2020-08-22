using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace UI
{
	public class SettingsPanel : UIPanel
	{
		public static SettingsPanel instance;
		
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

			panelType = PanelType.settingsPanel;

			
			fullScreenModes = new List<FullScreenMode>
			{
				FullScreenMode.FullScreenWindow,
				FullScreenMode.Windowed
			};

			resolutions = new List<Resolution>();
			foreach (Resolution resolution in Screen.resolutions) // Filtering out double resolutions in Screen.resolutions
			{
				if (resolutions.TrueForAll(testResolution => testResolution.height != resolution.height))
				{
					resolutions.Add(resolution);
				}
			}
			
			foreach (string t in resolutions.Select(resolution => resolution.width + " x " + resolution.height))
			{
				resolutionDropdown.options.Add (new TMP_Dropdown.OptionData() {text = t});
			}
		}

		[SerializeField] private TMP_Dropdown fullscreenModeDropdown;
		[SerializeField] private TMP_Dropdown resolutionDropdown;

		private List<FullScreenMode> fullScreenModes;
		private List<Resolution> resolutions;
		public Resolution currentResolution;

		public override void OnLoad()
		{
			fullscreenModeDropdown.value = fullScreenModes.IndexOf(Screen.fullScreenMode);
			
			for (int i = 0; i < resolutions.Count; i++)
			{
				if (resolutions[i].height != Screen.height) continue;
				currentResolution = resolutions[i];
				resolutionDropdown.value = i;
			}
		}

		public override void OnUnload()
		{
			currentResolution = resolutions[resolutionDropdown.value];
			Screen.SetResolution(currentResolution.width, currentResolution.height, fullScreenModes[fullscreenModeDropdown.value]);
		}
	}
}