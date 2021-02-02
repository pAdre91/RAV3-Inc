using GameCore;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
	public class InventoryUI : MonoBehaviour
	{
		[SerializeField] List<InventoryCell> _holdersUI;

		public static Action<Item> TakeItem;

		public void DisplayInventory()
		{
			gameObject.SetActive(true);
		}

		public void CloseInventory()
		{
			foreach (var cell in _holdersUI)
			{
				if (RectTransformUtility.RectangleContainsScreenPoint(cell.GetComponent<RectTransform>(), Input.mousePosition) && cell.CurrentItem != null)
				{
					TakeItem?.Invoke(cell.CurrentItem);
					break;
				}
			}

			gameObject.SetActive(false);
		}

		public void DisplayItem(Item item, int count)
		{
			if (item == null)
			{
				Debug.LogWarning("Empty item to remove");
				return;
			}
			InventoryCell holderForInputItem = GetHolderByType(item.ItemType);

			if (holderForInputItem == null)
				return;

			holderForInputItem.CurrentItem = item;
			holderForInputItem.Icon.sprite = item.Icon;
			holderForInputItem.ItemName.text = item.ItemName;
			holderForInputItem.ItemsCount.text = count.ToString();
		}

		public void RemoveItem(Item item, int remainingItems = 0)
		{
			if (item == null)
			{
				Debug.LogWarning("Empty item to remove");
				return;
			}

			InventoryCell holderForOutputItem = GetHolderByType(item.ItemType);

			if (holderForOutputItem == null)
				return;

			if (remainingItems > 0)
			{
				holderForOutputItem.ItemsCount.text = remainingItems.ToString();
				return;
			}

			holderForOutputItem.CurrentItem = null;
			holderForOutputItem.Icon.sprite = null;
			holderForOutputItem.ItemName.text = "Empty";
			holderForOutputItem.ItemsCount.text = "";
		}

		private InventoryCell GetHolderByType(ItemTypes itemType)
		{
			foreach (var holder in _holdersUI)
			{
				if (holder.ItemTypeUI == itemType)
					return holder;
			}

			Debug.LogWarning(itemType.ToString() + "not found");
			return null;
		}
	}
}