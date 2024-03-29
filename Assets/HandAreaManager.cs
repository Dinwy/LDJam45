﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LDJam45.Game
{
	public class HandAreaManager : MonoBehaviour
	{
		private Vector3 Position = new Vector3(-1.8f, -0.75f, 3.2f);
		private float gap = 0.3f;

		public void Sort()
		{
			int idx = 0;
			foreach (Transform child in transform)
			{
				if (!child.gameObject.GetComponent<MeshRenderer>().enabled) continue;
				var pos = new Vector3(Position.x + gap * idx, Position.y, Position.z + (-idx * 0.1f * gap));
				child.localPosition = pos;
				idx++;
			}
		}

		public void ClearHands()
		{
			foreach (Transform child in transform)
			{
				Destroy(child.gameObject);
			}
		}
	}
}
