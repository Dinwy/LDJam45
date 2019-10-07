using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
						var enemyUnit = enemy.GetComponent<UnitManager>();
						enemyUnit.Deck = new List<CardData>(gameManager.MonsterCardManager.GetCardList(enemyUnit.UnitData.Name).CardList);
					}

					// Force changing state
					gameManager.ChangeState(GameState.CardDrawPhase);
					break;
				case GameState.CardDrawPhase:
					gameManager.UserManager.PlayerUnitManager.InitialPhase();
					gameManager.ChangeState(GameState.PlayerTurnStart);
					break;
				case GameState.PlayerTurnStart:
					Debug.LogWarning("Turnstart");
					gameManager.UserManager.PlayerUnitManager.isInAction = false;
					Debug.Log($"{gameManager.UserManager.PlayerUnitManager.UserType}");
					gameManager.UserManager.PlayerUnitManager.Draw();
					break;
				case GameState.PlayerTurnEnd:
					int howManyDead = 0;

					foreach (var enemy in enemies)
					{
						if (enemy.GetComponent<UnitManager>().IsDead)
						{
							howManyDead++;
						}
					}

					if (howManyDead == enemies.Count)
					{
						gameManager.ChangeState(GameState.BattleFinished);
						return;
					}

					gameManager.ChangeState(GameState.EnemyTurnStart);
					break;
				case GameState.EnemyTurnStart:
					StartCoroutine(EnemyLogic());
					break;
				case GameState.EnemyTurnEnd:
					if (gameManager.UserManager.PlayerUnitManager.IsDead)
					{
						gameManager.ChangeState(GameState.GameOver);
					}
					else
					{
						gameManager.ChangeState(GameState.PlayerTurnStart);
					}
					break;
				case GameState.BattleFinished:
					Debug.Log("BattleFinished");

					if (gameManager.MapManager.IsLastRoom())
					{
						Debug.Log("Game finished!");
						gameManager.GameUIManager.ShowEndScene();
					}
					else
					{
						gameManager.ChangeState(GameState.RewardPhase);
					}
					break;
				case GameState.GameOver:
					Debug.LogWarning("GameOver");
					StartCoroutine(OnStageFinished());
					break;
				default:
					break;
			}
		}

		private IEnumerator OnStageFinished()
		{
			yield return new WaitForSeconds(2f);

			SceneManager.LoadSceneAsync(1);
		}

		private IEnumerator EnemyLogic()
		{
			yield return new WaitForSeconds(1f);

			var enemies = gameManager.MapManager.GetAllEnemies();
			enemyCount = enemies.Count;

			foreach (var enemy in enemies)
			{
				var enemyUnit = enemy.GetComponent<UnitManager>();
				if (enemyUnit.IsDead)
				{
					enemyCount -= 1;
					continue;
				}

				enemyUnit.Draw();
				Debug.LogWarning($"{enemyUnit.Hands[0].Name}: Drawn");
				var random = new System.Random();
				var idx = random.Next(enemyUnit.Deck.Count);
				var card = enemyUnit.Deck[idx];
				enemyUnit.UseCard(gameManager.UserManager.PlayerUnitManager.ID,
				card, enemyCallback);
			}
		}

		private int enemyCount = 0;
		private void enemyCallback()
		{
			enemyCount--;
			if (enemyCount == 0) gameManager.ChangeState(GameState.EnemyTurnEnd);
		}

		private void RegisterEvnets()
		{
			gameManager.OnStageChange += OnStageChange;
		}
	}
}