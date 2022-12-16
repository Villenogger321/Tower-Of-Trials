using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Tower Of Trials/WeaponBaseInfo")]
public class WeaponBaseInfo : ScriptableObject
{
    public DamageType DmgType = DamageType.physical;
    public List<OnHitBase> onHit;
    public int Damage;
    public float FireRate;
    public float BulletSpeed;
    public float Lifetime;

    public Projectile Projectile;
    public Sprite ProjectileSprite;
    public Sprite WeaponSprite;
}
[Flags]
public enum DamageType
{
    physical = 1,
    water = 2,
    fire = 4,
    poison = 8,
    explosive = 16
}