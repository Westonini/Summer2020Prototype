using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRangedAttack : AIAttack
{
    [Space]
    public int shootPower;
    public Rigidbody2D projectilePrefab;
    public Transform shootPoint;

    [Space]
    public GameObject projectileHolder;
    public Transform characterTrans;
    public AudioSource shootAudioSource;
    public Animator anim;

    [Space]
    public bool isReloadable;
    public int clipSize = 0;
    public float reloadTime;
    public ReloadUI reloadUI;
    public AudioSource reloadAudioSource;
    private int bulletCount;
    private bool clipEmpty;

    private void Start()
    {
        bulletCount = clipSize;
    }

    private void Update()
    {
        if (!onCooldown && detectorScript.IsTargetFound())
            Attack();
    }

    protected override void Attack()
    {
        if (!clipEmpty)
        {
            //Create a projectile
            Rigidbody2D projectileInstance;
            projectileInstance = Instantiate(projectilePrefab, shootPoint.position, projectilePrefab.transform.rotation) as Rigidbody2D;

            //Rotate the projectile
            projectileInstance.transform.up = (detectorScript.GetTarget().position - transform.position).normalized;

            //Set the shooter variable in Projectile to the object that shot the projectile
            Projectile proj = projectileInstance.gameObject.GetComponent<Projectile>();
            proj.SetShooter(characterTrans);

            //Shoot the projectile
            projectileInstance.velocity = shootPoint.up * shootPower;

            //Parent the projectile with the projectile holder object
            projectileInstance.transform.parent = projectileHolder.transform;

            //Destroy the projectile and remove it from the projectileInstance list in x seconds
            StartCoroutine(DestroyProjectile(1.5f, projectileInstance.gameObject));

            //Do a short cooldown after shooting
            StartCoroutine("AttackCooldown");

            //Play shoot sound
            shootAudioSource.PlayOneShot(shootAudioSource.clip);

            //Shoot Animation
            anim.SetTrigger("Shoot");

            //Subtract a bullet from the bullet count if the weapon is reloadable
            if (isReloadable)
            {
                bulletCount--;

                if (bulletCount <= 0) //Reload if at 0 bullets
                {
                    clipEmpty = true;
                    StartCoroutine(ReloadTimer(reloadTime));
                }
            }
        }
    }

    protected override IEnumerator AttackCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(Random.Range(cooldownDuration, cooldownDuration + 0.5f));
        onCooldown = false;
    }

    IEnumerator DestroyProjectile(float timer, GameObject projectile)
    {
        yield return new WaitForSeconds(timer);

        Destroy(projectile);
    }

    IEnumerator ReloadTimer(float timer)
    {
        yield return new WaitForSeconds(0.15f);
        reloadAudioSource.PlayOneShot(reloadAudioSource.clip);
        reloadUI.ShowReloadBar(timer);
        yield return new WaitForSeconds(timer);
        bulletCount = clipSize;
        reloadUI.HideReloadBar();
        clipEmpty = false;
    }
}
