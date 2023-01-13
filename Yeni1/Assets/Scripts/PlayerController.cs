using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletGO;
    public Transform bulletSpawnTransform;
    private float bulletSpeed = 20f;
    bool isShootingOn;
    Animator PlayerAnimator;
    Transform PlayerSpawnerCenter;
    float goingToCenterSpeed = 4f;
    // Start is called before the first frame update
    void Start()
    {
        PlayerSpawnerCenter = transform.parent.gameObject.transform;
        PlayerAnimator = GetComponent<Animator>();
    } 

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, PlayerSpawnerCenter.position, Time.fixedDeltaTime * goingToCenterSpeed);
    }
    public void StartShooting()
    {
        StartShootingAnim();
        isShootingOn = true;
        StartCoroutine(Shooting());
    }
    public void StopShooting()
    {
        isShootingOn= false;
        StartRunAnim();
    }
    IEnumerator Shooting() 
    {
        while (isShootingOn)
        {
            yield return new WaitForSeconds(1.1f);
            Shoot();
            yield return new WaitForSeconds(1.5f);
        }
        
    }
    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletGO, bulletSpawnTransform.position, Quaternion.identity);
        Rigidbody bulletRB= bullet.GetComponent<Rigidbody>();
        bulletRB.velocity = transform.forward * bulletSpeed;
    }
    public void StartShootingAnim()
    {
        PlayerAnimator.SetBool("isShootingOn",true);
        PlayerAnimator.SetBool("isRunning", false);
    }
    private void StartRunAnim()
    {
        PlayerAnimator.SetBool("isShootingOn", false);
        PlayerAnimator.SetBool("isRunning",true);
    }
    public void StartIdleAnim()
    {
        PlayerAnimator.SetBool("isRunning", false);
        PlayerAnimator.SetBool("isLevelFinished", true);
    }

}
