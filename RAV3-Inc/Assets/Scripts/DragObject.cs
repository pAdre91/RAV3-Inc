using UnityEngine;

public class DragObject : MonoBehaviour
{
	[SerializeField] private Collider _myCollider;
	[SerializeField] private Vector3 _liftingHeight= new Vector3(0, 1f, 0);

	private RaycastHit _hit;
	private const float _maxDragDistance = 20f;		//Вынести в константы

	private void OnMouseDown()
	{
		transform.Translate(Vector3.up * 2, Space.World);
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

		//Запоминать крайнюю точку при вызоде за зону дарага и держать объект на ней
	}
}
 