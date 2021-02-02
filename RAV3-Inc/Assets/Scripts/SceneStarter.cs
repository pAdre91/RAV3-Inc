using Core;
using GameCore;
using UnityEngine;

namespace GameCore
{
	public class SceneStarter : MonoBehaviour
	{
		private Server _server;

		private void Awake()
		{
			Init();
		}

		private void OnDestroy()
		{
			PrepareToDestroy();
		}

		public void Init()
		{
			_server = new Server(GameSettings.ServerUrl, GameSettings.Auth);

			Inventory.ItemPut.AddListener(_server.SendAction);
			Inventory.ItemTake.AddListener(_server.SendAction);
		}

		private void PrepareToDestroy()
		{
			Inventory.ItemPut.RemoveListener(_server.SendAction);
			Inventory.ItemTake.RemoveListener(_server.SendAction);
		}
	}
}
