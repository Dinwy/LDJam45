using System;
using System.Collections.Generic;
using UnityEngine;

namespace LDJam45.Game
{
	[CreateAssetMenu(fileName = "New Room", menuName = "Room")]
	public class RoomData : ScriptableObject
	{
		public string Name;
		public string Description;
		public UnitData[] Units;
	}
}