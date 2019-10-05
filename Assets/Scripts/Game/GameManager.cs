using System;

using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LDJam45.Game
{
	public class GameManager : MonoBehaviour
	{
		[Header("Camera")]
		public Camera MainCamera;
		public Camera SubCamera;

		[Header("GameObjects")]
		public GameObject PlayerPrefab;
		public GameObject DebugPanel;
		public Button MoveNext;

		[Header("Managers")]
		public MapManager MapManager;
		public DialogManager DialogManager;
		public event EventHandler<GameState> OnStageChange;

		private GameObject player;
		private Vector3 offset;

		private bool animating = false;

		void Start()
		{
			Debug.LogWarning("Start game");
			Setup();

			// Setup Managers
			MapManager.Setup(this);

			MoveNext.onClick.AddListener(() =>
			{
				if (animating)
				{
					Debug.LogWarning("don't move");

					return;
				}

				Debug.LogWarning("Move");
				animating = true;
				var seq = player.GetComponent<UnitManager>().MoveToNextRoom();
				seq.OnComplete(() =>
				{
					animating = false;
				});

				// Trigger event
				OnStageChange?.Invoke(this, GameState.MoveToOtherRoom);
			});

			DialogManager.UpdateDialog("Game has been started");
		}

		void Setup()
		{
			player = GameObject.Instantiate(PlayerPrefab, new Vector3(0, 1f, 0), Quaternion.identity);
			player.GetComponent<UnitManager>().Setup(this);
			offset = MainCamera.transform.position - player.transform.position;
		}

		void LateUpdate()
		{
			MainCamera.transform.position = player.transform.position + offset;
		}
	}
}