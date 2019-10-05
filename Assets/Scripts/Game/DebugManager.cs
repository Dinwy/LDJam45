using System;
using System.Collections.Generic;
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

			Debug.Log("Setup Debug Manager");

			GiveMockDeck.onClick.AddListener(() =>
					{
						if (animating)
						{
							Debug.Log("Attacking");
							return;
						}

						Debug.Log("Attack the enemy");
					});

			Draw.onClick.AddListener(() => { });

			MoveNext.onClick.AddListener(() =>
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
					gameManager.Callback(GameState.MoveToRoomFinished);
				});

				// Trigger event
				gameManager.Callback(GameState.MoveToRoom);
			});
		}

		void OnFinished()
		{

		}

	}
}