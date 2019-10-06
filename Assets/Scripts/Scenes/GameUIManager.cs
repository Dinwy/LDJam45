using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LDJam45.Game
{
	public class GameUIManager : MonoBehaviour, IManager
	{
		[Header("UI")]
		public Canvas Canvas;
		public GameObject BlackPanel;

		[Header("Camera")]
		public Camera MainCamera;

		private Vector3 offset;
		private GameManager gameManager { get; set; }
		private bool initialized = false;

		public void Setup(GameManager gm)
		{
			gameManager = gm;
			RegisterEvnets();
		}

		private void OnStageChange(object sender, GameState gs)
		{
			switch (gs)
			{
				case GameState.Intro:
					Debug.LogWarning("intro");
					var seq = DOTween.Sequence();
					seq.Append(DOTween.To(() => BlackPanel.GetComponent<CanvasGroup>().alpha, x => BlackPanel.GetComponent<CanvasGroup>().alpha = x, 0, 1f));
					seq.AppendCallback(() => gameManager.Callback(GameState.InitializeGame));
					break;
				case GameState.InitializeFinished:
					Setup();
					break;
				default:
					break;
			}
		}

		private void Setup()
		{
			offset = MainCamera.transform.position - gameManager.UserManager.PlayerUnit.transform.position;
			gameManager.DebugManager.Setup(gameManager);
			initialized = true;
		}

		void LateUpdate()
		{
			if (initialized)
			{
				MainCamera.transform.position = gameManager.UserManager.PlayerUnit.transform.position + offset;
			}
		}

		private void RegisterEvnets()
		{
			gameManager.OnStageChange += OnStageChange;
		}
	}
}