using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LDJam45
{
	public class IntroManager : MonoBehaviour
	{
		public TextMeshProUGUI IntroText;

		void Start()
		{
			Sequence seq = DOTween.Sequence();
			seq.PrependInterval(0.5f);
			IntroText.color = new Color(1, 1, 1, 0);

			DOTween.ToAlpha(() => IntroText.color, x => IntroText.color = x, 1, 2)
				.OnComplete(() =>
					DOTween.ToAlpha(() => IntroText.color, x => IntroText.color = x, 0, 2)
							.OnComplete(() => SceneManager.LoadSceneAsync(1)));
		}
	}
}