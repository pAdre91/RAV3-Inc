using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	[SerializeField] private InventoryUI _inventoryUI = null;

	[SerializeField] private List<SnapPoint> _snapPoints = new List<SnapPoint>();
	[SerializeField] private Collider _bagCollider;
	[SerializeField] private Transform _unloadingPoint;

	private Dictionary<Item, InventoryItem> _itemsInInventory = new Dictionary<Item, InventoryItem>();
	private const float _snapDuration = 1f;

	public static Action<Item> ItemPut;
	public static Action<Item> ItemTake;


	private void Awake()
	{
		Init();
	}

	private void OnMouseDown()
	{
		_inventoryUI.DisplayInventory();
	}

	private void OnMouseUp()
	{
		_inventoryUI.CloseInventory();
	}

	private void OnDestroy()
	{
		PrepareToDestroy();
	}

	private void Init()
	{
		DragObject.ObjectDropped += PutItem;
		InventoryUI.TakeItem += TakeItem;

		if (_inventoryUI == null)
		{
			GameObject.FindObjectOfType(typeof(InventoryUI));
			Debug.LogWarning("Inventory UI not defined");
		}
	}

	private void PrepareToDestroy()
	{
		DragObject.ObjectDropped -= PutItem;
		InventoryUI.TakeItem -= TakeItem;
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

			_itemsInInventory.Add(tempItem.Item, tempItem);

			TurnOffInteractive(tempItem);
			MoveToPoint(tempItem.transform, point.Point).Play();
			UnHighlightPoint();

			ItemPut?.Invoke(tempItem.Item);

			_inventoryUI.DisplayItem(tempItem.Item, 1);				//Заменить на реальное количество
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

	private void TurnOnInteractive(InventoryItem inventoryItem)
	{
		inventoryItem.Rigidbody.freezeRotation = false;
		inventoryItem.Rigidbody.constraints = RigidbodyConstraints.None;

		inventoryItem.Collider.enabled = true;
	}

	private Sequence MoveToPoint(Transform inputObject, Transform endPoint)
	{
		Sequence moveSequence = DOTween.Sequence();

		moveSequence.Append(inputObject.gameObject.transform.DOMove(endPoint.position, _snapDuration));
		moveSequence.Join(inputObject.gameObject.transform.DORotateQuaternion(endPoint.rotation, _snapDuration));

		return moveSequence;
	}

	private void TakeItem(Item outputItem)
	{
		if (outputItem == null)
		{
			Debug.LogWarning("Take empty item");
			return;
		}

		if (!_itemsInInventory.ContainsKey(outputItem))
		{
			Debug.LogWarning("Item" + outputItem.ItemName + " not found");
			return;
		}

		var moveSeq = MoveToPoint(_itemsInInventory[outputItem].transform, _unloadingPoint);

		moveSeq.AppendCallback(() =>
		{
			TurnOnInteractive(_itemsInInventory[outputItem]);
			_itemsInInventory.Remove(outputItem);
		});


		moveSeq.Play();

		ItemTake?.Invoke(outputItem);

		_inventoryUI.RemoveItem(outputItem, 0);

	} 
}
