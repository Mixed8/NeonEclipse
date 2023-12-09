using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UImanager : MonoBehaviour
{
    public TextMeshProUGUI powerSupplyText;
    public TextMeshProUGUI isVisibleText;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        powerSupplyText.text = "100";
        isVisibleText.text = "off";
    }

    // Update is called once per frame
    void Update()
    {
        powerSupplyText.text = player.GetComponent<ChargeController>().powerSupply.ToString();

        if (player.GetComponent<ChargeController>().isVisible)
        {
            isVisibleText.text = "off";
        }
        else
        {
            isVisibleText.text = "on";
        }
    }
}
