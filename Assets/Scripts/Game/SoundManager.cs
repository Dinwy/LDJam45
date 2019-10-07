using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LDJam45.Game
{
	public class SoundManager : MonoBehaviour
	{
		public GameManager GameManager;
		public AudioSource AudioSource;
		public AudioClip AmbientTrack;
		public AudioClip BattleTrack;

		void Start()
		{
			RegisterEvnets();
			AudioSource.volume = 0;
			AudioSource.clip = AmbientTrack;
			AudioSource.Play();
			AudioSource.DOFade(1, 1f);
		}

		void SwitchTrack(AudioClip targetTrack)
		{
			AudioSource.DOFade(0, 1f).OnComplete(() =>
			{
				AudioSource.clip = targetTrack;
				AudioSource.DOFade(1, 1f);
				AudioSource.Play();
				AudioSource.time = UnityEngine.Random.Range(0, targetTrack.length / 2);
			});
		}

		private void OnStageChange(object sender, GameState gs)
		{
			switch (gs)
			{
				case GameState.BattleBegin:
					SwitchTrack(BattleTrack);
					break;
				case GameState.PlayerTurnStart:
					break;
				case GameState.PlayerTurnEnd:
					break;
				case GameState.EnemyTurnStart:
					break;
				case GameState.EnemyTurnEnd:
					break;
				case GameState.BattleFinished:
					SwitchTrack(AmbientTrack);
					break;
				case GameState.GameOver:
					break;
				default:
					break;
			}
		}

		private void RegisterEvnets()
		{
			GameManager.OnStageChange += OnStageChange;
		}
	}
}