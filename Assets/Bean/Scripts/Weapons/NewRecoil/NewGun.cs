using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyObjects;

public class NewGun : MonoBehaviour
{
	[Header("Fiering")]
	[SerializeField] protected float fireRate;
	[SerializeField] bool holedToFire;
	[SerializeField] protected float damage;
	[SerializeField] Transform shotDirection;


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

	private void Start()
	{
		canshoot = true;
		bulletsInMag = magSize;
		bulletsInAmmo = ammoSize - bulletsInMag;
		realoding = false;
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
		if (Physics.Raycast(shotDirection.transform.position, shotDirection.transform.forward, out rayHit, Mathf.Infinity))
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
				if (hitObject != null)
					hitObject.Hit(damage, rayHit);
			}

		}
		Debug.DrawRay(shotDirection.transform.position, shotDirection.transform.forward * rayHit.distance, Color.yellow, 5);
		Invoke("ResetCanShoot", 60 / fireRate);
		ShootVisuals();
	}

	protected void ShootVisuals()
	{
		UpdateAmmoText();
		HandleMuzzleFlash();
		WeaponController.instance.FiredWeapon();
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
			yield return new WaitForSeconds(reloadTime / magSize);
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

	public float[] RecoilData()
	{
		float[] s = new float[2];
		s[0] = verticalRecoil;
		s[1] = horizontalRecoil;
		return s;
	}
}
