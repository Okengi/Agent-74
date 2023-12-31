using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSaveGameManager : MonoBehaviour
{
    public static WorldSaveGameManager instance;
	[SerializeField] public int worldsceneIndex = 1;

	private void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		DontDestroyOnLoad(gameObject);
	}

	public IEnumerator LoadNewGame()
	{
		AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldsceneIndex);

		yield return null;
	}
}