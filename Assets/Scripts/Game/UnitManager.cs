using System;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

namespace LDJam45.Game
{
	public class UnitManager : MonoBehaviour
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

		public void Attack(Unit unit)
		{
			unit.HP -= 10;
		}
	}
}