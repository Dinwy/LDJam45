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
	public class SoundManager : MonoBehaviour
	{
		public GameManager GameManager;
		public AudioSource BattleTack;

		void Start()
		{
			RegisterEvnets();
		}

		private void OnStageChange(object sender, GameState gs)
		{
			switch (gs)
			{
				case GameState.BattleBegin:
					BattleTack.Play();
					BattleTack.volume = 0;
					BattleTack.DOFade(1, 1f);
					break;
				case GameState.PlayerTurnStart:
					break;
				case GameState.PlayerTurnEnd:
					break;
				case GameState.EnemyTurnStart:
					break;
				case GameState.EnemyTurnEnd:
					break;
				case GameState.BattleFinished:
					BattleTack.DOFade(0, 2f);
					break;
				case GameState.GameOver:
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

			var enemies = GameManager.MapManager.GetAllEnemies();

			foreach (var enemy in enemies)
			{
				enemy.GetComponent<UnitManager>().Draw();
				enemy.GetComponent<UnitManager>().UseCardToPlayer(GameManager.UserManager.PlayerUnitManager);
			}

			yield return new WaitForSeconds(1f);
			GameManager.Callback(GameState.EnemyTurnEnd);
		}

		private void RegisterEvnets()
		{
			GameManager.OnStageChange += OnStageChange;
		}
	}
}