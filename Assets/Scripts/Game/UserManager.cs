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

		public Guid ID { get; private set; }
		public GameObject PlayerUnit { get; private set; }
		public UnitManager PlayerUnitManager { get; set; }
		private GameManager gameManager { get; set; }

		private Vector3 position;

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
				case GameState.InitializeGame:
					PlayerUnit = GameObject.Instantiate(UnitPrefab, new Vector3(-3f, 1f, 0), Quaternion.identity);
					PlayerUnitManager = PlayerUnit.GetComponent<UnitManager>();

					// Set Guid;
					PlayerUnit.name = PlayerUnitManager.ID.ToString();
					PlayerUnitManager.Setup();
					PlayerUnitManager.UserType = UserType.Human;
					gameManager.ChangeState(GameState.InitializeFinished);
					break;
				case GameState.BattleBegin:
					position = PlayerUnit.transform.position;
					break;
				case GameState.PlayerTurnStart:
				case GameState.BattleFinished:
					PlayerUnit.transform.position = position;
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