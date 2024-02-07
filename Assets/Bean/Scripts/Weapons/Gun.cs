using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UIElements;
using MyObjects;

public class Gun : MonoBehaviour
{
    [Header("Fiering")]
    [SerializeField] protected float fireRate;
	[SerializeField] bool holedToFire;
	[SerializeField] protected float damage;
	[SerializeField] protected Camera playerCam;

    [Header("Recoil")]
    [SerializeField] public float verticalRecoil;
    [SerializeField] public float horizontalRecoil;
    [SerializeField] public float duration;

    [Header("Magazin")]
	[SerializeField] protected int magSize;
	[SerializeField] protected int ammoSize;

    [Header("Reload")]
	[SerializeField] protected float reloadTime;
    int magBefore;
    int ammoBefore;

    [Header("MuzzleFlash")]
	[SerializeField] public GameObject muzzleFlashPrefab;
	[SerializeField] public Transform muzzle;
    [SerializeField] public float muzzleFlashLiftime = 0.1f;

    [Header("UI")]
	[SerializeField] public TMPro.TextMeshProUGUI ammo;

    protected bool shooting;
	protected bool canshoot;

	protected int bulletsInMag;
    protected int bulletsInAmmo;

    protected bool realoding;

    PlayerCamera CameraScript;

	private void Start()
	{
		canshoot = true;
        bulletsInMag = magSize;
        bulletsInAmmo = ammoSize - bulletsInMag;
        realoding = false;
        CameraScript = playerCam.GetComponent<PlayerCamera>();
        UpdateAmmoText();
	}

	private void Update()
	{
        GunInput();
	}

	protected void GunInput()
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

    public virtual void Shoot()
    {
        canshoot = false;
        bulletsInMag--;
        RaycastHit rayHit;
        if(Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out rayHit, Mathf.Infinity))
        {
            if (rayHit.transform.tag == "Player")
            {
                
			}
			else
			{
				My_Object hitObject = rayHit.transform.GetComponent<My_Object>();

				if (hitObject == null)
				{
					hitObject = rayHit.transform.GetComponentInParent<My_Object>();
				}
                if(hitObject != null)
                    hitObject.Hit(damage, rayHit);
			}

		}
		Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * rayHit.distance, Color.yellow, 5);
		Invoke("ResetCanShoot", 60 / fireRate);
        ShootVisuals();
    }

    protected void ShootVisuals()
    {
		UpdateAmmoText();
		if (CameraScript != null)
			CameraScript.DoRecoil();
		HandleMuzzleFlash();
	}

	protected void HandleMuzzleFlash()
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
		ammoBefore = bulletsInAmmo;
		magBefore = bulletsInMag;
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
	public void UpdateAmmoText()
    {
        if (ammo == null) return;
        ammo.text = $"{bulletsInMag}/{bulletsInAmmo}";
    }

    public void StopReload()
    {
        if (!realoding)
            return;
        StopCoroutine(ReloadCoroutine());
        bulletsInMag = magBefore;
        bulletsInAmmo = ammoBefore;
        realoding = false;
    }
}
