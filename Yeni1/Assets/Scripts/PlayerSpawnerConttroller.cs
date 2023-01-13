using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnerConttroller : MonoBehaviour
{
    public GameObject PlayerGO;
    float PlayerSpeed = 2    ;
    float xSpeed;
    float maxXPosition = 3; 
    public List<GameObject>PlayerList= new List<GameObject>();
   public static bool isPlayerMoving;
    public AudioSource playerSpawnerAudioSource;
    public AudioClip gateClip, congratsClip, failClip, ShootClip; 
    // Start is called before the first frame update
   
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerMoving ==false)
        {
            return;
        }
        hareket();
    }

   public void hareket()
    {
        float touchx = 0;
        float newXValue = 0;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            xSpeed = 250f;
            touchx = Input.GetTouch(0).deltaPosition.x / Screen.width;
        }
        else if (Input.GetMouseButton(0))
        {
            xSpeed = 500f;
            touchx = Input.GetAxis("Mouse X");
        }
        newXValue = transform.position.x + xSpeed * touchx * Time.deltaTime;
        newXValue = Mathf.Clamp(newXValue, -maxXPosition, maxXPosition);//sýnýrlandýrma..
        Vector3 PlayerNewPosition = new Vector3(newXValue, transform.position.y, transform.position.z + PlayerSpeed * Time.deltaTime);
        transform.position = PlayerNewPosition;
    }
    public void SpawPlayer(int gateValue,GateType gateType)
    {
        PlayAudio(gateClip);
        if (gateType==GateType.additionType)
        {
            
            for (int i = 0; i < gateValue; i++)
            {
                GameObject newPlayerGo = Instantiate(PlayerGO, GetPlayerPosition(), Quaternion.identity, transform);
                PlayerList.Add(newPlayerGo);
            }
        }
        else if (gateType==GateType.multiplyType)
        {
            int newPlayerCount = (PlayerList.Count * gateValue) - PlayerList.Count;
            for (int i = 0; i < newPlayerCount; i++)
            {
                GameObject newPlayerGo = Instantiate(PlayerGO, GetPlayerPosition(), Quaternion.identity, transform);
                PlayerList.Add(newPlayerGo);
            }
        }
        

    }
    public Vector3 GetPlayerPosition() 
    {
        Vector3 position = Random.insideUnitSphere * 0.1f;
        Vector3 newPos= transform.position+position;
        newPos.y = 0.5f;
        return newPos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FinishLine")
        {
            PlayAudio(congratsClip);
            StopBackgroundMusic();
            StartAllCopsIdleAnim();
            GameManager.instance.ShowSucesslPanel();
            isPlayerMoving = false;
            
        } 
    }
    public void PlayerGotKilled(GameObject playerGO)
    {
        PlayerList.Remove(playerGO);
        CheckPlayerCount();
        Destroy(playerGO);
    }
    private void CheckPlayerCount()
    {
        if (PlayerList.Count<=0)
        {
            StopBackgroundMusic(); 
            PlayAudio(failClip);
            StopPlayer();
            GameManager.instance.ShowFailPanel();
        }
    }
    public void ZombieDetected(GameObject target)
    {
        isPlayerMoving= false;
        LookAtZombies(target);
        StartAllCopsSohooting();
        StopBackgroundMusic();
        PlayAudio(ShootClip);
    }
    private void LookAtZombies(GameObject target)
    {
        Vector3 dir=target.transform.position- transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        lookRotation.x = 0;
        lookRotation.z = 0;
        transform.rotation = lookRotation;  
    }
    private void LookAtForward()
    {
        transform.rotation = Quaternion.identity;
    }
    public void AllZombiesKilled()
    {
        LookAtForward();
        MovePlayer();
    }
    public void MovePlayer()
    {
        
        StartAllCopsRunAnim();
        isPlayerMoving = true;
    }
    public void StopPlayer()
    {
        isPlayerMoving= false;
    }
    public void StartAllCopsSohooting()
    {
        for (int i = 0; i < PlayerList.Count; i++)
        {
           PlayerController cop = PlayerList[i].GetComponent<PlayerController>();
            cop.StartShooting();
        }
    }
    public void StartAllCopsRunAnim()
    {
        for (int i = 0; i < PlayerList.Count; i++)
        {
            PlayerController cop = PlayerList[i].GetComponent<PlayerController>();
            cop.StopShooting();
        }
    }
    public void StartAllCopsIdleAnim()
    {
        for (int i = 0; i < PlayerList.Count; i++)
        {
            PlayerController cop = PlayerList[i].GetComponent<PlayerController>();
            cop.StartIdleAnim();
        }
    }
    public void PlayAudio(AudioClip clip)
    {
        if (playerSpawnerAudioSource !=null)
        {
            playerSpawnerAudioSource.PlayOneShot(clip,0.4f);
        }
    }
    private void StopBackgroundMusic()
    {
        Camera.main.GetComponent<AudioSource>().Stop();
    }
     
}
