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

    private Vector3 playerPosition;
    private GameController gameManager;
    
    bool walking, chasing, prevWalking, prevChasing = false;

    Transform currentDestination;
    Vector3 destination;
    int randDestIndex, prevRandDestIndex;          // Integer for randomly determining the next destination

    [SerializeField]
    private AudioSource playerSpottedSound;
    [SerializeField]
    private AudioSource playerRanAwaySound;

    void Start()
    {
        walking = true;
        //player = GameObject.FindWithTag("Player").transform;
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameController>();
        randDestIndex = Random.Range(0, destinations.Count);
        //randDestIndex = 20;
        currentDestination = destinations[randDestIndex];

    }

    private void Update()
    {
        Debug.Log(gameManager.isSpotted);
        if (gameManager.isSpotted)
        {
            chasing = true;
            walking = false;
        }
        else
        {
            chasing = false;
            walking = true;
        }

        if (prevWalking == true && walking == false && prevChasing == false && chasing == true)
        {
            playerSpottedSound.Play();
        } 
        else if (prevWalking == false && walking == true && prevChasing == true && chasing == false)
        {
            playerRanAwaySound.Play();
        }

        prevWalking = walking;
        prevChasing = chasing;

        playerPosition = gameManager.playerExportPosition;

        if (walking)
        {
            destination = currentDestination.position;
            enemyAgent.destination = destination;
            enemyAgent.speed = walkSpeed;

            StartCoroutine(SetPatrolingDestination());

        }
        else if (chasing)
        {
            enemyAgent.destination = playerPosition;
            enemyAgent.speed = chaseSpeed;
        }
    }

    IEnumerator SetPatrolingDestination()
    {
        if (enemyAgent.pathPending)
        {
            yield return new WaitForSeconds(.5f);
        }
        //Debug.Log("REAMINING DISTANCE: " + enemyAgent.remainingDistance);
        if (enemyAgent.remainingDistance <= enemyAgent.stoppingDistance)
        {
            while (randDestIndex == prevRandDestIndex)
            {
                randDestIndex = Random.Range(0, destinations.Count);
                ///randDestIndex = (randDestIndex + 1) % destinations.Count;
                currentDestination = destinations[randDestIndex];
                //Debug.Log("Random Index: " + randDestIndex.ToString());
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

}
