using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerInputManager : MonoBehaviour
    {
		public static PlayerInputManager instance;
		PlayerContols playerControls;
		[Header("PLAYER MOVMENT INPUT")]
		[SerializeField] Vector2 movementInput;
		[SerializeField] public float verticalInput;
		[SerializeField] public float horizontalInput;
		[SerializeField] public float moveAmount;

		[Header("CAMERA ROTATION INPUT")]
		[SerializeField] Vector2 camerInput;
		[SerializeField] public float verticalCamerInput;
		[SerializeField] public float horizontalCamerInput;

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
			SceneManager.activeSceneChanged += OnSceneChanged;
			instance.enabled = false;
		}

		private void OnSceneChanged(Scene oldScene, Scene newScene)
		{
			if (newScene.buildIndex ==WorldSaveGameManager.instance.worldsceneIndex)
			{
				instance.enabled = true;
			}
			else
			{
				instance.enabled = false;
			}
		}

		private void OnEnable()
		{
			if (playerControls == null )
			{ 
				playerControls = new PlayerContols();
				playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
				playerControls.CameraRotation.Rotation.performed += i => camerInput = i.ReadValue<Vector2>();
			}
			playerControls.Enable();
		}

		private void OnDestroy()
		{
			SceneManager.activeSceneChanged -= OnSceneChanged;
		}

		private void Update()
		{
			HandlePlayerMovementInput();
			HandleCameraRotationInput();
		}

		private void HandlePlayerMovementInput()
		{
			verticalInput = movementInput.y;
			horizontalInput = movementInput.x;

			moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

			// Clamp
			if (moveAmount <= 0.5 && moveAmount > 0)
			{
				moveAmount = 0.5f;
			}
			else if (moveAmount > 0.5 && moveAmount <= 1)
			{
				moveAmount = 1;
			}
		}

		private void HandleCameraRotationInput()
		{
			verticalCamerInput = camerInput.y;
			horizontalCamerInput = camerInput.x;
		}
	}
}