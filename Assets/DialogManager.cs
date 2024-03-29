﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

namespace LDJam45.Game
{
	public class DialogManager : MonoBehaviour, IManager
	{
		public GameObject CenterPanel;
		public TextMeshProUGUI DialogText;
		public Button MoveNextRoom;

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
				case GameState.InitializeGame:
					UpdateDialog("Game has been started");
					break;
				case GameState.MoveToRoom:
					UpdateDialog("Moving to another room");
					break;
				case GameState.MoveToRoomFinished:
					UpdateDialog("Finished moving to another room");
					gameManager.CheckRoom();
					break;
				case GameState.BattleBegin:
					UpdateDialog($"Enemy exsits in the room! Battle begins\nDrag and drop your card to the enemy!");
					break;
				case GameState.BattleFinished:
					UpdateDialog("Enemy exsits in the room! Battle Finished!");
					break;
				case GameState.GameOver:
					CenterPanel.SetActive(true);
					DOTween.To(() => CenterPanel.GetComponent<CanvasGroup>().alpha, x => CenterPanel.GetComponent<CanvasGroup>().alpha = x, 1, 1f);
					break;
				default:
					break;
			}
		}
	}
}
