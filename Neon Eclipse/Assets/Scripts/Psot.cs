using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Psot : MonoBehaviour
{
    public Sprite[] slides;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeSprite());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ChangeSprite()
    {
        for (int i = 0; i < slides.Length; i++)
        {

            //change image's sprite
            GetComponent<Image>().sprite = slides[i];
            yield return new WaitForSeconds(5);
        }


    }
}
