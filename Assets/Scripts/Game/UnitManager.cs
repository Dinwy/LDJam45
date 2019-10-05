using System;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

namespace LDJam45.Game
{
	public class UnitManager : MonoBehaviour
	{
		public Unit unit;

		void Start()
		{
			Debug.Log("meow");
			GetComponent<SpriteRenderer>().sprite = unit.Artwork;
		}

		public void MoveX()
		{
			var seq = DOTween.Sequence();

			gameObject.transform.DOMoveX(transform.position.x + 1, 2f);
		}
	}
}