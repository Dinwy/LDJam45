using System;
using System.Collections.Generic;
using UnityEngine;

namespace LDJam45.Game
{
	[CreateAssetMenu(fileName = "New Unit", menuName = "Unit")]
	public class UnitData : ScriptableObject
	{
		public string Name;
		public int HP;
		public int Damage;

		public string Description;
		public Sprite Artwork;

		public List<CardData> Hand { get; private set; }
		public Stack<CardData> Deck { get; private set; }
	}
}