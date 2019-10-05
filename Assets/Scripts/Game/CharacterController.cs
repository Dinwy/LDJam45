using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LDJam45.Game
{
	public class CharacterController : MonoBehaviour
	{
		public GameObject PlayerPrefab;

		void Start()
		{
			Debug.LogWarning("Start game");
			Setup();
		}

		void Setup()
		{
			GameObject.Instantiate(PlayerPrefab, new Vector3(0, 0.5f, 0), Quaternion.identity);
		}
	}
}