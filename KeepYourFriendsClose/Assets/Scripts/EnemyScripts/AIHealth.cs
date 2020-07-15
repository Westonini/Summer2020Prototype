using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class AIHealth : CharacterHealth
{
    private AIStateManager stateManager;
    public Transform inactiveEntities;
    public AudioSource[] hurtSounds;

    [Space]
    public Light2D flashlight;
    public SpriteRenderer weapon;

    protected override void Awake()
    {
        base.Awake();
        stateManager = GetComponentInParent<AIStateManager>();
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        int random = UnityEngine.Random.Range(0, hurtSounds.Length);
        hurtSounds[random].PlayOneShot(hurtSounds[random].clip);
    }

    protected override void CharacterDeath()
    {
        if (stateManager.GetState() != AIStateManager.State.Dead)
            StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        if (flashlight != null) { flashlight.enabled = false; }
        if (weapon != null) { weapon.enabled = false; }
        stateManager.SetState(AIStateManager.State.Dead);
        anim.SetBool("Dead", true);
        yield return new WaitForSeconds(10f);
        transform.parent.SetParent(inactiveEntities);
        transform.parent.gameObject.SetActive(false);
    }
}
