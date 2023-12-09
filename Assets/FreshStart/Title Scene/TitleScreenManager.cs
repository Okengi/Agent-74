using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;


public class TitleScreenManager : MonoBehaviour
{
	public void StartNetworkHost()
	{
		NetworkManager.Singleton.StartHost();
	}
	public void StartNetworkClient()
	{
		NetworkManager.Singleton.StartClient();
	}
	public void StartNewGame()
	{
		StartCoroutine(WorldSaveGameManager.instance.LoadNewGame());
	}
}