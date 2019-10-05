using System;

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

		public void Setup(GameManager gm)
		{
			Debug.LogWarning("Setup");
			gm.OnStageChange += OnStageChange;
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
				case GameState.MoveToOtherRoom:
					CreateNewRoom();
					break;
				default:
					break;
			}

			return;
		}
	}
}