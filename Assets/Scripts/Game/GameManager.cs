using System;
using UnityEngine;

namespace LDJam45.Game
{
	public class GameManager : MonoBehaviour
	{
		[Header("Managers")]
		public MapManager MapManager;
		public DialogManager DialogManager;
		public DebugManager DebugManager;
		public UserManager UserManager;
		public RuleManager RuleManager;
		public GameUIManager GameUIManager;

		public event EventHandler<GameState> OnStageChange;

		[Header("Settings")]
		public GameMode GameMode;

		void Start()
		{
			Debug.Log("Start game");

			// Setup Managers
			MapManager.Setup(this);
			UserManager.Setup(this);
			DialogManager.Setup(this);
			RuleManager.Setup(this);
			GameUIManager.Setup(this);

			OnStageChange?.Invoke(this, GameState.Intro);
		}

		public void Callback(GameState state)
		{
			OnStageChange?.Invoke(this, state);
		}

		public void CheckRoom()
		{
			if (MapManager.DoesEnemyExists())
			{
				OnStageChange?.Invoke(this, GameState.BattleBegin);
			}
		}
	}
}