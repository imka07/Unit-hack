using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public delegate void OnHpChangeHandler(float maxHp, float currentHp);
    public OnHpChangeHandler OnHpChange;

    private float health;
    public float maxHealth;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
            health = Mathf.Max(health - damage, 0);
            OnHpChange?.Invoke(maxHealth, health);
            if (health == 0)
            {
                GameManager.instance.GameOver();
            }
    }
}
