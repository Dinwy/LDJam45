using System;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

namespace LDJam45.Game
{
	public class UnitManager : MonoBehaviour, IManager
	{
		public Unit unit;
		public UnitUI UnitUI;

		public GameManager GameManager { get; private set; }

		private const float roomDistance = 10f;

		public void Setup(GameManager gm)
		{
			GameManager = gm;

			Debug.Log("Setup Unit Manager");
			SetupUI(this.unit);
		}

		public void SetupUI(Unit unit)
		{
			UnitUI.Setup(unit);
		}

		public Sequence MoveToNextRoom()
		{
			var seq = DOTween.Sequence();
			seq.Append(gameObject.transform.DOMoveX(transform.position.x + roomDistance, 2f));
			return seq;
		}
	}
}