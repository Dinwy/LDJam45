using System;
using System.Collections.Generic;
using UnityEngine;

namespace LDJam45.Game
{
	[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
	public class CardData : ScriptableObject
	{
		public string Name;
		public string Description;
		public Sprite Artwork;
		public TargetType CardType;
		public AttackType AttackType;
		public int Amount;
		public sbyte Priority;
		public CardClass CardClass;
		public Guid OwnerID { get; set; }
	}
}