using System;
using System.Collections.Generic;

namespace LDJam45.Game
{
	public class CardFactory
	{
		public Dictionary<CardClass, Action> CardActionFactory = new Dictionary<CardClass, Action>();

		public CardFactory()
		{
			CardActionFactory.Add(CardClass.Damage, DamageTaget);
			CardActionFactory.Add(CardClass.Heal, HealTarget);
		}

		public Action GetAction(CardClass cc)
		{
			return CardActionFactory[cc];
		}

		public ICard Create()
		{
			return null;
		}

		private void DamageTaget()
		{

		}

		private void HealTarget()
		{

		}
	}
}
