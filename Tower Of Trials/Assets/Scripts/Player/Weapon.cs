using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeReference]
    public List<OnHitBase> OnHit;

    public WeaponBaseInfo WeaponInfo;
    [SerializeField] Transform firePos;

    [SerializeField] Transform weaponHolder;


    #region FMOD var
    [Header("FMOD")]
    public FMODUnity.EventReference attackRef;
    private FMOD.Studio.EventInstance attackInstance;
    #endregion

    public void FireProjectile()
    {
        Projectile proj =  Instantiate(WeaponInfo.Projectile);
        proj.transform.position = firePos.position;
        proj.transform.rotation = Quaternion.Euler(0, 0, ProjectileDirection(proj));

        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        rb.AddForce(proj.transform.right * WeaponInfo.BulletSpeed);

        proj.AssignToProjectile(TriggerOnHit);
        proj.AssignOwner(true);

        foreach (var onHitBase in OnHit)
        {
            proj.AssignToProjectile(onHitBase.OnHit);
        }
    }
    float ProjectileDirection(Projectile _proj)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rot = weaponHolder.localPosition;
        float rotZ = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;
        return rotZ;
    }
    void TriggerOnHit(Health _health)
    {
        _health.TakeDamage(WeaponInfo.Damage, WeaponInfo.DmgType);
    }
    void OnFire()
    {
        // implemenet cooldown :-)

        ////////////////////////// Shooting sfx
        FireProjectile();

        //fire sfx
        attackInstance.start();
    }
    void Start()
    {
        OnEquip();

        attackInstance = FMODUnity.RuntimeManager.CreateInstance(attackRef); //fmod
    }
    void OnEquip()
    {
        // TODO: Equip new weapons = go here
        OnHit = WeaponInfo.onHit;
    }
}
