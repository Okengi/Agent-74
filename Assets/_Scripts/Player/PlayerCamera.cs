using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	[Range(0.1f, 800f), SerializeField] float sensitivity = 400f;
	[Tooltip("Limits vertical Camera rotation."), Range(0f, 90f), SerializeField] float rotationLimit = 90f;
	bool invertedX= false;
	bool invertedY = false;


	float recoilIntensity;
	float horizontalRecoil;
	float duration;
	float time;
	float reverseTimer;
	int counter = 0;

	public Transform orientation;

    float xRotation;
    float yRotation;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}

	private void Update()
	{
		float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity;
		float mouseY = Input.GetAxisRaw("Mouse Y")* Time.deltaTime * sensitivity;

		if (invertedX)
			mouseX = -mouseX;
		if (invertedY)
			mouseY = -mouseY;

		yRotation += mouseX;
		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -90f, 90f);

		if (time > 0)
		{
			ApplyRecoil();
		}
		if (reverseTimer > 0 && time <= 0)
		{
			xRotation += (recoilIntensity * Time.deltaTime) / duration * counter / 2;
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

		//transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
		transform.rotation = Quaternion.Euler(transform.rotation.x + xRotation, transform.rotation.y + yRotation, transform.rotation.z);
		orientation.rotation = Quaternion.Euler(0, orientation.rotation.y + yRotation, 0);
	}

	void ApplyRecoil()
	{
		xRotation -= (recoilIntensity * Time.deltaTime) / duration;
		yRotation -= (Random.Range(-horizontalRecoil, horizontalRecoil) * Time.deltaTime) / duration;
		time -= Time.deltaTime;
		if (time <= 0)
			Invoke("SetRevers", duration);
	}

	void SetRevers()
	{
		if (time <= 0)
		{
			reverseTimer = duration * 2;
		}
	}

	public void NewWeapon(float vertical, float horizontal, float duration)
	{
		recoilIntensity = vertical;
		horizontalRecoil = horizontal;
		this.duration = duration;
	}

	public void DoRecoil()
	{
		counter++;
		time = duration;
	}
}