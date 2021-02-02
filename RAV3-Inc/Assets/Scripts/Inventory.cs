using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	[SerializeField] private List<SnapPoint> _snapPoints = new List<SnapPoint>();
	[SerializeField] private Collider _bagCollider;

	private List<Item> _itemsInInventory = new List<Item>();
	private const float _snapDuration = 1f;

	private void Awake()
	{
		Init();
	}

	private void OnDestroy()
	{
		PrepareToDestroy();
	}

	private void Init()
	{
		DragObject.ObjectDropped += PutItem;
	}

	private void PrepareToDestroy()
	{
		DragObject.ObjectDropped -= PutItem;
	}

	#region Highlight
	private void OnMouseEnter()
	{
		HighlightPoint();
	}

	private void OnMouseExit()
	{
		UnHighlightPoint();
	}

	private void HighlightPoint()
	{
		InventoryItem tempInventaryItem = null;

		if (DragObject.CurrentDragObject == null)
			return;

		tempInventaryItem = DragObject.CurrentDragObject.GetComponent<InventoryItem>();

		if (tempInventaryItem == null)
			return;

		foreach (var point in _snapPoints)
		{
			if (point.PointType != tempInventaryItem.GetInventoryType())
				continue;

			point.PointRenderer.enabled = true;
		}
	}

	private void UnHighlightPoint()
	{
		foreach (var point in _snapPoints)
		{
			point.PointRenderer.enabled = false; ;
		}
	}
	#endregion


	private void PutItem(DragObject inputObject)
	{
		if (!IsMouseOnBag())
			return;

		InventoryItem tempItem = DragObject.CurrentDragObject.GetComponent<InventoryItem>();
		if (tempItem == null)
			return;

		foreach (var point in _snapPoints)
		{
			if (point.PointType != tempItem.GetInventoryType())
				continue;

			_itemsInInventory.Add(tempItem.Item);

			TurnOffInteractive(tempItem);
			MoveToSnapPoint(tempItem.gameObject.transform, point);
			UnHighlightPoint();
			return;
		}
	}

	private bool IsMouseOnBag()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit))
		{
			if (hit.collider == _bagCollider)
			{
				return true;
			}
		}

		return false;
	}

	private void TurnOffInteractive(InventoryItem inventoryItem)
	{
		inventoryItem.Rigidbody.freezeRotation = true;
		inventoryItem.Rigidbody.constraints = RigidbodyConstraints.FreezePosition;

		inventoryItem.Collider.enabled = false;
	}

	private void MoveToSnapPoint(Transform inputObject, SnapPoint endPoint)
	{
		inputObject.gameObject.transform.DOMove(endPoint.Point.position, _snapDuration);
		inputObject.gameObject.transform.DORotateQuaternion(endPoint.Point.rotation, _snapDuration);
	}

	private void TakeItem(Item outputItem)
	{
		if (!_itemsInInventory.Contains(outputItem))
		{
			Debug.LogWarning("Item" + outputItem.ItemName + " not found");
			return;
		}

		Instantiate(outputItem.Prefab);		//Заменить на пулл
		_itemsInInventory.Remove(outputItem);
	}
}
