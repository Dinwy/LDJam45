using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace LDJam45.Game
{
	public class UserManager : MonoBehaviour, IUser, IManager
	{
		[Header("GameObjects")]
		public GameObject UnitPrefab;
		public UserType UserType;

		public Guid ID { get; private set; }
		public GameObject PlayerUnit { get; private set; }
		public UnitManager PlayerUnitManager { get; set; }
		private GameManager gameManager { get; set; }

		public UserManager()
		{
			ID = Guid.NewGuid();
		}

		public void Setup(GameManager gm)
		{
			gameManager = gm;
			RegisterEvnets();
		}

		public Sequence MoveToNextRoom()
		{
			var seq = DOTween.Sequence();
			seq.Append(PlayerUnit.transform.DOMoveX(PlayerUnit.transform.position.x + gameManager.MapManager.roomDistance, 2f));
			return seq;
		}

		private void OnStageChange(object sender, GameState gs)
		{
			switch (gs)
			{
				case GameState.Initialize:
					PlayerUnit = GameObject.Instantiate(UnitPrefab, new Vector3(-3f, 1f, 0), Quaternion.identity);
					PlayerUnitManager = PlayerUnit.GetComponent<UnitManager>();
					PlayerUnitManager.Setup();
					gameManager.Callback(GameState.InitializeFinished);
					break;
				default:
					break;
			}
		}

		private void RegisterEvnets()
		{
			gameManager.OnStageChange += OnStageChange;
		}
	}
}