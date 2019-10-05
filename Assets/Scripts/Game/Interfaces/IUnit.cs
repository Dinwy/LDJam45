using System;
using System.Collections.Generic;

namespace LDJam45.Game
{
	public interface IUnit
	{
		string Name { get; }
		int HP { get; }
		int Damage { get; }
		List<Card> Hand { get; }
		Stack<Card> Deck { get; }

		void Draw();
		void Use();
	}
}