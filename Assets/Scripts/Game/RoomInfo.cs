using System;
using System.Collections.Generic;

using UnityEngine;

namespace LDJam45.Game
{
	public class RoomInfo : MonoBehaviour
	{
		public GameObject UnitPrefab;
		public RoomData RoomData;

		public List<GameObject> UnitGameObject = new List<GameObject>();

		public void Setup()
		{
			var pos = new Vector3(transform.position.x + 2f, transform.position.y + 1, transform.position.z);
			var go = GameObject.Instantiate(UnitPrefab, pos, Quaternion.identity);
			go.transform.SetParent(this.transform);

			var um = go.GetComponent<UnitManager>();
			um.UnitData = RoomData.Units[0];
			um.Setup();
			go.name = um.ID.ToString();

			UnitGameObject.Add(go);
		}
	}
}