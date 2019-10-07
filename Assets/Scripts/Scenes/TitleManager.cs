using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LDJam45
{
	public class TitleManager : MonoBehaviour
	{
		public Canvas Canvas;
		public Button StartButton;
		private bool gameStarted = false;

		void Start()
		{
			DOTween.To(() => Canvas.GetComponent<CanvasGroup>().alpha, x => Canvas.GetComponent<CanvasGroup>().alpha = x, 1, 3f).SetEase(Ease.InCubic);
			StartButton.onClick.AddListener(StartGame);
			var canvasGroup = StartButton.GetComponent<CanvasGroup>();
			DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0, 1f).SetEase(Ease.InCubic).SetLoops(-1, LoopType.Yoyo);
		}

		void OnDestroy()
		{
			StartButton.onClick.RemoveListener(StartGame);
		}

		public void StartGame()
		{
			DOTween.To(() => Canvas.GetComponent<CanvasGroup>().alpha, x => Canvas.GetComponent<CanvasGroup>().alpha = x, 0, 3f)
			.OnComplete(() => SceneManager.LoadSceneAsync(2));
		}

		void Update()
		{
			if (!gameStarted && Input.anyKeyDown)
			{
				gameStarted = true;
				StartGame();
			}
		}

		public void OnClickExit()
		{
			Application.Quit();
		}
	}
}