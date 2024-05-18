using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Zombie Stats")]
    public float speed;
    [HideInInspector] public float health;
    [SerializeField] private float maxHealth;
    protected float timeBetweenAttack;
    public float startBetweenAttack;
    public float attackRange;
    [SerializeField] private float damage;
    bool isWalking;
    bool canAttack;

    [Header("Zombie Components")]
    [SerializeField] private AudioSource audioSource;
    private Rigidbody2D rb;
    public Transform attackPos;
    public AudioClip[] audioClips;
    public Animator anim;
    public LayerMask playerMask;

    public void Init()
    {
        isWalking = true;
        //rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        health = maxHealth;
    }

    public void PlayClips(int index)
    {
        audioSource.clip = audioClips[index];
        audioSource.Play();
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Max(health - damage, 0);
        if (health <= 0)
        {
            Death();
        }
    }
    private void Death()
    {
        anim.SetTrigger("Death");
        Invoke("DestroyMesh", 2f);
    }


    private void DestroyMesh()
    {
        Destroy(gameObject);
    }

}
