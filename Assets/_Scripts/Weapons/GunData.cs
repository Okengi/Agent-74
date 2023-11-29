using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.Toolbars;
using UnityEngine;

[CreateAssetMenu]
public class GunData : ScriptableObject
{
	[Header("Fiering")]
	public float fireRate;
	public bool holedToFire;
	public float damage;

	[Header("Magazin")]
	public int magSize;
	public int amountBullets;

	[Header("Reload")]
	public float reloadTime;
}
