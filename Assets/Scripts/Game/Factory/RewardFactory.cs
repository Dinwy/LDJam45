using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace LDJam45.Game
{
	public class RewardFactory : MonoBehaviour
	{
		public Dictionary<RewardType, CardData> Rewards = new Dictionary<RewardType, CardData>();

		void Start()
		{
			Rewards.Add(RewardType.Reward1, Reward1());
		}

		public CardData GetAction(RewardType cc)
		{
			return Rewards[cc];
		}

		private CardData Reward1()
		{
			return new CardData();
		}
	}
}
