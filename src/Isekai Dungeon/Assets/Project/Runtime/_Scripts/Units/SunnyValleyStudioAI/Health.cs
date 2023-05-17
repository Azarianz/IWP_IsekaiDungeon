using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int currentHealth, maxHealth;

    [SerializeField]
    private bool isDead = false;

    [SerializeField]
    private Slider HP_Bar;

    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;

    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
        isDead = false;

        HP_Bar.maxValue = maxHealth;
        HP_Bar.value = currentHealth;
    }

    public void GetHit(int amount, GameObject sender)
    {
        if (isDead)
            return;
        if (sender.layer == gameObject.layer)
            return;

        currentHealth -= amount;
        HP_Bar.value = currentHealth;

        if (currentHealth > 0)
        {
            OnHitWithReference?.Invoke(sender);
        }
        else
        {
            OnDeathWithReference?.Invoke(sender);
            isDead = true;
            Destroy(gameObject);
        }

        Debug.Log(sender + " Remaining HP: " + currentHealth);
    }

    public void UpdateHealth()
    {

    }
}
