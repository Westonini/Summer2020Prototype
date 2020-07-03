using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterHealth : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;

    public int knockbackResist;

    private Rigidbody2D rb;

    protected virtual void Awake() { rb = GetComponentInParent<Rigidbody2D>(); }
    protected virtual void Start() { currentHealth = maxHealth; }
  
    public int GetHealth() { return currentHealth; }
    public void SetHealth(int amount)
    {
        if (amount > maxHealth)
            currentHealth = maxHealth;
        else if (amount < 0)
            currentHealth = 0;
        else
            currentHealth = amount;
    }

    public virtual void TakeDamage(int amount)
    {
        SetHealth(currentHealth - amount);

        if (currentHealth == 0)
            CharacterDeath();
    }

    public virtual void HealHealth(int amount)
    {
        SetHealth(currentHealth + amount);
    }

    protected virtual void CharacterDeath() { /*To be done in inherited classes*/ }
}
