using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Gun
{
	public int bulletsPerShot;

	public override void Shoot()
	{
		canshoot = false;
		int shootAmount;
		if (bulletsInMag >= bulletsPerShot)
		{
			bulletsInMag -= bulletsPerShot;
			shootAmount = bulletsPerShot;
		}
		else
		{
			shootAmount = bulletsInMag;
			bulletsInMag = 0;
		}

		for (int i = 0; i < shootAmount; i++)
		{
			Vector3 offset = new Vector3(Random.Range(-1f,1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
			RaycastHit rayHit;
			if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward + offset * 0.07f, out rayHit, Mathf.Infinity))
			{
				if (rayHit.transform.tag == "Player")
				{
					Debug.Log(rayHit.transform.name);
				}
				else
				{
					My_Object hitObject = rayHit.transform.GetComponent<My_Object>();

					if (hitObject == null)
					{
						hitObject = rayHit.transform.GetComponentInParent<My_Object>();
					}
					if (hitObject != null)
						hitObject.Hit(damage, rayHit);
				}
				Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * rayHit.distance, Color.yellow, 5);
			}
		}
		Invoke("ResetCanShoot", 60 / fireRate);
		ShootVisuals();
	}
}
