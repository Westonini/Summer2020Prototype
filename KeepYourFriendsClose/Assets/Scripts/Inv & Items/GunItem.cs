using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunItem : Item
{
    public int shootPower;
    public float shootCooldown;
    public int clipSize;
    public float reloadTime;
    public string[] shootSounds;
    public string reloadSound;
    private Animator anim;
    private GunItemUI GIU;
    private ScreenShake SS;
    private Camera mainCam;

    public Transform playerTrans;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        GIU = gameObject.GetComponent<GunItemUI>();
        SS = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScreenShake>();
        mainCam = Camera.main;
    }

    protected virtual void Start()
    {
        bulletCount = clipSize;
        GIU.SetClipSize(clipSize);
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

    public override void EquipItem()
    {
        base.EquipItem();

        GIU.EnableAmmoSprites();
    }

    public override void UnequipItem()
    {
        base.UnequipItem();

        //When the gun item gets unequipped, cancel the reload coroutine.
        if (reloadCoroutine != null)
        {
            StopCoroutine(reloadCoroutine);
            currentlyReloading = false;
            GIU.reloadUI.HideReloadBar();
            AudioManager.instance.Stop(reloadSound);
        }

        GIU.DisableAmmoSprites();
    }

    public override void ItemLeftClick()
    {
        if (!onCooldown && !currentlyReloading)
        {
            if (!clipEmpty)
            {
                //Create a projectile
                Rigidbody2D projectileInstance;
                projectileInstance = Instantiate(projectilePrefab, shootPoint.position, projectilePrefab.transform.rotation) as Rigidbody2D;

                //Rotate the projectile
                projectileInstance.transform.up = ((Vector2)(mainCam.ScreenToWorldPoint(Input.mousePosition)) - (Vector2)transform.position).normalized;

                //Set the shooter variable in Projectile to the object that shot the projectile
                Projectile proj = projectileInstance.gameObject.GetComponent<Projectile>();
                proj.SetShooter(playerTrans);

                //Shoot the projectile
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

                //Play shoot sound
                AudioManager.instance.PlayOneShot(shootSounds);

                //Darken one bullet on the visual ammo sprites for the player
                GIU.DarkenOneBullet();

                //Light Screen Shake
                SS.DoScreenShake(1);

                //Gun Shoot Animation
                anim.SetTrigger("Shoot");
            }
            else
            {
                //Automatically start Reload
                Reload();

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
        AudioManager.instance.Play(reloadSound);
        GIU.reloadUI.ShowReloadBar(timer);
        currentlyReloading = true;
        yield return new WaitForSeconds(timer);
        bulletCount = clipSize;
        GIU.ResetAllBullets();
        GIU.reloadUI.HideReloadBar();
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
