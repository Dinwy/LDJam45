using System;
using System.Collections.Generic;
using UnityEngine;

namespace LDJam45.Game
{
	[CreateAssetMenu(fileName = "New Unit", menuName = "Unit")]
	public class Unit : ScriptableObject
	{
		public string Name;
		public int HP;
		public int Damage;

		public string Description;
		public Sprite Artwork;
		public sbyte Priority;

		public List<Card> Hand { get; private set; }
		public Stack<Card> Deck { get; private set; }
	}
}