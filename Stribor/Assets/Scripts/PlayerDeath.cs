using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField]
    private Collider deathCollider;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == deathCollider)
        {
#if UNITY_EDITOR 
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
