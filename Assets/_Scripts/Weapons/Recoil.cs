using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
	public AnimationCurve verticalRecoilCurve;
	public AnimationCurve horizontalRecoilCurve;

	
	float verticalRecoil;
	float horizontalRecoil;
	float time;

	public float vverticalRecoil = 1.0f;

	public void GenerateRecoil()
	{
		// Rotate the camera's local rotation by applying recoil to its vertical axis
		transform.Rotate(-vverticalRecoil, 0f, 0f);
	}

	private void Update()
	{
		if (time > 0)
		{
			transform.rotation = Quaternion.Euler(horizontalRecoil * 10+transform.rotation.x, transform.rotation.y, transform.rotation.z);
			time -= Time.deltaTime;
		}
		
	}
}
