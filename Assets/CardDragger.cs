
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LDJam45.Game
{
	public class CardDragger : MonoBehaviour
	{
		private Vector3 screenPoint;
		private Vector3 offset;

		public string TargetGuid;
		private Vector3 originPos;

		public CardData Card { get; set; }

		void Start()
		{
			originPos = transform.localPosition;
			MakeItBillboard();
		}

		void OnMouseDown()
		{
			screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		}

		void OnMouseDrag()
		{
			Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
			transform.position = cursorPosition;

			RaycastHit hit;
			int layerMask = 1 << 10;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 300.0f, layerMask))
			{
				TargetGuid = hit.transform.name;
				Debug.Log($"Hit target: {hit.transform.name}");
			}
			else
			{
				TargetGuid = Guid.Empty.ToString();
			}
		}

		void OnMouseUp()
		{
			if (TargetGuid == Guid.Empty.ToString())
			{
				gameObject.transform.localPosition = originPos;
				return;
			}

			GameObject.Find(TargetGuid).GetComponent<UnitManager>()?.GetDamage(Card.Damage);

			var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
			gameManager.Callback(GameState.PlayerTurnEnd);

			GameObject.Find("HandArea").GetComponent<HandAreaManager>().Sort();
			Debug.Log(TargetGuid);

			Destroy(gameObject);
		}

		void MakeItBillboard()
		{
			transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
		}
	}
}