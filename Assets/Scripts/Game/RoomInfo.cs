using System;

using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LDJam45.Game
{
	public class RoomInfo : MonoBehaviour
	{
		public GameObject UnitPrefab;
		public Room Room;

		public void Setup()
		{
			var pos = new Vector3(transform.position.x + 2f, transform.position.y + 1, transform.position.z);
			var go = GameObject.Instantiate(UnitPrefab, pos, Quaternion.identity);
			go.transform.SetParent(this.transform);

			var um = go.GetComponent<UnitManager>();
			um.UnitData = Room.Units[0];
			um.Setup();
			go.name = um.ID.ToString();
		}
	}
}