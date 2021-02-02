using UnityEngine;

public static class GameSettings
{
	public static string ServerUrl => "https://dev3r02.elysium.today/inventory/status";
	public static string Auth => "BMeHG5xqJeB4qCjpuJCTQLsqNGaqkfB6";

	public static float DragDistance => 20f;
	public static Vector3 LiftingHeight => new Vector3(0, 2f, 0);
}
