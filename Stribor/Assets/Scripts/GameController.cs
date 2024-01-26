using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject lokacijaDictHolder;
    public Dictionary<string, List<string>> neighbourAreas;
    private ProstorEnums.Lokacija PlayerLocation;
    private ProstorEnums.Lokacija PrevPlayerLocation;

    private GameObject player;
    private GameObject enemy;
    private ExposureManager exposureManager;

    public Vector3 playerExportPosition;
    private bool givePlayerLocation;
    private List<string> possiblePlayerAreas;
    public List<Transform> exportDestinations;
    public List<Transform> exportPlayerDestinations;

    public bool isSpotted = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        enemy = GameObject.FindWithTag("Enemy");
        exposureManager = player.GetComponent<ExposureManager>();

        PlayerLocation = ProstorEnums.lokacijaIgraca;
        neighbourAreas = lokacijaDictHolder.GetComponent<LokacijaSkripta>().susjednaPodrucja;
        possiblePlayerAreas = neighbourAreas[PlayerLocation.ToString()];
        possiblePlayerAreas.Add(PlayerLocation.ToString());

        exportDestinations = new List<Transform>();
        foreach (string area in possiblePlayerAreas)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(area);
            foreach (GameObject obj in objects)
            {
                exportDestinations.Add(obj.transform);
            }
        }
        Debug.Log("Game Manager initialized!");
        
        givePlayerLocation = false;

        InvokeRepeating("IsInEnemyFOV", 0f, 3f);
        InvokeRepeating("RevealApproxPlayerLocation", 30f, 120f);
    }

    private void Update()
    {
        PlayerLocation = ProstorEnums.lokacijaIgraca;

        if (PlayerLocation != PrevPlayerLocation)
        {
            possiblePlayerAreas = neighbourAreas[PlayerLocation.ToString()];
            possiblePlayerAreas.Add(PlayerLocation.ToString());

            exportDestinations = new List<Transform>();
            foreach (string area in possiblePlayerAreas)
            {
                GameObject[] objects = GameObject.FindGameObjectsWithTag(area);
                foreach (GameObject obj in objects)
                {
                    exportDestinations.Add(obj.transform);
                }
            }

            if (givePlayerLocation)
            {
                exportPlayerDestinations = new List<Transform>();
                GameObject[] objects = GameObject.FindGameObjectsWithTag(PlayerLocation.ToString());
                foreach (GameObject obj in objects)
                {
                    exportDestinations.Add(obj.transform);
                }
            }
            else
            {
                if (exportPlayerDestinations.Count > 0)
                {
                    exportPlayerDestinations.Clear();
                }
            }

        }
        
        

        Debug.Log("LOKACIJA: " + PlayerLocation);
        if (isSpotted)
        {
            playerExportPosition = player.transform.position;
        } 
        else
        {
            playerExportPosition = Vector3.zero;
        }

        PrevPlayerLocation = PlayerLocation;
    }

    private void IsInEnemyFOV()
    {
        // Check if player is in viewing range of the enemy and if he is in the enemies FOV
        // Adjust FOV so that the enemy is more aware of the player once the player is spotted
        if (!isSpotted) {
            // Field of view = 90 (45 degrees from each side)
            isSpotted = Physics.OverlapSphere(enemy.transform.position, exposureManager.Exposure, LayerMask.GetMask("Player")).Length > 0
           && Vector3.Dot(enemy.transform.forward, player.transform.position - enemy.transform.position) < 0.707f;  
        }
        else
        {
            // Field of view = 150 (75 degrees from each side)
            // If player is close to the enemy, the enemy can't lose him
            isSpotted = (Physics.OverlapSphere(enemy.transform.position, exposureManager.Exposure, LayerMask.GetMask("Player")).Length > 0
           && Vector3.Dot(enemy.transform.forward, player.transform.position - enemy.transform.position) < 0.259f)
           || Physics.OverlapSphere(enemy.transform.position, 10f, LayerMask.GetMask("Player")).Length > 0;
        }
        
    }

    private void RevealApproxPlayerLocation()
    {
        givePlayerLocation = !givePlayerLocation;
    }

}
