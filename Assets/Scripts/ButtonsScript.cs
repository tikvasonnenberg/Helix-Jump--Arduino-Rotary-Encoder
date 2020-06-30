using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsScript : MonoBehaviour
{

    GameObject reset; 

        public void Start()
    {
        reset = GameObject.Find("restartLevel");
    }
    public void back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
    
   static bool toggle=true;  
    public void mute()
    {
      
     toggle  = !toggle;

        if (toggle)
            AudioListener.volume = 1f;

        else
            AudioListener.volume = 0f;
    }
    public void resetLevel()
    {
        GameManager.FindObjectOfType<GameManager>().RestartLevel();
    }

   
 
}
