using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private PlayerSpawnerConttroller playerSpawner;
    private GameObject playerSpawnerGO;
    // Start is called before the first frame update
    void Start()
    {
        playerSpawnerGO = GameObject.FindGameObjectWithTag("PlayerSpawner");
        playerSpawner = playerSpawnerGO.GetComponent<PlayerSpawnerConttroller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerSpawner.PlayerGotKilled(other.gameObject);
        } 
    }
}
