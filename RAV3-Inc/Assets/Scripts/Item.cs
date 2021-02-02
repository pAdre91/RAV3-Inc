using UnityEngine;

namespace GameCore
{
	[CreateAssetMenu(fileName = "New Item", menuName = "Item/New Item")]
	public class Item : ScriptableObject
	{
		[SerializeField] private GameObject _prefab;
		[Space]
		[SerializeField] private int _id = -1;
		[SerializeField] private string _itemName = "DefaultItemName";
		[SerializeField] private float _weight = -1;
		[SerializeField] private ItemTypes _itemType = ItemTypes.Default;
		[SerializeField] private Sprite _icon;

		public GameObject Prefab => _prefab;
		public int Id => _id;
		public string ItemName => _itemName;
		public float Weight => _weight;
		public ItemTypes ItemType => _itemType;
		public Sprite Icon => _icon;
	}

	public enum ItemTypes
	{
		Default,
		Weapon,
		Armor,
		Potion
	}
}
