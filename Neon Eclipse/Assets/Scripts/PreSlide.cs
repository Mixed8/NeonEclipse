using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreSlide : MonoBehaviour
{
    //get sprite array
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

    //change spires with 5 seconds delay
    IEnumerator ChangeSprite()
    {
        for (int i = 0; i < slides.Length; i++)
        {
            
            //change image's sprite
            GetComponent<Image>().sprite = slides[i];
            yield return new WaitForSeconds(5);
        }
        //load next scene
        GetComponentInParent<Buttons>().NextScene();
    }

}
