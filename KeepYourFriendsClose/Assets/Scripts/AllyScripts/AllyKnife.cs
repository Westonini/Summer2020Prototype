using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyKnife : MonoBehaviour
{
    public AIMeleeAttack atkScript;
    public AllyRender render;
    public SpriteRenderer knifeSwipe;

    public SpriteRenderer knife1;
    public SpriteRenderer knife2;

    public AudioSource AS;

    private void OnEnable()
    {
        atkScript._attacked += AttackVisuals;
    }
    private void OnDisable()
    {
        atkScript._attacked -= AttackVisuals;
    }

    private void AttackVisuals()
    {
        StartCoroutine(AtkVisuals());
    }

    private IEnumerator AtkVisuals()
    {
        AS.PlayOneShot(AS.clip);
        render.SetSprites(render.GetCurrentCharacterSprites().frontOneArm2, render.GetCurrentCharacterSprites().backOneArm2);
        knife1.enabled = false;
        knife2.enabled = true;
        knifeSwipe.enabled = true;
        yield return new WaitForSeconds(0.15f);
        render.SetSprites(render.GetCurrentCharacterSprites().frontOneArm1, render.GetCurrentCharacterSprites().backOneArm1);
        knife1.enabled = true;
        knife2.enabled = false;
        knifeSwipe.enabled = false;
    }
}
