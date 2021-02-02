using System;
using UnityEngine;

namespace GameCore
{
	[Serializable]
	public class SnapPoint
	{
		[SerializeField] private Transform _point;
		[SerializeField] private ItemTypes _pointType;
		[SerializeField] private Renderer _pointRenderer;

		public Transform Point => _point;
		public ItemTypes PointType => _pointType;
		public Renderer PointRenderer => _pointRenderer;
	}
}
