using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : CharacterHealth
{
    public Slider healthBar;
    public TextMeshProUGUI healthText;
    public string[] hurtSounds;
    private bool isInvincible;

    private void UpdateHealthBar()
    {
        healthBar.value = GetHealth();
        healthText.text = GetHealth().ToString();
    }

    public override void TakeDamage(int amount)
    {
        if (!isInvincible)
        {
            base.TakeDamage(amount);
            AudioManager.instance.Play(hurtSounds);
            UpdateHealthBar();
            StartCoroutine(Invincibility());
        }
    }

    protected override void CharacterDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator Invincibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(0.1f);
        isInvincible = false;
    }
}
