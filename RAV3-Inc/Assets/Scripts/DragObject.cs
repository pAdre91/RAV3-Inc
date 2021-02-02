using UnityEngine;

public class DragObject : MonoBehaviour
{
	[SerializeField] private Collider _myCollider;
	[SerializeField] private Vector3 _liftingHeight = new Vector3(0, 2f, 0);

	private RaycastHit _hit;
	private const float _maxDragDistance = 20f;     //Вынести в константы

	private static DragObject _currentDragObject = null;
	public static DragObject CurrentDragObject => _currentDragObject;

	private void OnMouseDown()
	{
		transform.Translate(_liftingHeight, Space.World);
		_currentDragObject = this;
	}

	private void OnMouseDrag()
	{
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit, _maxDragDistance))
		{
			if (_hit.collider == _myCollider)
				return;

			transform.position = _hit.point;
			transform.Translate(_liftingHeight, Space.World);
		}
	}

	private void OnMouseUp()
	{
		_currentDragObject = null;
	}
}
 