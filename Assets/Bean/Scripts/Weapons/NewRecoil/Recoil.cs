using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
	float verticalRecoil;
	float horizontalRecoil;
	private void Start()
	{
		WeaponController.Shot += DoRecoil;
		WeaponController.OnAktivGunSwitch += UpdateValues;
	}

	private void OnDestroy()
	{
		WeaponController.Shot -= DoRecoil;
		WeaponController.OnAktivGunSwitch -= UpdateValues;
	}

	private void DoRecoil(int heat)
	{
		float x = verticalRecoil ;
		float y = horizontalRecoil   ;

		transform.rotation = Quaternion.Euler(- x,- y, 0);
	}
	private void UpdateValues(NewGun gun)
	{
		verticalRecoil = gun.verticalRecoil;
		horizontalRecoil = gun.horizontalRecoil;
	}
}
