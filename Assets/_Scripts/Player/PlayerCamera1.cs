using Unity.Netcode;
using UnityEngine;

namespace Player
{
	public class PlayerCamera1 : MonoBehaviour
	{
		public static PlayerCamera1 instance;
		public PlayerManager player;
		public Camera cameraObject;
		
		[Header("Camera Settings")]
		[SerializeField] public float horizontalCameraSpeed = 200f;
		[SerializeField] private float verticalCameraSpeed = 100f;
		[SerializeField, Range(0, 90)] private float downMaxLookAngle = 90f;
		[SerializeField, Range(0, 90)] private float upMaxLookAngle = 90f;
		private float cameraSmoothTime = 1;

		[Header("Camera Values")]
		private Vector3 cameraVelocity;
		private float horizontal;
		private float vertical;

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
		private void Start()
		{
			DontDestroyOnLoad(gameObject);
		}

		public void HandleAllCameraActions()
		{
			if(player == null)
				return;
			HandleFollowPlayer();
			HandleRotation();
		}

		private void HandleFollowPlayer()
		{
			Vector3 playerPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraVelocity, cameraSmoothTime * Time.deltaTime);
			transform.position = playerPosition;
		}

		private void HandleRotation()
		{
			horizontal += (PlayerInputManager.instance.horizontalCamerInput * horizontalCameraSpeed) * Time.deltaTime;
			vertical -= (PlayerInputManager.instance.verticalCamerInput * verticalCameraSpeed) * Time.deltaTime;
			vertical = Mathf.Clamp(vertical, -upMaxLookAngle, downMaxLookAngle);

			Vector3 cameraRotation = Vector3.zero;
			cameraRotation.y = horizontal;

			Quaternion targetRotation = Quaternion.Euler(cameraRotation);
			transform.rotation = targetRotation;

			cameraRotation = Vector3.zero;
			cameraRotation.x = vertical;

			targetRotation = Quaternion.Euler(cameraRotation);
			cameraObject.transform.localRotation = targetRotation;
		}

		public void ResetRotation()
		{
			transform.rotation = player.transform.rotation;
			cameraObject.transform.localRotation = Quaternion.Euler(0,0,0);
		}

		public void SetRotation(Quaternion rot)
		{
			transform.rotation = Quaternion.Euler(0,0,0);
		}
	}
}