using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Item
{
    public Collider2D swingCol;
    public SpriteRenderer swingSR;
    private LookAtMouse LAM;

    public SpriteRenderer knife2;

    private bool onCooldown;

    private Coroutine swingCoroutine = null;

    protected override void Awake()
    {
        base.Awake();
        base.SetItemName("Knife");
        LAM = GetComponentInChildren<LookAtMouse>();
    }

    public override void EquipItem()
    {
        base.EquipItem();
        PR.SetSprites(PR.GetCurrentCharacterSprites().frontOneArm1, PR.GetCurrentCharacterSprites().backOneArm1);
    }

    public override void UnequipItem()
    {
        base.UnequipItem();
        PR.SetSprites(PR.GetCurrentCharacterSprites().frontIdle, PR.GetCurrentCharacterSprites().backIdle);
        
        if (swingCoroutine != null)
        {
            StopCoroutine(swingCoroutine);
            swingCol.enabled = false;
            swingSR.enabled = false;
            LAM.enabled = true;
            onCooldown = false;
            knife2.enabled = false;
        }
    }

    public override void ItemLeftClick()
    {
        if (!onCooldown)
            swingCoroutine = StartCoroutine(Swipe());
    }

    private IEnumerator Swipe()
    {
        PR.SetSprites(PR.GetCurrentCharacterSprites().frontOneArm2, PR.GetCurrentCharacterSprites().backOneArm2);
        itemSR.enabled = false;
        knife2.enabled = true;
        AudioManager.instance.Play("KnifeSwing");
        onCooldown = true;
        swingCol.enabled = true;
        swingSR.enabled = true;
        LAM.enabled = false;

        yield return new WaitForSeconds(0.05f); //Disable the collider quickly to prevent unintended additional hits
        swingCol.enabled = false;

        yield return new WaitForSeconds(0.1f); //Disable everything else after the attack ends
        swingSR.enabled = false;
        LAM.enabled = true;
        PR.SetSprites(PR.GetCurrentCharacterSprites().frontOneArm1, PR.GetCurrentCharacterSprites().backOneArm1);
        itemSR.enabled = true;
        knife2.enabled = false;

        yield return new WaitForSeconds(0.125f); //Cooldown
        onCooldown = false;
    }
}
