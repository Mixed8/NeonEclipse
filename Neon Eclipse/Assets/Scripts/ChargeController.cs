using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeController : MonoBehaviour
{
    public int powerSupply = 100;
    int maxPowerSupply = 100;
    bool isCharging = false;
    public bool isVisible = true;

    float timeSinceLastPowerDecrease = 0f;
    float powerDecreaseInterval = 5f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //when I press the space bar, I want to be visible
        if (Input.GetKeyDown(KeyCode.C))
        {
            isVisible = !isVisible;
        }


        if (powerSupply <= 0)
        {
            //die
        }

        if(!isVisible)
        {
            timeSinceLastPowerDecrease += Time.deltaTime;

            if (timeSinceLastPowerDecrease >= powerDecreaseInterval)
            {
                timeSinceLastPowerDecrease = 0f;
                DecreasePower();
            }
        }
        print(powerSupply);
    }


    private void DecreasePower()
    {

        powerSupply -= 5;


        powerSupply = Mathf.Max(powerSupply, 0);
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Charger"))
        {

            StartCoroutine(ChargePowerOverTime());
        }

        

    }

    private IEnumerator ChargePowerOverTime()
    {
        isCharging = true;
        float elapsedTime = 0f;
        float chargingDuration = 3f;

        while (elapsedTime < chargingDuration)
        {

            float completionPercentage = elapsedTime / chargingDuration;


            powerSupply = (int)Mathf.Lerp(powerSupply, maxPowerSupply, completionPercentage);


            elapsedTime += Time.deltaTime;


            yield return null;
        }


        powerSupply = maxPowerSupply;
        isCharging = false;
    }

}
