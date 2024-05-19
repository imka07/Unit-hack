using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public delegate void OnHpChangeHandler(float maxHp, float currentHp);
    public OnHpChangeHandler OnHpChange;
    public AudioClip[] FootstepAudioClips;
    public AudioSource audioSource;

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


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Battery")
        {
            GameManager.instance.AddBattaries();
            Destroy(other.gameObject);
        }
    }

    public void OnFootstep()
    {
        if (FootstepAudioClips.Length > 0)
        {
            var index = Random.Range(0, FootstepAudioClips.Length);
            audioSource.clip = FootstepAudioClips[index];
            audioSource.Play();

        }
    }
}
