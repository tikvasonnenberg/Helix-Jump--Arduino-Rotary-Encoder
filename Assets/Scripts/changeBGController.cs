using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeBGController : MonoBehaviour
{

    public Sprite[] spriteArray;

    public  void changeBG( int i)
    {
        i = i % spriteArray.Length ;

        Image myImageComponent = GetComponent<Image>();
        myImageComponent.sprite = spriteArray[i];
     

    }
}
