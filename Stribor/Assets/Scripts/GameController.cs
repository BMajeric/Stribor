using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public ProstorEnums.Lokacija PlayerLocation;
    private GameObject player;
    private GameObject enemy;
    private ExposureManager exposureManager;
    public Vector3 playerExportPosition;

    public bool isSpotted;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        enemy = GameObject.FindWithTag("Enemy");
        exposureManager =player.GetComponent<ExposureManager>();
        PlayerLocation = ProstorEnums.lokacijaIgraca;
        Debug.Log("Game Manager initialized!");
        InvokeRepeating("IsInEnemyFOV", 0, 3);

    }

    private void Update()
    {
        if (isSpotted)
        {
            playerExportPosition = player.transform.position;
        } 
        else
        {
            playerExportPosition = Vector3.zero;
        }
    }

    private void IsInEnemyFOV()
    {
        // Check if player is in viewing range of the enemy and if he is in the enemies FOV of 90 degrees 
        isSpotted = Physics.OverlapSphere(enemy.transform.position, exposureManager.Exposure, LayerMask.GetMask("Player")).Length > 0
           && Vector3.Dot(enemy.transform.forward, player.transform.position - enemy.transform.position) < 0.707;
    }

}
