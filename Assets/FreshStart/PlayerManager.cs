using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	public class PlayerManager : MonoBehaviour
	{
		public static PlayerManager instance;
		PlayerLocomotionManager playerLocomotionManager;
		public CharacterController characterController;

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