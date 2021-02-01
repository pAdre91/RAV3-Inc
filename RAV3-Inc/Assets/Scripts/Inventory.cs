using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	private List<Item> _itemsInInventory = new List<Item>();

	private void OnCollisionEnter(Collision collision)
	{
		PutItem(collision.gameObject.GetComponent<InventoryItem>());
	}

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
