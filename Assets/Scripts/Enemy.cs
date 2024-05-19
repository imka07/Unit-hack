using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Zombie Stats")]
    public float health;
    [SerializeField] private float maxHealth;
    public float damage;

    [Header("Zombie Components")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject deathPartices, particlesSpawnPos;
    public AudioClip[] audioClips;
    public Animator anim;

    public void Init()
    {
        health = maxHealth;
    }

    public void PlayClips(int index)
    {
        audioSource.clip = audioClips[index];
        audioSource.Play();
    }

    private void Update()
    {
        if (health <= 0)
        {
            Death();
        }
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Max(health - damage, 0);
    }
    private void Death()
    {
        Instantiate(deathPartices, particlesSpawnPos.transform.position, Quaternion.identity);
        DestroyMesh();
    }


    private void DestroyMesh()
    {
        Destroy(gameObject);
    }

}
