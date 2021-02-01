using UnityEngine;

public class InventoryItem : MonoBehaviour
{
	[SerializeField] private Item _item;
	public Item Item => _item;

	public ItemTypes GetInventoryType()
	{
		return _item.ItemType;
	}

	public Sprite GetIcon()
	{
		return _item.Icon;
	}
}
