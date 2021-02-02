using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	[SerializeField] private List<SnapPoint> _snapPoints = new List<SnapPoint>();

	private List<Item> _itemsInInventory = new List<Item>();
	private InventoryItem _tempInventaryItem = null;

	private void OnCollisionEnter(Collision collision)
	{
		PutItem(collision.gameObject.GetComponent<InventoryItem>());
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
		_tempInventaryItem = null;

		if (DragObject.CurrentDragObject == null)
			return;

		_tempInventaryItem = DragObject.CurrentDragObject.GetComponent<InventoryItem>();

		if (_tempInventaryItem == null)
			return;

		foreach (var point in _snapPoints)
		{
			if (point.PointType != _tempInventaryItem.GetInventoryType())
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

	private void PutItem(InventoryItem inputItem)
	{
		if (inputItem == null)
			return;

		_itemsInInventory.Add(inputItem.Item);
		Destroy(inputItem.gameObject);		//Заменить на пулл
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
