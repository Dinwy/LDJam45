using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LDJam45.Game
{

	public class DialogManager : MonoBehaviour, IManager
	{
		public TextMeshProUGUI DialogText;

		private GameManager gameManager { get; set; }

		public void Setup(GameManager gm)
		{
			gameManager = gm;
			gm.OnStageChange += OnStageChange;
		}

		public void UpdateDialog(string text)
		{
			DialogText.text = text;
		}

		private void OnStageChange(object sender, GameState gameState)
		{
			switch (gameState)
			{
				case GameState.Initialize:
					UpdateDialog("Game has been started");
					break;
				case GameState.MoveToRoom:
					UpdateDialog("Moving to another room");
					break;
				case GameState.MoveToRoomFinished:
					UpdateDialog("Finished moving to another room");
					break;
				default:
					break;
			}
		}
	}
}
