using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

namespace LDJam45.Game
{
	public class DebugManager : MonoBehaviour, IManager
	{
		public GameObject DebugPanel;
		public Button MoveNext;
		public Button GiveMockDeck;
		public Button Draw;

		private GameManager gameManager { get; set; }
		private bool animating = false;

		public void Setup(GameManager gm)
		{
			gameManager = gm;

			// Setup debug functions
			GiveMockDeck.onClick.AddListener(GiveMockDeckToUser);
			MoveNext.onClick.AddListener(MoveToNextRoom);
			Draw.onClick.AddListener(gameManager.UserManager.PlayerUnitManager.Draw);

			Debug.Log("[Setup] Debug Manager");
		}

		private void MoveToNextRoom()
		{
			if (animating)
			{
				Debug.LogWarning("don't move");

				return;
			}

			Debug.LogWarning("Move");
			animating = true;

			var seq = gameManager.UserManager.MoveToNextRoom();
			seq.OnComplete(() =>
			{
				animating = false;
				gameManager.ChangeState(GameState.MoveToRoomFinished);
			});

			// Trigger event
			gameManager.ChangeState(GameState.MoveToRoom);
		}

		private void GiveMockDeckToUser()
		{
			gameManager.UserManager.PlayerUnitManager.Deck = new Stack<CardData>(gameManager.MonsterCardManager.GetCardList("Boy").CardList);
		}
	}
}