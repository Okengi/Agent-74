using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
	public float verticalRecoilAmount = 10f;
	public float horizontalRecoil = 5f;
	public float duration = 0.1f;
	float time;
	float reverseTimer;
	int counter = 0;

	float xRotation;
	float yRotation;


	private void Update()
	{
		xRotation = transform.localRotation.x;
		yRotation = transform.localRotation.y;


		if (time > 0)
		{
			ApplyRecoil();
		}

		if (reverseTimer > 0 && time <= 0)
		{
			xRotation += (verticalRecoilAmount * Time.deltaTime) / duration * counter;
			yRotation += (Random.Range(-horizontalRecoil, horizontalRecoil) * Time.deltaTime) / duration;
			reverseTimer -= Time.deltaTime;
			if (reverseTimer <= 0)
			{
				counter = 0;
			}
		}
		else
		{
			reverseTimer = 0;
		}



		transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
	}

	void ApplyRecoil()
	{
		xRotation -= (verticalRecoilAmount * Time.deltaTime) / duration;
		yRotation -= (Random.Range(-horizontalRecoil, horizontalRecoil) * Time.deltaTime) / duration;
		time -= Time.deltaTime;
		if (time <= 0)
			Invoke("SetRevers", duration);
	}

	void SetRevers()
	{
		if (time <= 0)
		{
			reverseTimer = duration;
		}
	}

	public void DoRecoil()
	{
		counter++;
		time = duration;
	}
}
