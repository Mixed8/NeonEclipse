using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    [SerializeField]
    private Transform[] picture;

    //[SerializeField]
    //private GameObject winText;

    public static bool youWin;

    void Start()
    {
        youWin = false;
    }

    void Update()
    {
        if (picture[0].rotation.z == 0 &&
            
            picture[1].rotation.z == 0 &&
            
            picture[2].rotation.z == 0 &&
            
            picture[3].rotation.z == 0 &&
            
            picture[4].rotation.z == 0 &&
            picture[5].rotation.z == 0 &&
            picture[6].rotation.z == 0 &&
            picture[7].rotation.z == 0 &&
            picture[8].rotation.z == 0 )
        {
            youWin = true;
            print("kazandin");
        }
    }
}