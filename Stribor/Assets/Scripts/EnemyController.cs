using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent enemyAgent;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private List<Transform> destinations;        // The destinations to which the enemy will travel to simulate searching
    [SerializeField]
    private float walkSpeed, chaseSpeed;
    
    bool walking, chasing;

    Transform currentDestination;
    Vector3 destination;
    int randDestIndex, prevRandDestIndex;          // Integer for randomly determining the next destination


    void Start()
    {
        walking = true;
        randDestIndex = Random.Range(0, destinations.Count);
        currentDestination = destinations[randDestIndex];
        //Debug.Log("REMAINING: " + enemyAgent.remainingDistance);
        //Debug.Log("STOPPING: " + enemyAgent.stoppingDistance);
    }

    private void Update()
    {
        if (walking)
        {
            destination = currentDestination.transform.position;
            enemyAgent.destination = destination;
            enemyAgent.speed = walkSpeed;

            if (enemyAgent.remainingDistance <= enemyAgent.stoppingDistance)
            {
                while(randDestIndex == prevRandDestIndex)
                {
                    randDestIndex = Random.Range(0, destinations.Count);
                    currentDestination = destinations[randDestIndex];
                    //Debug.Log(randDestIndex);
                    //Debug.Log(walking);
                }
                StartCoroutine(WaitingCoroutine(1));
            }
            //Debug.Log("A");
            prevRandDestIndex = randDestIndex;
        } else if (chasing)
        {
            enemyAgent.destination = player.transform.position;
            enemyAgent.speed = chaseSpeed;
        }
    }

    IEnumerator WaitingCoroutine(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.tag);
        if (other.CompareTag("Player"))
        {
            chasing = true;
            walking = false;
        }
    }

}
