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
			var idx = 0;

			foreach (var unitData in RoomData.Units)
			{
				var pos = new Vector3(transform.position.x + 3f - idx, transform.position.y + 1, transform.position.z + idx);
				var go = GameObject.Instantiate(UnitPrefab, pos, Quaternion.identity);
				go.transform.SetParent(this.transform);

				var um = go.GetComponent<UnitManager>();
				um.UnitData = unitData;

				// Need to set proper unit factory
				um.UserType = UserType.Computer;
				um.Setup();
				go.name = um.ID.ToString();

				UnitGameObject.Add(go);
				idx++;
			}
		}
	}
}