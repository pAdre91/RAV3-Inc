using UnityEngine;
using UnityEngine.Networking;

public class Server
{
	private string _serverUrl;
	private string _auth;

	public Server(string serverUrl, string auth)
	{
		_serverUrl = serverUrl;
		_auth = auth;
	}

	public void SendAction(int id, string action)
	{
		using (var request = new UnityWebRequest(_serverUrl, UnityWebRequest.kHttpVerbPOST))
		{
			if (request.isNetworkError || request.isHttpError)
			{
				Debug.LogError(request.error);
				return;
			}

			request.SetRequestHeader("auth", _auth);
			request.SetRequestHeader("action", action);
			request.SetRequestHeader("id", id.ToString());
			request.SendWebRequest();
		}
	}
}
