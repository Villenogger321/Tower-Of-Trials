using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeReference]
    public List<OnHitBase> OnHit;

    public WeaponBaseInfo weaponInfo;
    [SerializeField] Projectile projectile;

    public void FireProjectile()
    {
        Projectile proj =  Instantiate(projectile);
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();

        rb.AddForce(Vector2.left);  // fix shoot to cursor

        proj.AssignToProjectile(TriggerOnHit);
        foreach (var onHitBase in OnHit)
        {
            proj.AssignToProjectile(onHitBase.OnHit);
        }
    }
    void TriggerOnHit(Health _health)
    {
        _health.TakeDamage(weaponInfo.Damage, weaponInfo.DmgType);
    }
    void OnFire()
    {
        // implemenet cooldown :-)
        FireProjectile();
    }
}
