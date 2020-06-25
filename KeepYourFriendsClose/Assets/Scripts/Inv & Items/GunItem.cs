using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunItem : Item
{
    public int shootPower;
    public float shootCooldown;
    public int clipSize;
    public float reloadTime;

    protected virtual void Start()
    {
        bulletCount = clipSize;
    }

    [Space]
    public Rigidbody2D projectilePrefab;
    public Transform shootPoint;

    [Space]
    public GameObject projectileHolder;

    private int bulletCount;
    private bool onCooldown;
    private bool clipEmpty;
    private bool currentlyReloading;
    private Coroutine reloadCoroutine = null;

    public override void UnequipItem()
    {
        base.UnequipItem();

        //When the gun item gets unequipped, cancel the reload coroutine.
        if (reloadCoroutine != null)
        {
            StopCoroutine(reloadCoroutine);
            currentlyReloading = false;
        }
    }

    public override void ItemLeftClick()
    {
        if (!onCooldown && !currentlyReloading)
        {
            if (!clipEmpty)
            {
                //Create a projectile and shoot it
                Rigidbody2D projectileInstance;
                projectileInstance = Instantiate(projectilePrefab, shootPoint.position, projectilePrefab.transform.rotation) as Rigidbody2D;
                projectileInstance.velocity = shootPoint.up * shootPower;

                //Parent the projectile with the projectile holder object
                projectileInstance.transform.parent = projectileHolder.transform;

                //Destroy the projectile and remove it from the projectileInstance list in x seconds
                StartCoroutine(DestroyProjectile(1.5f, projectileInstance.gameObject));

                //Do a short cooldown after shooting
                StartCoroutine("ShootCooldown");

                //Subtract a bullet from the bullet count
                bulletCount--;
                if (bulletCount <= 0)
                    clipEmpty = true;

                //Gun Shoot Sound

            }
            else
            {
                //Clip Empty Sound

            }
        }
    }

    public override bool IsItemReloadable()
    {
        return true;
    }

    public override void Reload()
    {
        //If the current amount of bullets are less than the max in the clip AND the player isn't already reloading, reload the weapon
        if (bulletCount < clipSize && !currentlyReloading)
            reloadCoroutine = StartCoroutine(ReloadTimer(reloadTime));
    }

    IEnumerator ReloadTimer(float timer)
    {
        currentlyReloading = true;
        yield return new WaitForSeconds(timer);
        bulletCount = clipSize;
        clipEmpty = false;
        currentlyReloading = false;
    }

    IEnumerator ShootCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(shootCooldown);
        onCooldown = false;
    }

    IEnumerator DestroyProjectile(float timer, GameObject projectile)
    {
        yield return new WaitForSeconds(timer);

        Destroy(projectile);
    }
}
