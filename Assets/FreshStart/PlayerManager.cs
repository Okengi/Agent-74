using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oki
{
	public class PlayerManager : MonoBehaviour
	{
		PlayerLocomotionManager playerLocomotionManager;
		public CharacterController characterController;

		private void Awake()
		{
			playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
			characterController = GetComponent<CharacterController>();
		}

		private void Start()
		{
			PlayerCamera1.instance.playerManager = this;
		}

		private void Update()
		{
			playerLocomotionManager.HandleAllMovment();
		}

		private void LateUpdate()
		{
			PlayerCamera1.instance.HandleAllCameraActions();
			playerLocomotionManager.HandleRotation();
		}
	}
}