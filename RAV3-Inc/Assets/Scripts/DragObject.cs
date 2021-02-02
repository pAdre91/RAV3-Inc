using System;
using UnityEngine;

public class DragObject : MonoBehaviour
{
	[SerializeField] private Collider _myCollider;
	[SerializeField] private Rigidbody _myRigidbody;
	[SerializeField] private Vector3 _liftingHeight = new Vector3(0, 2f, 0);

	private RaycastHit _hit;
	private const float _maxDragDistance = 20f;     //Вынести в константы

	private static DragObject _currentDragObject = null;
	public static DragObject CurrentDragObject => _currentDragObject;

	public static Action<DragObject> ObjectDropped;

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

			_myRigidbody.freezeRotation = true;
			_myRigidbody.useGravity = false;

			_myRigidbody.MovePosition(_hit.point);
			_myRigidbody.MovePosition(_myRigidbody.position + _liftingHeight);
		}
	}

	private void OnMouseUp()
	{
		ObjectDropped?.Invoke(_currentDragObject);

		_myRigidbody.freezeRotation = false;
		_myRigidbody.useGravity = true;

		_currentDragObject = null;
	}
}
 