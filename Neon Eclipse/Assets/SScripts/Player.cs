using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int HP = 100; //Hear bar

    public void TakeDamage(int damageAmount)//Taking Damage
    {
        HP -= damageAmount;
        
        if(HP <= 0)
        {
            print("Player Dead");

            //Game OVER
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        // Material which damage out player
        if (other.CompareTag("DamagedMaterial"))
        {
            TakeDamage(other.gameObject.GetComponent<ZombieHand>().damage);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
