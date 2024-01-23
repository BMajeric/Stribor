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

    public bool isSpotted;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        enemy = GameObject.FindWithTag("Enemy");
        exposureManager = player.GetComponent<ExposureManager>();

        PlayerLocation = ProstorEnums.lokacijaIgraca;
        Debug.Log("Game Manager initialized!");
        neighbourAreas = lokacijaDictHolder.GetComponent<LokacijaSkripta>().susjednaPodrucja;
        givePlayerLocation = false;

        InvokeRepeating("IsInEnemyFOV", 0f, 3f);
        InvokeRepeating("RevealApproxPlayerLocation", 10f, 40f);
    }

    private void Update()
    {
        PlayerLocation = ProstorEnums.lokacijaIgraca;

        if (givePlayerLocation)
        {
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

            }
        }
        else
        {
            if (exportDestinations.Count > 0)
            {
                exportDestinations.Clear();
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
        // Adjust FOV so that the enemy is more aware of the player once the player is spotted
        float fov;
        if (!isSpotted) {
            fov = 0.707f;   // Field of view = 90 (45 degrees from each side)
        }
        else
        {
            fov = 0.259f;   // Field of view = 150 (75 degrees from each side)
        }
        // Check if player is in viewing range of the enemy and if he is in the enemies FOV
        isSpotted = Physics.OverlapSphere(enemy.transform.position, exposureManager.Exposure, LayerMask.GetMask("Player")).Length > 0
           && Vector3.Dot(enemy.transform.forward, player.transform.position - enemy.transform.position) < fov;
    }

    private void RevealApproxPlayerLocation()
    {
        givePlayerLocation = !givePlayerLocation;
    }

}
