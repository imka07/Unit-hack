using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTrigger : MonoBehaviour
{
    GameObject Player;
    public AudioSource biteSound;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void PlayerDamage()
    {
        Player.GetComponent<PlayerManager>().TakeDamage(25);
        biteSound.Play();
    }
}
