using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LDJam45.Game
{
	public class HandAreaManager : MonoBehaviour
	{
		private Vector3 Position = new Vector3(-1.5f, -0.75f, 3.2f);
		private float gap = 1f;

		public void Sort()
		{
			int idx = 0;
			foreach (Transform child in transform)
			{
				var pos = new Vector3(Position.x + gap * idx, Position.y, Position.z);
				child.localPosition = pos;
				idx++;
			}
		}
	}
}
