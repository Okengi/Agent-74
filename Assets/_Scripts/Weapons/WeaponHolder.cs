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
	private void Start()
	{
        aktiveWeapon = 0;
		lastAktiv = 0;
		aktiv = weapons[0];
		changed = false;
		Debug.Log(weapons.Length);
	}
	private void Update()
	{
		WeaponHolderInput();
		if(changed && aktiveWeapon != lastAktiv)
		{
			aktiv.GetComponent<Gun>().StopReload();
			aktiv.SetActive(false);
			aktiv = weapons[aktiveWeapon];
			aktiv.SetActive(true);
			aktiv.GetComponent<Gun>().UpdateAmmoText();
			lastAktiv = aktiveWeapon;
		}
		changed = false;
	}
	void WeaponHolderInput()
    {
        float i = Input.mouseScrollDelta.y;
		if (i < 0)
		{
			aktiveWeapon--;
			changed = true;
		}
		else if (i > 0)
		{
			aktiveWeapon++;
			changed = true;
		}
		aktiveWeapon = Mathf.Clamp(aktiveWeapon, 0, weapons.Length - 1);
    }
}