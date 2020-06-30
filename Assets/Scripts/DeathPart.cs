using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//represent the parts that the user should avoid
public class DeathPart : MonoBehaviour
{
   

    private void OnEnable()
    {
      
    }

    //Reset level if death part is hit
    public void HitDeathPart()
    {

        Time.timeScale = 0.0f; //Stop time flow
                               
    }
}
