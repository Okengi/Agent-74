using UnityEngine;

namespace MyObjects
{
	public class My_Object : MonoBehaviour
	{
		[System.Serializable]
		public class ObjectStats
		{
			public float Health = 1000f;
			public bool damageable = false;
			public bool interactable = false;
		}
		[SerializeField] ObjectStats stats;

		[Header("Prefabs")]
		[SerializeField] GameObject impactPrefab;
		[SerializeField] HolePrefabsData holePrefabsData;

		private void Awake()
		{
			if (holePrefabsData == null)
			{
				holePrefabsData = Resources.Load<HolePrefabsData>("HolePrefabsFolder/Default");
			}
		}

		public void Hit(float damage, RaycastHit rayHit)
		{
			ApplyDamage(damage);

			SpawnHolePrefab(rayHit);

			SpawnImpactPrefab(rayHit);
		}

		private void ApplyDamage(float damage)
		{
			if (stats.damageable) 
				stats.Health -= damage;
			if (stats.Health <= 0)
				DestroySelf();
		}

		private void SpawnHolePrefab(RaycastHit rayHit)
		{
			var hole = Instantiate(holePrefabsData.holePrefabs[Random.Range(0, holePrefabsData.holePrefabs.Length)], rayHit.point + new Vector3(rayHit.normal.x * 0.01f, rayHit.normal.y * 0.01f, rayHit.normal.z * 0.01f), Quaternion.LookRotation(-rayHit.normal));
			Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
			hole.transform.rotation = Quaternion.LookRotation(-rayHit.normal, Vector3.up) * randomRotation;
			hole.transform.parent = transform;

			Destroy(hole, holePrefabsData.holeLifetime);
		}

		private void SpawnImpactPrefab(RaycastHit rayHit)
		{
			if (impactPrefab == null)
				return;
			var impact = Instantiate(impactPrefab, rayHit.point + new Vector3(rayHit.normal.x * 0.02f, rayHit.normal.y * 0.02f, rayHit.normal.z * 0.02f), Quaternion.LookRotation(rayHit.normal));
			impact.transform.parent = transform;

			Destroy(impact, 3f);
		}

		private void DestroySelf()
		{
			Destroy(gameObject);
		}
	}
}