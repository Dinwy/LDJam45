using System;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

namespace LDJam45.Game
{
	public class UnitManager : MonoBehaviour, IUnitAction
	{
		public Unit unit;
		public UnitUI UnitUI;

		public List<Card> Hands = new List<Card>();
		public Stack<Card> Deck = new Stack<Card>();

		public void Setup()
		{
			Debug.Log("Setup Unit Manager");
			SetupUI(unit);
		}

		public void SetupUI(Unit unit)
		{
			UnitUI.Setup(unit);
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
		}

		public void Use()
		{

		}

		public void Attack(Unit unit)
		{
			unit.HP -= 10;
		}
	}
}