using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombiePathrollingState : StateMachineBehaviour
{
    public FieldOfView fieldOfView;
    float timer;
    public float patrolingTime = 10f;

    Transform player;
    UnityEngine.AI.NavMeshAgent agent;

    public float detectionArea = 18f;
    public float patrolSpeed = 2f;

    List<Transform> waypointsList = new List<Transform>(); // Can Enemy Return WayPoints


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // -- Initialization -- \\
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();

        agent.speed = patrolSpeed;
        timer = 0;

        // -- Get all waypoint and Move to First Waypoint -- \\

        Zombie zombieScript = animator.GetComponent<Zombie>(); // Zombi scriptine eriþ

        if (zombieScript != null)
        {
            // Zombie scriptindeki GameObject'e eriþim
            GameObject myObject = zombieScript.waypointsList;
            foreach (Transform t in myObject.transform) //waypoints tagli objenin altýndaki objelere enemy gider
            {
                waypointsList.Add(t);
            }
            Vector3 nextPosition = waypointsList[Random.Range(0, waypointsList.Count)].position;
            agent.SetDestination(nextPosition);
        }
        else
        {
            Debug.LogError("Zombie script not found.");
        }


    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // -- Walking Sound -- \\
        if(SoundManager.Instance.enemyChannel.isPlaying == false)
        {
            SoundManager.Instance.enemyChannel.clip = SoundManager.Instance.enemyWalking;
            SoundManager.Instance.enemyChannel.PlayDelayed(1f);
        }

        // -- If agent arrived at waypoint, move to next waypoint -- \\
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(waypointsList[Random.Range(0, waypointsList.Count)].position);
        }


        // -- Transition to Idle State -- \\
        timer += Time.deltaTime;
        if(timer > patrolingTime)
        {
            animator.SetBool("isPatrolling", false);
        }

        // FieldOfView bileþenine animator içinden eriþim
        fieldOfView = animator.GetComponent<FieldOfView>();
        // -- Transition the Chase State -- \\
        if (fieldOfView != null && fieldOfView.canSeePlayer) // Enemynin görüþ alanýndaysa...
        {
            Debug.Log("DÜÞMAN GÖRÜLDÜ");
            float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
            if (distanceFromPlayer < detectionArea)
            {
                animator.SetBool("isChasing", true);
            }
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Stop the agent
        agent.SetDestination(agent.transform.position);
    }

}
