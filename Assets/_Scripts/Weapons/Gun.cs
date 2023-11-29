using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UIElements;

public class Gun : MonoBehaviour
{
    [Header("Fiering")]
    public float fireRate;
    public bool holedToFire;
    public float damage;
    public Camera playerCam;

    [Header("Magazin")]
    public int magSize;
    public int ammoBullets;

    [Header("Reload")]
    public float reloadTime;

    [Header("MuzzleFlash")]
	public GameObject muzzleFlashPrefab;
    public Transform muzzle;
    public float muzzleFlashLiftime = 0.1f;

    [Header("Bullet Hole")]
    public GameObject bulletHolePrefab;
    public GameObject impactPrefab;

    [Header("Visuals")]
	public TMPro.TextMeshProUGUI ammo;

    bool shooting;
    bool canshoot;

    int bulletsInMag;
    int bulletsInAmmo;

    bool realoding;

	private void Start()
	{
		canshoot = true;
        bulletsInMag = magSize;
        bulletsInAmmo = ammoBullets - bulletsInMag;
        realoding = false;
        UpdateAmmoText();
	}

	private void Update()
	{
        GunInput();
	}

	void GunInput()
    {
        if (realoding)
            return;

        if (holedToFire) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyUp(KeyCode.Mouse0);

		if (Input.GetKeyDown(KeyCode.R))
		{
			Reload();
		}

		if (shooting && canshoot && bulletsInMag != 0)
        {
		    Shoot();
        }
    }

    void Shoot()
    {
        canshoot = false;
        bulletsInMag--;
        UpdateAmmoText();
        RaycastHit rayHit;
        if(Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out rayHit, Mathf.Infinity))
        {
            if (rayHit.transform.tag == "Player")
            {
                Debug.Log(rayHit.transform.name);
			}
            else
            {
				try
                {
					rayHit.transform.gameObject.GetComponent<My_Object>().Hit(damage, rayHit);
				}
                catch
                {
                    try
                    {
                        rayHit.transform.gameObject.GetComponentInParent<My_Object>().Hit(damage, rayHit);
                    }
                    catch { }
                }
				
			}
           
        }
        HandleMuzzleFlash();

		Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * rayHit.distance, Color.yellow, 5);
		Invoke("ResetCanShoot", 60 / fireRate);
    }

    void HandleMuzzleFlash()
    {
		GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, muzzle);
        // Rotation
		var euler = muzzleFlash.transform.eulerAngles;
		euler.z += Random.Range(0.0f, 360.0f);

		muzzleFlash.transform.eulerAngles = euler;
        // Scale
        var scale = muzzleFlash.transform.localScale;
        float sc = 0.02f;
        scale.x += Random.Range(-sc, sc);
		scale.y += Random.Range(-sc, sc);
		scale.z += Random.Range(-sc, sc);
        muzzleFlash.transform.localScale = scale;

		Destroy(muzzleFlash, muzzleFlashLiftime);
	}

    void ResetCanShoot()
    {
        canshoot = true;
    }

    void Reload()
    {
        realoding = true;
        StartCoroutine(ReloadCoroutine());
    }
    IEnumerator ReloadCoroutine()
    {
       for (int i = 0; i < magSize; i++)
       {
            if (bulletsInAmmo <= 0 || bulletsInMag >= magSize) 
            {
                break;
            }
            bulletsInAmmo--;
            bulletsInMag++;
            UpdateAmmoText();
            yield return new WaitForSeconds(reloadTime/magSize);
       }
       realoding = false;
    }
    void UpdateAmmoText()
    {
        if (ammo == null) return;
        ammo.text = $"{bulletsInMag}/{bulletsInAmmo}";
    }
}
