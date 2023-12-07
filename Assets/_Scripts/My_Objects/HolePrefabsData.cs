using UnityEngine;

namespace MyObjects
{
	[CreateAssetMenu(fileName = "HolePrefabsData", menuName = "Custom/HolePrefabsData")]
	public class HolePrefabsData : ScriptableObject
	{
		[SerializeField] public GameObject[] holePrefabs;
		[SerializeField] public float holeLifetime;
	}
}