using System;
using System.Collections.Generic;
using UnityEngine;

namespace LDJam45.Game
{
	public class UnitManager : MonoBehaviour, IUnitAction
	{
		public UnitData UnitData;
		public UnitUI UnitUI;
		public UserType UserType = UserType.Human;

		// Default card
		public CardData defaultCardData;

		public List<CardData> Hands = new List<CardData>();
		public List<CardData> Deck = new List<CardData>();

		public event EventHandler<CardData> OnCardDraw;
		public event EventHandler<CardData> OnCardAddedToDeck;
		public event EventHandler<Action> OnCardRemoved;
		public event EventHandler<CardArgs> OnAttack;// Need to change arg to Card
		public event EventHandler<CardArgs> OnGetHeal; // Need to change arg to Card
		public event EventHandler OnUnitDied;
		public event EventHandler<int> OnGetDamage; // Need to change arg to Card
		public event EventHandler<Action> OnExhausted;// Need to change arg to Card

		public Guid ID { get; private set; } = Guid.NewGuid();
		public int HP { get; private set; }
		public UnitState UnitState { get; private set; }
		public bool IsDead
		{
			get
			{
				return this.HP <= 0;
			}
			private set
			{
				IsDead = value;
			}
		}

		public bool isInAction = false;

		public void Setup()
		{
			if (UnitData == null)
			{
				Debug.Log($"[{this.GetType().Name}] Unit data is null!");
				return;
			}

			Debug.Log("Setup Unit Manager");
			HP = UnitData.HP;
			UnitUI.Setup(this);
		}

		public void AddToDeck(CardData card)
		{
			Deck.Add(card);
			OnCardAddedToDeck?.Invoke(this, card);
		}

		public void InitialPhase()
		{
			Hands.Clear();

			for (int i = 0; i < 2; i++)
			{
				Draw();
			}
		}

		public void Draw()
		{
			// Draw default card when nothing on your deck
			if (Deck.Count == 0)
			{
				Debug.LogWarning("No cards in the deck!");
				for (int i = 0; i < 2; i++)
				{
					Deck.Add(defaultCardData);
				}
				Hands.Add(defaultCardData);
				Debug.Log($"Card added: {defaultCardData.Name}, {Hands.Count}");

				Debug.Log($"{UnitData.Name}, {UserType.ToString()}");

				// Don't tirgger UI
				if (UserType == UserType.Computer) return;

				OnCardDraw?.Invoke(this, defaultCardData);
				return;
			}

			var random = new System.Random();
			var idx = random.Next(Deck.Count);
			var card = Deck[idx];

			// Remove from the deck and add to hand
			// Deck.RemoveAt(idx);
			Hands.Add(card);
			Debug.Log($"Card added: {card.Name}, {Hands.Count}");

			Debug.Log($"{UnitData.Name}, {UserType.ToString()}");

			// Don't tirgger UI
			if (UserType == UserType.Computer) return;

			OnCardDraw?.Invoke(this, card);
		}

		public void Attack(UnitManager unit)
		{
		}

		public void UseCard(Guid targetId, CardData card, Action callBack)
		{
			if (UnitState == UnitState.Exhausted)
			{
				UnitState = UnitState.Normal;
				OnExhausted?.Invoke(this, callBack);
				return;
			}

			// Temp
			if (card.Name == "Tail Attack")
			{
				Debug.LogWarning("Tail attack!!!!!");
				UnitState = UnitState.Exhausted;
			}

			// var cf = new CardFactory();
			// var act = cf.GetAction(card.CardClass);
			isInAction = true;
			var target = GameObject.Find(targetId.ToString()).GetComponent<UnitManager>();
			var args = new CardArgs(this, target, card, callBack);

			switch (card.CardClass)
			{
				case CardClass.Damage:
					OnAttack?.Invoke(this, args);
					break;
				case CardClass.Heal:
					this.HP += card.Amount;
					if (this.HP >= UnitData.HP) this.HP = UnitData.HP;

					OnGetHeal?.Invoke(this, args);
					break;
				default:
					break;
			}
		}

		public void GetDamage(int amount)
		{
			this.HP -= amount;
			if (this.HP <= 0) this.HP = 0;

			// Tigger when unit died
			if (!IsDead)
			{
				OnGetDamage?.Invoke(this, amount);
			}
			else if (IsDead)
			{
				OnGetDamage?.Invoke(this, amount);
				OnUnitDied?.Invoke(this, EventArgs.Empty);
			}
		}
	}

	public class CardArgs : EventArgs
	{
		public UnitManager Attacker;
		public UnitManager Receiver;
		public CardData CardData;
		public Action Callback;
		public CardArgs(UnitManager attacker, UnitManager receiver, CardData data, Action callBack)
		{
			Attacker = attacker;
			Receiver = receiver;
			CardData = data;
			Callback = callBack;
		}
	}
}