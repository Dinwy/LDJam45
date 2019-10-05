using System;
using System.Collections.Generic;

using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LDJam45.Game
{
	public class MapManager : MonoBehaviour, IManager
	{
		[Header("GameObjects")]
		public GameObject Map;

		[Header("Prefabs")]
		public GameObject RoomPrefab;
		private Vector3 roomPos = Vector3.zero;
		public float roomDistance { get; set; } = 10f;

		private List<RoomInfo> roomInfos = new List<RoomInfo>();
		private int currentPosition = 0;

		private GameManager gameManager;

		public void Setup(GameManager gm)
		{
			gameManager = gm;
			Debug.LogWarning("Setup");

			gm.OnStageChange += OnStageChange;

			if (gm.GameMode == GameMode.Campaign)
			{
				foreach (Transform child in Map.transform)
				{
					var room = child.GetComponent<RoomInfo>();
					if (room != null)
					{
						roomInfos.Add(child.GetComponent<RoomInfo>());
					}
				}
			}
		}

		public void CreateNewRoom()
		{
			roomPos = new Vector3(roomPos.x + 10f, roomPos.y, roomPos.z);
			var room = GameObject.Instantiate(RoomPrefab, roomPos, Quaternion.identity);
			room.transform.SetParent(Map.transform);
		}

		private void OnStageChange(object sender, GameState e)
		{
			switch (e)
			{
				case GameState.MoveToRoom:
					currentPosition++;

					if (gameManager.GameMode == GameMode.Infinite)
					{
						CreateNewRoom();
					}

					roomInfos[currentPosition].Setup();
					break;
				default:
					break;
			}

			return;
		}
	}
}