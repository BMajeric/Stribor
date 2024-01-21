using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public ProstorEnums.Lokacija PlayerLocation;
    private Transform player;
    private GameObject enemy;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private void Initialize()
    {
        player = GameObject.FindWithTag("Player").transform;
        enemy = GameObject.FindWithTag("Enemy");
    }
}
