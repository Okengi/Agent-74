using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] GameObject knife;
    [SerializeField] GameObject[] weapons;
    int aktiveWeapon = 0;
	int lastAktiv = 0;
	GameObject aktiv;
	bool changed;
	public Camera playerCam;
	PlayerCamera CamScript;
	Gun AktivGunScript;
	private void Start()
	{
        aktiveWeapon = 0;
		lastAktiv = 0;
		aktiv = weapons[0];
		AktivGunScript = aktiv.GetComponent<Gun>();
		CamScript = playerCam.GetComponent<PlayerCamera>();
		CamScript.NewWeapon(AktivGunScript.verticalRecoil, AktivGunScript.horizontalRecoil, AktivGunScript.duration);
	}
	private void Update()
	{
		WeaponHolderInput();
		if(aktiveWeapon != lastAktiv)
		{
			AktivGunScript.StopReload();
			aktiv.SetActive(false);
			aktiv = weapons[aktiveWeapon];
			AktivGunScript = aktiv.GetComponent<Gun>();
			aktiv.SetActive(true);
			AktivGunScript.UpdateAmmoText();
			lastAktiv = aktiveWeapon;
			CamScript.NewWeapon(AktivGunScript.verticalRecoil, AktivGunScript.horizontalRecoil, AktivGunScript.duration);
		}

	}
	void WeaponHolderInput()
    {
        float i = Input.mouseScrollDelta.y;
		if (i < 0)
		{
			aktiveWeapon--;
		}
		else if (i > 0)
		{
			aktiveWeapon++;
		}
		aktiveWeapon = Mathf.Clamp(aktiveWeapon, 0, weapons.Length - 1);
    }
}