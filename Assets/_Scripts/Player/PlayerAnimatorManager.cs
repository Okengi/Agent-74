using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	public class PlayerAnimatorManager : MonoBehaviour
	{
		PlayerManager player;
		float horizontal;
		float vertical;

		private void Awake()
		{
			player = GetComponent<PlayerManager>();
		}

		public void UpdateAnimatorMovementParameters(float horizontalValue, float verticalValue)
		{
			player.animator.SetFloat("Horizontal", horizontalValue, 0.1f, Time.deltaTime);
			player.animator.SetFloat("Vertical", verticalValue, 0.1f, Time.deltaTime);
		}
	}
}