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
    public AudioSource audioSource;
    public Animator anim;

    private void Update()
    {
        if (!onCooldown && detectorScript.IsTargetFound())
            Attack();
    }

    protected override void Attack()
    {
        //Create a projectile
        Rigidbody2D projectileInstance;
        projectileInstance = Instantiate(projectilePrefab, shootPoint.position, projectilePrefab.transform.rotation) as Rigidbody2D;

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
        audioSource.PlayOneShot(audioSource.clip);

        //Gun Shoot Animation
        anim.SetTrigger("Shoot");
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
}
