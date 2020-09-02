using System;
using System.Collections.Generic;
using SharedFiles.Lobby;
using SharedFiles.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panles
{
	[Serializable]
	struct GameModeVoteImage
	{
		public Image image;
		public GameModeType gameModeType;
	}
	public class VotePanel : UIPanel
	{
		public static VotePanel instance;
		
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

			panelType = PanelType.votePanel;

			voteLobbyButtons = new List<Button>();
			foreach (GameObject lobbyPreFab in GameManager.instance.lobbyPreFabList)
			{
				LobbyData lobbyData = lobbyPreFab.GetComponent<LobbyData>();
				Button voteButton = Instantiate(voteButtonPreFab, voteLobbyContent.transform).GetComponent<Button>();
				voteButton.image = lobbyData.preViewImage;
				voteLobbyButtons.Add(voteButton);
			}
			
			voteGameModeButtons = new List<Button>();
			foreach (GameModeVoteImage gameModeVoteImage in voteGameModeImages)
			{
				Button voteButton = Instantiate(voteButtonPreFab, voteGameModeContent.transform).GetComponent<Button>();
				voteButton.image = gameModeVoteImage.image;
				voteGameModeButtons.Add(voteButton);
			}
		}

		[SerializeField] private GameObject voteButtonPreFab;
		[SerializeField] private GameObject voteLobbyContent;
		private List<Button> voteLobbyButtons;
		[SerializeField] List<GameModeVoteImage> voteGameModeImages;
		[SerializeField] private GameObject voteGameModeContent;
		private List<Button> voteGameModeButtons;

		public override void OnLoad()
		{
			Cursor.lockState = CursorLockMode.None;
			base.OnLoad();
		}
	}
}