using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject MainPanel;
    public GameObject FailPanel;
    public GameObject SucesslPanel;
    public static GameManager instance;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartButtonTapped()
    {
       
        MainPanel.gameObject.SetActive(false);
        GameObject PlayerSpawnerGO = GameObject.FindGameObjectWithTag("PlayerSpawner");
        PlayerSpawnerConttroller playerSpawner=PlayerSpawnerGO.GetComponent<PlayerSpawnerConttroller>();
        playerSpawner.MovePlayer();
       
    }
    public void ShowFailPanel()
    {
        FailPanel.gameObject.SetActive(true);
    }
    public void RestartButtonTapped()
    {
        LevelLoader.instance.GetLevel();
        //Time.timeScale = 1f;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    public void ShowSucesslPanel()
    {
        SucesslPanel.gameObject.SetActive(true);
    }
    public void NextLevelButtonTapped()
    {
        LevelLoader.instance.NextLevel();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
