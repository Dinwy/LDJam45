using System;
using System.Collections.Generic;
using DG.Tweening;

namespace LDJam45.Game
{
	public class CardAnimationFactory
	{
		public Dictionary<CardAnimation, Tween> CardAnimations = new Dictionary<CardAnimation, Tween>();

		public CardAnimationFactory()
		{
			CardAnimations.Add(CardAnimation.NormalAttack, DamageTaget());
			CardAnimations.Add(CardAnimation.Magic, HealTarget());
		}

		public Tween GetAction(CardAnimation cc)
		{
			return CardAnimations[cc];
		}

		public ICard Create()
		{
			return null;
		}

		private Tween DamageTaget()
		{
			return DOTween.Sequence();
		}

		private Tween HealTarget()
		{
			return DOTween.Sequence();
		}
	}
}
