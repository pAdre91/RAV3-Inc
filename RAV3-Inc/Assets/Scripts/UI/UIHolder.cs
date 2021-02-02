using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UIHolder
{
	[SerializeField] private Image _icon;
	[SerializeField] private ItemTypes _itemTypeUI;
	[SerializeField] private Text _itemName;
	[SerializeField] private Text _countItems;


	public Image Icon => _icon;
	public ItemTypes ItemTypeUI => _itemTypeUI;
	public Text ItemName => _itemName;
	public Text ItemsCount => _countItems;
}
