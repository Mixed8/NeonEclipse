using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public GameObject waypointsList; // Zombiye özel waypoint listesi
    //Contackt hand
    public ZombieHand zombieHand;

    public int zombieDamage;

    //Put value of zombie damage
    private void Start()
    {
        zombieHand.damage = zombieDamage;
    }
}
