using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public GameObject PlayerSpawnerGO;
    public ZombieSpawnerController zombieSpawner;
    private bool isZombieAlive;
    // Start is called before the first frame update
    void Start()
    {
        isZombieAlive= true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (zombieSpawner.isZombieAttacking == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerSpawnerGO.transform.position, Time.fixedDeltaTime * 1.5f);
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Player" && isZombieAlive == true)
        {
            isZombieAlive = false;
            zombieSpawner.ZombieAttackThisCop(collision.gameObject,this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Bullet")
        {
            Destroy(other.gameObject);  
            zombieSpawner.ZombieGotShoot(this.gameObject);
        }
    }
}
