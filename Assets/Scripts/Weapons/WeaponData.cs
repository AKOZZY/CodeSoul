using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    normal,
    shotgun,
    melee,
    special,
    explosive
}

[CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObjects/Weapon")]
public class WeaponData : ScriptableObject
{
    // Gun Identity
    public int id;
    public int price;
    public string title;
    public string upgradedVariantOf;
    public string description;
    public Sprite sprite;
    public Sprite bullet;
    public GameObject projectile;
    public RuntimeAnimatorController controller;
    public WeaponType type;
    public AudioClip sound;
    public bool isUpgraded;

    // Gun Stats
    public int damage;
    public int ammo;
    public int magazine;
    public int shellsPerShot;
    public float reload;
    public float firerate;
}
