using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
	[SerializeField] GameObject[] weapons;
	int aktiveWeapon = 0;
	int lastAktiv = 0;
	GameObject aktiv;
	NewGun AktivGunScript;


	public static WeaponController instance;
	public static event Action<NewGun> OnAktivGunSwitch;
	public static event Action<int> Shot;

	public struct RecoilData
	{

	}

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(this);
		}	
	}
	private void Start()
	{
		aktiveWeapon = 0;
		lastAktiv = 0;
		aktiv = weapons[0];
		AktivGunScript = aktiv.GetComponent<NewGun>();
	}
	private void Update()
	{
		SwitchWeapon();
	}
	void SwitchWeapon()
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
		if (aktiveWeapon != lastAktiv)
		{
			AktivGunScript.StopReload();
			aktiv.SetActive(false);
			aktiv = weapons[aktiveWeapon];

			AktivGunScript = aktiv.GetComponent<NewGun>();
			aktiv.SetActive(true);
			AktivGunScript.UpdateAmmoText();
			lastAktiv = aktiveWeapon;

			OnAktivGunSwitch?.Invoke(AktivGunScript);
		}
	}

	public void FiredWeapon()
	{
		Shot?.Invoke(1);
	}
}
