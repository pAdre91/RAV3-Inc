using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/New Item")]
public class Item : ScriptableObject
{
	[SerializeField] private int _id = -1;
	[SerializeField] private string _itemName = "DefaultItemName";
	[SerializeField] private float _weight = -1;
	[SerializeField] private ItemTypes _ = ItemTypes.Default;

}

public enum ItemTypes
{
	Default,
	Weapon,
	Armor,
	Potion
}
