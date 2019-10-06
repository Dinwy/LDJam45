
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using DG.Tweening;
using TMPro;

namespace LDJam45
{
	public class IntroManager : MonoBehaviour
	{
		public TextMeshProUGUI IntroText;
		public Text textMessage;

		void Start()
		{
			Sequence seq = DOTween.Sequence();
			seq.PrependInterval(0.5f);
			IntroText.color = new Color(1, 1, 1, 0);

			seq.Append(DOTween.ToAlpha(() => IntroText.color, x => IntroText.color = x, 1, 2));
			seq.Append(textMessage.DOText("LDJam45", 2f));
			seq.Append(textMessage.DOText("       ", 2f));
			seq.AppendCallback(() => SceneManager.LoadSceneAsync(1));
		}

		void Update()
		{
			IntroText.text = textMessage.text;
		}
	}
}