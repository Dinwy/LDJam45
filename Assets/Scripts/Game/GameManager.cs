using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LDJam45.Game
{
	public class GameManager : MonoBehaviour
	{
		public GameObject PlayerPrefab;
		public GameObject DebugPanel;
		public Button MoveNext;

		[Header("Managers")]
		public MapManager MapManager;

		private GameObject player;

		void Start()
		{
			Debug.LogWarning("Start game");
			Setup();

			MoveNext.onClick.AddListener(() =>
			{
				Debug.Log("moveX");
				player.GetComponent<UnitManager>().MoveX();
			});
		}

		void Setup()
		{
			player = GameObject.Instantiate(PlayerPrefab, new Vector3(0, 0.5f, 0), Quaternion.identity);
		}
	}
}