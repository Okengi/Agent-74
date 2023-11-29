using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class My_Object_Data : ScriptableObject
{
	public bool damagble = false;
	public bool interactable = false;
	public float Health = 1000f;

	public GameObject impactPrefap;
	public GameObject holePrefap;
	public float holeLifetime = 15f;
}
