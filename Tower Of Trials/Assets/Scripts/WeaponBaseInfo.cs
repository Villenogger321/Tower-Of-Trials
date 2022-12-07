using System;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Tower Of Trials/WeaponBaseInfo")]
public class WeaponBaseInfo : ScriptableObject
{
    public DamageType DmgType = DamageType.physical;
    public int Damage;
    public float FireRate;
    public float BulletSpeed;
    public float Lifetime;

    public GameObject Projectile;
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

public class Test : MonoBehaviour
{
    public Action onHit;
    void Hej(Action _subscriber)
    {
    }
    private void Start()
    {
        onHit += Subscribe;
        onHit?.Invoke();
    }
    void Subscribe()
    {

    }
}