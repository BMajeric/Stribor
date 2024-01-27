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
    
    private List<Transform> destinations;        // The destinations to which the enemy will travel to simulate searching
    private List<Transform> moreLikelyestinations;  // The destinations provided by the GameManager based on the players current location
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

    private float timeSpentOnDestination; //unstuck mehanika ako treba predugo vremenski

    private Vector3 positionUnstuck;

    private Vector3 prevPositionUnstuck; //unstuck ako smo na istom mjestu predugo

    void Start()
    {
        walking = true;
        //player = GameObject.FindWithTag("Player").transform;
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameController>();

        destinations = gameManager.exportDestinations;
        randDestIndex = Random.Range(0, destinations.Count);
        currentDestination = destinations[randDestIndex];
        moreLikelyestinations = new List<Transform>();
        StartCoroutine(CheckIfStuck());

    }

    private void Update()
    {
        //Debug.Log(gameManager.isSpotted);
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
        moreLikelyestinations = gameManager.exportPlayerDestinations;

        if (walking)
        {
            destination = currentDestination.position;
            enemyAgent.destination = destination;
            enemyAgent.speed = walkSpeed;

            StartCoroutine(SetPatrolingDestination());

        }
        else if (chasing)
        {
            if (playerPosition != Vector3.zero)
            {
                enemyAgent.destination = playerPosition;
                enemyAgent.speed = chaseSpeed;
            }
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
                if (moreLikelyestinations.Count > 0)
                {
                    randDestIndex = Random.Range(0, moreLikelyestinations.Count);
                    currentDestination = moreLikelyestinations[randDestIndex];
                }
                else
                {
                    randDestIndex = Random.Range(0, destinations.Count);
                    ///randDestIndex = (randDestIndex + 1) % destinations.Count;
                    currentDestination = destinations[randDestIndex];
                    //Debug.Log("Random Index: " + randDestIndex.ToString());
                }
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

    IEnumerator CheckIfStuck() {
        prevPositionUnstuck = this.transform.position;

        yield return new WaitForSeconds(30f);

        positionUnstuck = this.transform.position;

        if (Vector3.Distance(positionUnstuck, prevPositionUnstuck) < 3f && !chasing) {
            Debug.Log("Enemy stuck, redirecting");
           if (moreLikelyestinations.Count > 0)
            {
                randDestIndex = Random.Range(0, moreLikelyestinations.Count);
                currentDestination = moreLikelyestinations[randDestIndex];
            }
            else
            {
                randDestIndex = Random.Range(0, destinations.Count);
                ///randDestIndex = (randDestIndex + 1) % destinations.Count;
                currentDestination = destinations[randDestIndex];
                //Debug.Log("Random Index: " + randDestIndex.ToString());
            }

            walkSpeed = 5f;
        } else {
            Debug.Log("Not stuck");
            walkSpeed = 3f;
        }

        StartCoroutine(CheckIfStuck());

        
    }

    
}
