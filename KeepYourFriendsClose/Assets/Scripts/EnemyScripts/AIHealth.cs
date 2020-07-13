using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : CharacterHealth
{
    private AIStateManager stateManager;
    public Transform inactiveEntities;
    public string[] hurtSounds;

    protected override void Awake()
    {
        base.Awake();
        stateManager = GetComponentInParent<AIStateManager>();
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        AudioManager.instance.PlayOneShot(hurtSounds);
    }

    protected override void CharacterDeath()
    {
        StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        stateManager.SetState(AIStateManager.State.Dead);
        anim.SetBool("Dead", true);
        yield return new WaitForSeconds(10f);
        transform.parent.SetParent(inactiveEntities);
        transform.parent.gameObject.SetActive(false);
    }
}
