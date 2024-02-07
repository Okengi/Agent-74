using UnityEngine;

namespace MyObjects
{
	[CreateAssetMenu(fileName = "HolePrefabsData", menuName = "Custom/My_Objects/HolePrefabsData")]
	public class HolePrefabsData : ScriptableObject
	{
		[SerializeField] public GameObject[] holePrefabs;
		[SerializeField] public float holeLifetime;
	}
}