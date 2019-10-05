using System;
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
			switch (gs)
			{
				case GameState.BattleBegin:
					var enemies = gameManager.MapManager.GetAllEnemies();
					foreach (var enemy in enemies)
					{
						enemy.transform.SetParent(EnemyFields.transform);
					}
					break;
				case GameState.BattleFinished:
					Debug.Log("BattleFinished");
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