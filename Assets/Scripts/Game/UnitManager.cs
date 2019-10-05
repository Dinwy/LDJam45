using System;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

namespace LDJam45.Game
{
	public class UnitManager : MonoBehaviour, IUnitAction
	{
		public UnitData UnitData;
		public UnitUI UnitUI;

		public List<Card> Hands = new List<Card>();
		public Stack<Card> Deck = new Stack<Card>();

		public event EventHandler<Card> OnCardDraw;

		public void Setup()
		{
			Debug.Log("Setup Unit Manager");
			UnitUI.Setup(this);
		}

		public void Draw()
		{
			if (Deck.Count == 0)
			{
				Debug.LogWarning("No cards in the deck!");
				return;
			}

			var card = Deck.Pop();
			Hands.Add(card);
			Debug.Log($"Card added: {card.Name}, {Hands.Count}");
			OnCardDraw?.Invoke(this, card);
		}

		public void Use()
		{

		}

		public void Attack(UnitManager unit)
		{
			unit.UnitData.HP -= 10;
		}
	}
}