using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LDJam45.Game
{
	public class CardDragger : MonoBehaviour
	{
		public Sprite TargetCircle;
		public Sprite TargetCircle_Yes;
		public Sprite TargetCircle_No;

		private Vector3 screenPoint;
		private Vector3 offset;

		public string TargetGuid;
		private Vector3 originPos;

		public CardData Card { get; set; }
		private GameManager gameManager;
		private Renderer Renderer;

		// Temp
		private GameObject handArea;

		void Start()
		{
			originPos = transform.localPosition;
			gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
			MakeItBillboard();
			Renderer = GetComponent<Renderer>();
			handArea = GameObject.Find("HandArea");
		}

		void OnMouseDown()
		{
			var scrPos = Camera.main.WorldToScreenPoint(transform.position);
			var distance = new Vector2(scrPos.x, scrPos.y) - new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0) + new Vector3(distance.x, distance.y, screenPoint.z));
		}

		void OnMouseDrag()
		{
			var ownerUnit = GameObject.Find(Card.OwnerID.ToString()).GetComponent<UnitManager>();

			if (gameManager.GameState != GameState.PlayerTurnStart) return;
			if (ownerUnit.isInAction) return;

			Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
			transform.position = cursorPosition;

			if (Vector3.Distance(originPos, cursorPosition) > 600)
			{
				Renderer.material.mainTexture = TargetCircle.texture;
			};

			RaycastHit hit;
			int layerMask = 1 << 10;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 300.0f, layerMask))
			{
				TargetGuid = hit.transform.name;
				if (TargetGuid != Guid.Empty.ToString())
				{
					if (TargetGuid == this.gameManager.UserManager.PlayerUnitManager.ID.ToString())
					{
						if (Card.CardClass == CardClass.Damage)
						{
							Renderer.material.mainTexture = TargetCircle_No.texture;
						}
					}
					else
					{
						Renderer.material.mainTexture = TargetCircle_Yes.texture;
					}
				}

				Debug.Log($"Hit target: {hit.transform.name}");
			}
			else
			{
				TargetGuid = Guid.Empty.ToString();
				Renderer.material.mainTexture = TargetCircle.texture;

			}
		}

		public void ResetArt()
		{
			Renderer.material.mainTexture = Card.Artwork.texture;
		}

		void OnMouseUp()
		{
			if (TargetGuid == Guid.Empty.ToString())
			{
				Renderer.material.mainTexture = Card.Artwork.texture;
				gameObject.transform.localPosition = originPos;
				handArea.GetComponent<HandAreaManager>().Sort();

				return;
			}

			if (TargetGuid == this.gameManager.UserManager.PlayerUnitManager.ID.ToString())
			{
				if (Card.CardClass == CardClass.Damage)
				{
					Debug.Log("Cannot damage myself");
					Renderer.material.mainTexture = Card.Artwork.texture;

					handArea.GetComponent<HandAreaManager>().Sort();
					return;
				}
			}

			var ownerUnit = GameObject.Find(Card.OwnerID.ToString()).GetComponent<UnitManager>();
			if (ownerUnit.isInAction) return;
			ownerUnit.isInAction = true;

			gameObject.GetComponent<MeshRenderer>().enabled = false;
			handArea.GetComponent<HandAreaManager>().Sort();

			ownerUnit.UseCard(Guid.Parse(TargetGuid), Card, () =>
			{
				gameManager.ChangeState(GameState.PlayerTurnEnd);
				Destroy(gameObject);
			});
		}


		void MakeItBillboard()
		{
			transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
		}
	}
}