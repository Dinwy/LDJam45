using System;
using System.Collections.Generic;
using UnityEngine;

namespace LDJam45.Game
{
	[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
	public class Card : ScriptableObject
	{
		public string Name;
		public string Description;
		public Sprite Artwork;
		public CardType CardType;
		public AttackType AttackType;
		public int Damage;
		public sbyte Priority;
	}
}