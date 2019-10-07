using System;
using System.Collections;
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

		[Header("UIButton")]
		public Button MoveNext;
		public GameObject DeckIcon;

		[Space()]
		public SceneType SceneType;

		[Header("Reward")]
		public GameObject CardPrefab;
		public GameObject RewardPanel;
		public Image RewardCardImage;
		public TextMeshProUGUI Description;

		private Vector3 offset;
		private GameManager gameManager { get; set; }
		private bool initialized = false;
		private bool animating = false;

		// Temp
		private GameObject handArea;

		void Awake()
		{
			BlackPanel.SetActive(true);
		}

		void Start()
		{
			handArea = GameObject.Find("HandArea");
		}

		public void Setup(GameManager gm)
		{
			gameManager = gm;
			RegisterEvnets();
		}

		private float blinkDuration = 1f;
		private Tween blinkTween;

		private void OnStageChange(object sender, GameState gs)
		{
			switch (gs)
			{
				case GameState.Intro:
					Debug.LogWarning("intro");
					var seq = DOTween.Sequence();
					seq.Append(GetIntroSequence());
					seq.Append(DOTween.To(() => BlackPanel.GetComponent<CanvasGroup>().alpha, x => BlackPanel.GetComponent<CanvasGroup>().alpha = x, 0, 1f));
					seq.AppendCallback(() =>
					{
						BlackPanel.SetActive(false);
						gameManager.ChangeState(GameState.InitializeGame);
					});
					break;
				case GameState.InitializeFinished:
					Setup();
					break;
				case GameState.PlayerTurnEnd:
					break;
				case GameState.Movable:
					blinkTween = MoveNext.GetComponent<Image>().DOFade(0.0f, blinkDuration).SetEase(Ease.InCubic).SetLoops(2, LoopType.Yoyo);
					blinkTween.OnComplete(() => blinkTween.Restart());
					break;
				case GameState.MoveToRoom:
					if (blinkTween != null)
					{
						blinkTween.OnComplete(null);
						blinkTween.SetAutoKill(true);
					}
					break;
				case GameState.BattleFinished:
					GameObject.Find("HandArea").GetComponent<HandAreaManager>().ClearHands();
					break;
				case GameState.RewardPhase:
					if (gameManager.MapManager.DoesRewardExists())
					{
						var rewardCard = gameManager.MapManager.GetReward();
						gameManager.UserManager.PlayerUnitManager.AddToDeck(rewardCard);
						RewardPanel.SetActive(true);
						RewardCardImage.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
						var seq1 = DOTween.Sequence();
						RewardCardImage.sprite = gameManager.MapManager.GetReward().Artwork;
						Description.text = rewardCard.Description;
						seq1.Append(RewardCardImage.rectTransform.DOScale(0.5f, 1f));
						seq1.AppendInterval(1f);
						var originalPosition = RewardCardImage.transform.position;
						seq1.Append(RewardCardImage.transform.DOMoveY(DeckIcon.transform.position.y, 1f));
						seq1.Join(RewardCardImage.rectTransform.DOScale(0.04f, 1f).SetEase(Ease.InCubic));
						seq1.AppendCallback(() =>
						{
							// Reset the position
							RewardCardImage.transform.position = originalPosition;
							RewardPanel.SetActive(false);
							gameManager.ChangeState(GameState.Movable);
						});
					}
					else
					{
						gameManager.ChangeState(GameState.Movable);
					}
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
				if (gameManager.GameState == GameState.MoveToRoom)
				{
					MainCamera.transform.position = gameManager.UserManager.PlayerUnit.transform.position + offset;
				}
			}
		}

		private void RegisterEvnets()
		{
			gameManager.OnStageChange += OnStageChange;
			MoveNext.onClick.AddListener(MoveToNextRoom);
		}

		private void MoveToNextRoom()
		{
			if (animating)
			{
				Debug.LogWarning("Don't move while animating");

				return;
			}

			if (gameManager.GameState != GameState.Movable)
			{
				Debug.LogWarning("Don't move when not movable state");

				return;
			}

			Debug.LogWarning("Move");
			animating = true;

			var seq = gameManager.UserManager.MoveToNextRoom();
			seq.OnComplete(() =>
			{
				animating = false;
				gameManager.ChangeState(GameState.MoveToRoomFinished);
			});

			// Trigger event
			gameManager.ChangeState(GameState.MoveToRoom);
		}
	}
}