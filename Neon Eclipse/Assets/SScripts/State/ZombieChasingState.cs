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


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
            // -- Initialization -- \\
            Debug.Log("player chasinge girdi");
            player = GameObject.FindGameObjectWithTag("Player").transform;
            agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();

            agent.speed = chaseSpeed;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

            Debug.Log("giriþ yapýlmýþtýr");
            // -- Chasing Sound -- \\
            if (SoundManager.Instance.enemyChannel.isPlaying == false)
            {
                SoundManager.Instance.enemyChannel.clip = SoundManager.Instance.enemyChase;
                SoundManager.Instance.enemyChannel.PlayDelayed(0f);
            }

            agent.SetDestination(player.position);
            animator.transform.LookAt(player);

            float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
            // -- Checking if the agent should stop Chasing -- \\
            if (distanceFromPlayer > stopChasingDistance)
            {
                animator.SetBool("isChasing", false);
            }

            // -- Checking if the agent should Attack -- \\
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
