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
		public TextMeshProUGUI IntroText;
		public Text textMessage;

		[Header("Camera")]
		public Camera MainCamera;

		[Space()]
		public SceneType SceneType;

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
					seq.Append(GetIntroSequence());
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

		#region  Intro
		public Image CharacterSprite;

		private Tween GetIntroSequence()
		{
			var seq = DOTween.Sequence();

			if (SceneType == SceneType.Tutorial)
			{
				Debug.Log("Scene type is tutorial");
				seq.Append(textMessage.DOText("There was a boy", 2f));
				seq.Append(CharacterSprite.DOColor(new Color(1, 1, 1, 1), 1f));
				seq.Append(textMessage.DOText("               ", 1f));
				seq.Append(textMessage.DOText("Who has nothing", 2f));
				seq.Append(textMessage.DOText("               ", 1f));
				seq.Append(textMessage.DOText("He has a dream", 2f));
				seq.Append(textMessage.DOText("              ", 1f));
				seq.Append(textMessage.DOText("To become a hero", 2f));
				seq.Append(textMessage.DOText("                ", 1f));
				seq.Append(textMessage.DOText("With nothing but only bare hand", 3f));
				seq.Append(textMessage.DOText("                               ", 1f));
				seq.Append(textMessage.DOText("Can he gonna make it?", 3f));
				seq.Append(textMessage.DOText("                     ", 1f));
				seq.Append(textMessage.DOText("Here the advanture begins", 3f));
				seq.Append(textMessage.DOText("                         ", 1f));
			}

			return seq;
		}

		void Update()
		{
			if (initialized) return;
			IntroText.text = textMessage.text;
		}
		#endregion

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