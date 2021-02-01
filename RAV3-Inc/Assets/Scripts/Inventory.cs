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
		Destroy(inputItem.gameObject);
	}
}
