using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
	[SerializeField] private Image _icon;
	[SerializeField] private ItemTypes _itemTypeUI;
	[SerializeField] private Text _itemName;
	[SerializeField] private Text _countItems;
	[SerializeField] private Button _button;

	public Image Icon => _icon;
	public ItemTypes ItemTypeUI => _itemTypeUI;
	public Text ItemName => _itemName;
	public Text ItemsCount => _countItems;

	public Item CurrentItem { get; set; }
}
