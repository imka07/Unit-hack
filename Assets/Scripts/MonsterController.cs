using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : Enemy
{
    private NavMeshAgent AI_Agent;
    private GameObject Player;

    public Transform[] WayPoints;
    public int Current_Patch;

    public enum AI_State { Patrol, Stay, ChaseAndPatrol, Chase };
    public AI_State AI_Enemy;

    void Start()
    {
        Init();
        AI_Agent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    [System.Obsolete]
    void FixedUpdate()
    {
        if (AI_Enemy == AI_State.Patrol)
        {
            AI_Agent.Resume();
            anim.SetBool("isWalking", true);
            AI_Agent.SetDestination(WayPoints[Current_Patch].transform.position);
            float Patch_Dist = Vector3.Distance(WayPoints[Current_Patch].transform.position, gameObject.transform.position);
            if (Patch_Dist < 2)
            {
                Current_Patch++;
                Current_Patch = Current_Patch % WayPoints.Length;
            }
        }
        if (AI_Enemy == AI_State.Stay)
        {
            anim.SetBool("isWalking", false);
            AI_Agent.Stop();
        }

        if (AI_Enemy == AI_State.ChaseAndPatrol)
        {
            anim.SetBool("isWalking", true);
            AI_Agent.SetDestination(Player.transform.position);
        }


        float Dist_Player = Vector3.Distance(Player.transform.position, gameObject.transform.position);
        if (Dist_Player < 4)
        {
            AI_Agent.Stop();
            anim.SetBool("isWalking", false);
        }
    }

}

