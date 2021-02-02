using UnityEngine;

public class InventoryItem : MonoBehaviour
{
	[SerializeField] private Rigidbody _myRigitbody;
	[SerializeField] private Collider _myCollider;
	[SerializeField] private Item _item;

	public Item Item => _item;
	public Rigidbody Rigidbody => _myRigitbody;
	public Collider Collider => _myCollider;

	private void Awake()
	{
		_myRigitbody.mass = _item.Weight;
	}

	public ItemTypes GetInventoryType()
	{
		return _item.ItemType;
	}

	public Sprite GetIcon()
	{
		return _item.Icon;
	}
}
