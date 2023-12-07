using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace Oki
{
	public class PlayerCamera1 : MonoBehaviour
	{
		public static PlayerCamera1 instance;
		public PlayerManager playerManager;
		public Camera cameraObject;
		
		[Header("Camera Settings")]
		[SerializeField] public float horizontalCameraSpeed = 200f;
		[SerializeField] private float verticalCameraSeed = 100f;
		[SerializeField, Range(-180f, 180)] private float downMaxLookAngle = -90f;
		[SerializeField, Range(-180f, 180)] private float upMaxLookAngle = 90f;
		private float cameraSmoothTime = 1;

		[Header("Camera Values")]
		[SerializeField] private Vector3 cameraVelocity;
		[SerializeField] private float horizontal;
		[SerializeField] private float vertical;

		private void Awake()
		{
			if (instance == null)
			{
				instance = this;
			}
			else
			{
				Destroy(gameObject);
			}
		}

		public void HandleAllCameraActions()
		{
			if(playerManager == null)
				return;
			HandleFollowPlayer();
			HandleRotation();
		}

		private void HandleFollowPlayer()
		{
			Vector3 playerPosition = Vector3.SmoothDamp(transform.position, playerManager.transform.position, ref cameraVelocity, cameraSmoothTime * Time.deltaTime);
			transform.position = playerPosition;
		}

		private void HandleRotation()
		{
			horizontal += (PlayerInputManager.instance.horizontalCamerInput * horizontalCameraSpeed) * Time.deltaTime;
			vertical -= (PlayerInputManager.instance.verticalCamerInput * verticalCameraSeed) * Time.deltaTime;
			vertical = Mathf.Clamp(vertical, downMaxLookAngle, upMaxLookAngle);

			Vector3 cameraRotation = Vector3.zero;
			cameraRotation.y = horizontal;

			Quaternion targetRotation = Quaternion.Euler(cameraRotation);
			transform.rotation = targetRotation;

			cameraRotation = Vector3.zero;
			cameraRotation.x = vertical;

			targetRotation = Quaternion.Euler(cameraRotation);
			cameraObject.transform.localRotation = targetRotation;
		}
	}
}