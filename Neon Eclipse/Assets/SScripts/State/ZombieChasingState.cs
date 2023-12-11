using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieChasingState : StateMachineBehaviour
{
    Transform player;
    UnityEngine.AI.NavMeshAgent agent;

    public float chaseSpeed = 6f;

    public float stopChasingDistance = 21f;
    public float attackingDistance = 2.5f;

    int previousLayer = 6; 
    public bool takip = true;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // -- Initialization -- \
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();

        agent.speed = chaseSpeed;
        previousLayer = player.gameObject.layer; 
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int currentLayer = player.gameObject.layer; 

        
        if (currentLayer != 6)
        {
            
            takip = false;
        }
        else
        {
            takip = true;
        }

        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

        if (takip)
        {
            // -- Chasing Sound -- \
            if (SoundManager.Instance.enemyChannel.isPlaying == false)
            {
                SoundManager.Instance.enemyChannel.clip = SoundManager.Instance.enemyChase;
                SoundManager.Instance.enemyChannel.PlayDelayed(0f);
            }
            agent.SetDestination(player.position);
            animator.transform.LookAt(player);

            // -- Checking if the agent should stop Chasing -- \
            if (distanceFromPlayer > stopChasingDistance)
            {
                animator.SetBool("isChasing", false);
            }
        }
        else if (!takip)
        {
            animator.SetBool("isChasing", false);
        }

        // -- Checking if the agent should Attack -- \
        if (distanceFromPlayer < attackingDistance)
        {
            animator.SetBool("isAttacking", true);
        }
    }



    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(animator.transform.position);
    }

}