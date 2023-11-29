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
		var hole = Instantiate(my_object.holePrefap, rayHit.point + new Vector3(rayHit.normal.x * 0.01f, rayHit.normal.y * 0.01f, rayHit.normal.z * 0.01f), Quaternion.LookRotation(-rayHit.normal));
		var impact = Instantiate(my_object.impactPrefap, rayHit.point + new Vector3(rayHit.normal.x * 0.01f, rayHit.normal.y * 0.01f, rayHit.normal.z * 0.01f), Quaternion.LookRotation(rayHit.normal));
		hole.transform.parent = transform;
		impact.transform.parent = transform;

		Destroy(hole, my_object.holeLifetime);

	}

	private void DestroySelf()
	{
		Destroy(gameObject);
	}
}
