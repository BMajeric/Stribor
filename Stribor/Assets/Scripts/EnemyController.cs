using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Configuration")]
    [SerializeField]
    private NavMeshAgent enemyAgent;
    [SerializeField]
    private float walkSpeed, chaseSpeed;
    [SerializeField]
    private List<Transform> destinations;        // The destinations to which the enemy will travel to simulate searching

    private Transform player;
    
    bool walking, chasing;

    Transform currentDestination;
    Vector3 destination;
    int randDestIndex, prevRandDestIndex;          // Integer for randomly determining the next destination


    void Start()
    {
        walking = true;
        player = GameObject.FindWithTag("Player").transform;
        randDestIndex = Random.Range(0, destinations.Count);
        //randDestIndex = 20;
        currentDestination = destinations[randDestIndex];

    }

    private void Update()
    {
        if (walking)
        {
            destination = currentDestination.position;
            enemyAgent.destination = destination;
            enemyAgent.speed = walkSpeed;

            StartCoroutine(SetPatrolingDestination());

        }
        else if (chasing)
        {
            enemyAgent.destination = player.transform.position;
            enemyAgent.speed = chaseSpeed;
        }
    }

    IEnumerator SetPatrolingDestination()
    {
        if (enemyAgent.pathPending)
        {
            yield return new WaitForSeconds(.5f);
        }
        Debug.Log("REAMINING DISTANCE: " + enemyAgent.remainingDistance);
        if (enemyAgent.remainingDistance <= enemyAgent.stoppingDistance)
        {
            while (randDestIndex == prevRandDestIndex)
            {
                randDestIndex = Random.Range(0, destinations.Count);
                ///randDestIndex = (randDestIndex + 1) % destinations.Count;
                currentDestination = destinations[randDestIndex];
                Debug.Log("Random Index: " + randDestIndex.ToString());
                StartCoroutine(WaitingCoroutine(1));
            }
        }
        //Debug.Log("A");
        prevRandDestIndex = randDestIndex;

        yield return null;
    }

    IEnumerator WaitingCoroutine(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag);
        if (other.CompareTag("Player"))
        {
            chasing = true;
            walking = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chasing = false;
            walking = true;
        }
    }

}
