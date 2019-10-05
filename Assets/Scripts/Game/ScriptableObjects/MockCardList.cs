using UnityEngine;

namespace LDJam45.Game
{
	[CreateAssetMenu(fileName = "CardList", menuName = "MockCardList")]
	public class MockCardList : ScriptableObject
	{
		public Card[] CardList;
	}
}