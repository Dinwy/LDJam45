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
		public MonsterCardManager MonsterCardManager;

		public event EventHandler<GameState> OnStageChange;

		public GameState GameState { get; private set; }

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
			MonsterCardManager.Setup(this);

			OnStageChange += (object sender, GameState gs) =>
			{
				Debug.Log($"[{this.GetType().Name}] State changed from {GameState} to {gs}");
			};

			OnStageChange?.Invoke(this, GameState.Intro);
		}

		public void ChangeState(GameState gameState)
		{
			Debug.Log($"[{this.GetType().Name}] State changed from {GameState} to {gameState}");

			if (gameState == GameState.InitializeFinished)
			{
				OnStageChange?.Invoke(this, gameState);
				ChangeState(GameState.Movable);
				return;

			}
			GameState = gameState;
			OnStageChange?.Invoke(this, gameState);
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