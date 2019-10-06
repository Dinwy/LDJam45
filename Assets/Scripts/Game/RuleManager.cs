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
						enemy.GetComponent<UnitManager>().Deck = new Stack<CardData>(gameManager.DebugManager.MockCardList_Lamia.CardList);
					}

					// Force changing state
					gameManager.Callback(GameState.PlayerTurnStart);
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
						gameManager.Callback(GameState.BattleFinished);
						return;
					}

					gameManager.Callback(GameState.EnemyTurnStart);
					break;
				case GameState.EnemyTurnStart:
					StartCoroutine(EnemyLogic());
					break;
				case GameState.EnemyTurnEnd:
					if (gameManager.UserManager.PlayerUnitManager.IsDead)
					{
						gameManager.Callback(GameState.GameOver);
					}
					else
					{
						gameManager.Callback(GameState.PlayerTurnStart);
					}
					break;
				case GameState.BattleFinished:
					Debug.Log("BattleFinished");

					if (gameManager.MapManager.IsLastRoom())
					{
						Debug.Log("Stage finished!");
						StartCoroutine(OnStageFinished());
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

			foreach (var enemy in enemies)
			{
				enemy.GetComponent<UnitManager>().Draw();
				enemy.GetComponent<UnitManager>().UseCard(gameManager.UserManager.PlayerUnitManager.ID,
				enemy.GetComponent<UnitManager>().Hands[0],
				() => gameManager.Callback(GameState.EnemyTurnEnd));
			}
		}

		private void RegisterEvnets()
		{
			gameManager.OnStageChange += OnStageChange;
		}
	}
}