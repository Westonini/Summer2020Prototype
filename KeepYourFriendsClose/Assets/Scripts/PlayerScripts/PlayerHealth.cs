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

    private void UpdateHealthBar()
    {
        healthBar.value = GetHealth();
        healthText.text = GetHealth().ToString();
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        UpdateHealthBar();
    }

    protected override void CharacterDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
