using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	public class PlayerLocomotionManager : MonoBehaviour
	{
		PlayerManager player;
		
		[Header("MOVEMENT")]
		public float verticalMovement;
		public float horizontalMovement;
		public float moveAmount;

		[Header("ROTATION")]
		float horizontalCamerRotation;
		float horizontalRotation;

		private Vector3 moveDirection;

		[Header("Locomotion Settings")]
		[SerializeField] float walkingSpeed = 2;

		private void Awake()
		{
			player = GetComponent<PlayerManager>();
		}

		public void HandleAllMovement()
		{
			HandleGroundMovement();
		}

		private void GetVerticalAndHorizontalInputs()
		{
			verticalMovement = PlayerInputManager.instance.verticalInput;
			horizontalMovement = PlayerInputManager.instance.horizontalInput;

			//Clamp Movement
		}

		private void GetRotationInput()
		{
			horizontalCamerRotation = PlayerInputManager.instance.horizontalCamerInput;
		}

		private void HandleGroundMovement()
		{
			GetVerticalAndHorizontalInputs();
			// this means when pressing forward you walke in the direction the Camera looks not the Charakter
			moveDirection = PlayerCamera1.instance.transform.forward * verticalMovement;
			moveDirection = moveDirection + PlayerCamera1.instance.transform.right * horizontalMovement;
			moveDirection.Normalize();
			moveDirection.y = 0;

			player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
		}

		public void HandleAllRotation()
		{
			GetRotationInput();
			horizontalRotation += (horizontalCamerRotation * PlayerCamera1.instance.horizontalCameraSpeed) * Time.deltaTime;

			Vector3 targetRotationDirection = Vector3.zero;
			targetRotationDirection.y = horizontalRotation;

			Quaternion targetRotation = Quaternion.Euler(targetRotationDirection);
			transform.rotation = targetRotation;
		}
	}
}