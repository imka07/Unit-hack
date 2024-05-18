using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : Enemy
{
    private NavMeshAgent AI_agent;
    private GameObject player;

    private void Start()
    {
        Init();
        AI_agent = gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        if (health > 0)
        {
            AI_agent.SetDestination(player.transform.position);
        }
    }
}
