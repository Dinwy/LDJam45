using System;
using System.Collections;
using System.Collections.Generic;

using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LDJam45.Game
{
	public class RuleManager : MonoBehaviour, IManager
	{
		[Header("Fields")]
		public GameObject PlayerField;
		public GameObject EnemyFields;

		public List<GameObject> PlayerUnits;
		public List<GameObject> EnemyUnits;
		private GameManager gameManager { get; set; }

		public void Setup(GameManager gm)
		{
			gameManager = gm;
			RegisterEvnets();
		}

		private void OnStageChange(object sender, GameState gs)
		{
			var enemies = gameManager.MapManager.GetAllEnemies();

			switch (gs)
			{
				case GameState.BattleBegin:
					foreach (var enemy in enemies)
					{
						enemy.transform.SetParent(EnemyFields.transform);
						enemy.GetComponent<UnitManager>().Deck = new Stack<CardData>(gameManager.DebugManager.MockCardList_Lamia.CardList);
					}

					// Force changing state
					gameManager.Callback(GameState.PlayerTurnStart);
					break;
				case GameState.PlayerTurnStart:
					Debug.LogWarning("Turnstart");
					Debug.Log($"{gameManager.UserManager.PlayerUnitManager.UserType}");
					gameManager.UserManager.PlayerUnitManager.Draw();
					break;
				case GameState.PlayerTurnEnd:
					gameManager.Callback(GameState.EnemyTurnStart);
					break;
				case GameState.EnemyTurnStart:
					StartCoroutine(EnemyLogic());
					break;
				case GameState.EnemyTurnEnd:
					gameManager.Callback(GameState.PlayerTurnStart);
					break;
				case GameState.BattleFinished:
					Debug.Log("BattleFinished");
					break;
				default:
					break;
			}
		}

		private IEnumerator EnemyLogic()
		{
			yield return new WaitForSeconds(1f);

			var enemies = gameManager.MapManager.GetAllEnemies();

			foreach (var enemy in enemies)
			{
				enemy.GetComponent<UnitManager>().Draw();
				enemy.GetComponent<UnitManager>().UseCardToPlayer(gameManager.UserManager.PlayerUnitManager);
			}

			yield return new WaitForSeconds(1f);
			gameManager.Callback(GameState.EnemyTurnEnd);
		}

		private void RegisterEvnets()
		{
			gameManager.OnStageChange += OnStageChange;
		}
	}
}