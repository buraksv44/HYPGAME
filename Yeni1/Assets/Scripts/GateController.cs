using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public enum GateType { multiplyType,additionType}
public class GateController : MonoBehaviour
{
    public int gateValue;
    public TextMeshProUGUI gateText;
    public GateType gateType;
    GameObject playerSpawnerGo;
    PlayerSpawnerConttroller PlayerSpawner;
    bool hasGateUsed;
    GateHolderController gateHolder;
    // Start is called before the first frame update
    void Start()
    {
        playerSpawnerGo = GameObject.FindGameObjectWithTag("PlayerSpawner");
        PlayerSpawner = playerSpawnerGo.GetComponent<PlayerSpawnerConttroller>(); 
        gateHolder= transform.parent.gameObject.GetComponent<GateHolderController>();
        AddGateValueAndSymbol();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player" && hasGateUsed == false)
        {
            hasGateUsed = true;
            //karakter çoðalt
            PlayerSpawner.SpawPlayer(gateValue,gateType);
            gateHolder.CloseGates();
            Destroy(gameObject);
        }
    }
    private void AddGateValueAndSymbol()
    {
        switch (gateType)
        {
            case GateType.multiplyType:
                gateText.text="X" + gateValue.ToString();
                break;
            case GateType.additionType:
                gateText.text = "+" + gateValue.ToString();
                break;
            default: 
                break;
        }
    }
}
