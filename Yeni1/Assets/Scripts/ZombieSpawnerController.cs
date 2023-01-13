 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnerController : MonoBehaviour
{
    public GameObject zombieGO;
    public int zombieCount=0;
    public List<GameObject> zombieList = new List<GameObject>();
    public PlayerSpawnerConttroller playerSpawner;
    public GameObject playerSpawnerGO;
    public bool isZombieAttacking; 
    // Start is called before the first frame update
    void Start()
    {
        playerSpawnerGO = GameObject.FindGameObjectWithTag("PlayerSpawner");
        playerSpawner =playerSpawnerGO.GetComponent<PlayerSpawnerConttroller>();
        SpawnZombies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnZombies()
    {
        for (int i = 0; i < zombieCount; i++)
        {
            Quaternion zombieRotation= Quaternion.Euler(0, 180, 0);
            GameObject zombie = Instantiate(zombieGO, GetZombiePosition(),zombieRotation, transform);
            ZombieController zombie1 = zombie.GetComponent<ZombieController>();
            zombie1.PlayerSpawnerGO = playerSpawnerGO;
            zombie1.zombieSpawner = this;
            zombieList.Add(zombie);
        }
       
    }
    public Vector3 GetZombiePosition()
    {
        Vector3 pos = Random.insideUnitSphere * 0.1f;
        Vector3 newPos=transform.position + pos;
        return newPos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<BoxCollider>().enabled = false;
            playerSpawner.ZombieDetected(gameObject);
            LookAtPlayers(other.gameObject);
            isZombieAttacking = true;
        }
    }
    private void LookAtPlayers(GameObject target)
    {
        Vector3 dir =transform.position- target.transform.position;
        Quaternion lookRotation= Quaternion.LookRotation(dir);
        lookRotation.x = 0;
        lookRotation.z = 0;
        transform.rotation= lookRotation;
    }
    public void ZombieAttackThisCop(GameObject player,GameObject zombie)
    {
        zombieList.Remove(zombie);
        CheckZombieCount();
        playerSpawner.PlayerGotKilled(player);
        Destroy(zombie);
    }

    private void CheckZombieCount()
    {
        if (zombieList.Count <= 0)
        {
            playerSpawner.AllZombiesKilled();
        }
    }
    public void ZombieGotShoot(GameObject zombie)
    {
        zombieList.Remove(zombie);
        Destroy(zombie);
        CheckZombieCount();
    }
}
