using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
	[SerializeField] List<UIHolder> _holdersUI = new List<UIHolder>();

	public void DisplayInventory()
	{
		gameObject.SetActive(true);
	}

	public void CloseInventory()
	{
		gameObject.SetActive(false);
	}

	public void DisplayItem(Item item, int count)
	{
		UIHolder holderForInputItem = GetHolderByType(item.ItemType);

		if (holderForInputItem == null)
			return;

		holderForInputItem.Icon.sprite = item.Icon;
		holderForInputItem.ItemName.text = item.ItemName;
		holderForInputItem.ItemsCount.text = count.ToString();
	}

	public void RemoveItem(Item item, int remainingItems)
	{
		UIHolder holderForInputItem = GetHolderByType(item.ItemType);

		if (holderForInputItem == null)
			return;

		if (remainingItems > 0)
		{
			holderForInputItem.ItemsCount.text = remainingItems.ToString();
			return;
		}

		holderForInputItem.Icon.sprite = null;
		holderForInputItem.ItemName.text = "Empty";
		holderForInputItem.ItemsCount.text = "";
	}

	private UIHolder GetHolderByType(ItemTypes itemType)
	{
		foreach (var holder in _holdersUI)
		{
			if (holder.ItemTypeUI == itemType)
				return holder;
		}

		Debug.LogWarning(itemType.ToString() +  "not found");
		return null;
	}
}
