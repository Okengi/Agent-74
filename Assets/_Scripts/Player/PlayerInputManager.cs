using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerInputManager : MonoBehaviour
    {
		public static PlayerInputManager instance;
		public PlayerManager player;

		PlayerContols playerControls;
		[Header("PLAYER MOVMENT INPUT")]
		[SerializeField] Vector2 movementInput;
		[HideInInspector] public float verticalInput;
		[HideInInspector] public float horizontalInput;

		[Header("CAMERA ROTATION INPUT")]
		[SerializeField] Vector2 camerInput;
		[HideInInspector] public float horizontalCamerInput;
		[HideInInspector] public float verticalCamerInput;
		

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

		private void OnApplicationFocus(bool focus)
		{
			
			if(enabled)
			{
				if (focus)
				{
					playerControls.Enable();
				}
				else
				{
					playerControls.Disable();
				}
			}
		}

		private void Update()
		{
			HandlePlayerMovementInput();
			HandleCameraRotationInput();
			player.playerAnimatorManager.UpdateAnimatorMovementParameters(horizontalInput, verticalInput);
		}

		private void HandlePlayerMovementInput()
		{
			horizontalInput = movementInput.x;
			verticalInput = movementInput.y;

			if (player == null)
				return;
			player.playerAnimatorManager.UpdateAnimatorMovementParameters(horizontalInput, verticalInput);
		}

		private void HandleCameraRotationInput()
		{
			verticalCamerInput = camerInput.y;
			horizontalCamerInput = camerInput.x;
		}
	}
}