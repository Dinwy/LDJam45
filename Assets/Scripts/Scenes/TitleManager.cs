using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LDJam45
{
	public class TitleManager : MonoBehaviour
	{
		public Button WorldMap;
		public Button Exit;

		void Start()
		{
			WorldMap.onClick.AddListener(OnClickWorldMap);
			Exit.onClick.AddListener(OnClickExit);
		}

		void OnDestroy()
		{
			WorldMap.onClick.RemoveListener(OnClickWorldMap);
			Exit.onClick.RemoveListener(OnClickExit);
		}

		public void OnClickWorldMap()
		{
			// Which is Game
			SceneManager.LoadSceneAsync(2);
		}

		public void OnClickExit()
		{
			Application.Quit();
		}
	}
}