﻿using System;

using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LDJam45.Game
{
	public class GameManager : MonoBehaviour
	{
		[Header("Camera")]
		public Camera MainCamera;
		public Camera SubCamera;

		[Header("Managers")]
		public MapManager MapManager;
		public DialogManager DialogManager;
		public DebugManager DebugManager;
		public UserManager UserManager;
		public RuleManager RuleManager;

		public event EventHandler<GameState> OnStageChange;

		[Header("Settings")]
		public GameMode GameMode;

		private Vector3 offset;

		void Start()
		{
			Debug.LogWarning("Start game");

			// Setup Managers
			MapManager.Setup(this);
			UserManager.Setup(this);
			DialogManager.Setup(this);
			RuleManager.Setup(this);

			OnStageChange?.Invoke(this, GameState.Initialize);

			Setup();
		}

		void Setup()
		{
			offset = MainCamera.transform.position - UserManager.PlayerUnit.transform.position;
			DebugManager.Setup(this);

			// Debug purpose
			DebugManager.GiveMockDeck.onClick?.Invoke();
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

		void LateUpdate()
		{
			MainCamera.transform.position = UserManager.PlayerUnit.transform.position + offset;
		}
	}
}