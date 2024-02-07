using UnityEngine;
using Unity.Netcode;

namespace Player
{
	public class PlayerManager : NetworkBehaviour
	{
		public PlayerAnimatorManager playerAnimatorManager;
		public PlayerLocomotionManager playerLocomotionManager;
		[HideInInspector] public CharacterController characterController;
		[HideInInspector] public Animator animator;

		

		public PlayerNetworkManager playerNetworkManager;

		private void Awake()
		{
			DontDestroyOnLoad(this);
			playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
			characterController = GetComponent<CharacterController>();
			playerNetworkManager = GetComponent<PlayerNetworkManager>();
			animator = GetComponent<Animator>();
			playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
		}



		public override void OnNetworkSpawn() 
		{
			base.OnNetworkSpawn();
			if (IsOwner)
			{
				PlayerCamera1.instance.player = this;
				PlayerInputManager.instance.player = this;
				
				//playerNetworkManager.networkRotation.Value = Quaternion.identity;
			}
			else
			{
				//playerLocomotionManager.SetRotation(playerNetworkManager.networkRotation.Value);
				//PlayerCamera1.instance.SetRotation(playerNetworkManager.networkRotation.Value);
			}
			Debug.Log("Reset Rotation");
			transform.rotation = Quaternion.Euler(0, 0, 0);
			PlayerCamera1.instance.ResetRotation();
			
		}

		private void Update()
		{
			if (IsOwner)
			{
				//playerNetworkManager.networkPostion.Value = transform.position;
				//playerNetworkManager.networkRotation.Value = transform.rotation;
			}
			else
			{
				/*transform.position = Vector3.SmoothDamp(
					transform.position,
					playerNetworkManager.networkPostion.Value,
					ref playerNetworkManager.networkPositonVelocity,
					playerNetworkManager.networkPositonSmoothTime);
				transform.rotation = Quaternion.Slerp(
					transform.rotation,
					playerNetworkManager.networkRotation.Value,
					playerNetworkManager.networkPositonSmoothTime);*/
			}
			if(!IsOwner)
				return;

			playerLocomotionManager.HandleAllMovement();
			PlayerCamera1.instance.HandleAllCameraActions();
			playerLocomotionManager.HandleAllRotation();
		}
	}
}