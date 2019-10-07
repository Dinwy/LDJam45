using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LDJam45.Game
{
	public class MonsterCardManager : MonoBehaviour, IManager
	{
		[Header("Card Data")]
		public MockCardList MockCardList;
		public MockCardList MockCardList_Lamia;
		public MockCardList MockCardList_Cyclops;
		public MockCardList MockCardList_Centaur;
		public MockCardList MockCardList_MonsterA;

		public Dictionary<string, MockCardList> CardDict;

		private GameManager gameManager { get; set; }

		public void Setup(GameManager gm)
		{
			gameManager = gm;

			CardDict = new Dictionary<string, MockCardList>()
			{
				{"Boy", MockCardList},
				{"Lamia", MockCardList_Lamia},
				{"Cyclops", MockCardList_Cyclops},
				{"Centaur", MockCardList_Centaur},
				{"MonsterA", MockCardList_MonsterA},
			};
		}

		public MockCardList GetCardList(string name)
		{
			Debug.LogWarning($"{name} is getting card list");
			return CardDict[name];
		}
	}
}