using UnityEngine;
using Unity.Netcode;

namespace Player
{
	public class PlayerManager : NetworkBehaviour
	{
		PlayerLocomotionManager playerLocomotionManager;
		public CharacterController characterController;

		private void Awake()
		{
			DontDestroyOnLoad(this);
			playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
			characterController = GetComponent<CharacterController>();
		}

		private void Start()
		{
			PlayerCamera1.instance.playerManager = this;
		}

		private void Update()
		{
			playerLocomotionManager.HandleAllMovement();
		}

		private void LateUpdate()
		{
			PlayerCamera1.instance.HandleAllCameraActions();
			playerLocomotionManager.HandleAllRotation();
		}
	}
}