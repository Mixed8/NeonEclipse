using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PuzzleSolver : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (!GameControl.youWin)
        {
            transform.Rotate(0f, 0f, 90f);
        }
    }
    //private void OnMouseOver()
    //{
    //    if (Input.GetMouseButtonDown(1)) // 1, sað fare tuþunu temsil eder
    //    {
    //        if (!GameControl.youWin)
    //        {
    //            transform.Rotate(0f, 90f, 0f);
    //        }
    //    }
    //}
}