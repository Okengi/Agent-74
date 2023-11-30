using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class My_Object : MonoBehaviour
{
	public My_Object_Data my_object;

	float Health;

	private void Start()
	{
		Health = my_object.Health;
	}

	public void Hit(float damage, RaycastHit rayHit)
	{
		if (my_object.damagble) Health -= damage;
		if (Health <= 0)
		{
			DestroySelf();
		}
		var hole = Instantiate(my_object.holePrefaps[Random.Range(0, my_object.holePrefaps.Length)], rayHit.point + new Vector3(rayHit.normal.x * 0.01f, rayHit.normal.y * 0.01f, rayHit.normal.z * 0.01f), Quaternion.LookRotation(-rayHit.normal));
		Quaternion randomRotation = Quaternion.Euler(0,0, Random.Range(0, 360));
		hole.transform.rotation = Quaternion.LookRotation(-rayHit.normal, Vector3.up) * randomRotation;
		hole.transform.parent = transform;

		var impact = Instantiate(my_object.impactPrefap, rayHit.point + new Vector3(rayHit.normal.x * 0.02f, rayHit.normal.y * 0.02f, rayHit.normal.z * 0.02f), Quaternion.LookRotation(rayHit.normal));
		impact.transform.parent = transform;

		Destroy(hole, my_object.holeLifetime);
		Destroy(impact, 3f);
	}

	private void DestroySelf()
	{
		Destroy(gameObject);
	}
}
