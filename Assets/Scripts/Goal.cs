using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//represent  the bottom platform of the helix, indicates the transition to a new level
public class Goal : MonoBehaviour
{
    public ParticleSystem fireworks;
    private bool ignoreNextcollision=false;
    //This function detects the ball colliding with the last platform of the level and advances the player to the next level
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<BallController>())
        {
            collision.transform.GetComponent<BallController>().jump = false;
            if (ignoreNextcollision)
                return;
            fireworks.gameObject.SetActive(true);
            fireworks.Play();
            Invoke("stopFireworks", .7f);
            ignoreNextcollision = true;
            Invoke("AllowCollision", .2f);
        }
        
    }

    private void stopFireworks()
    {
        fireworks.gameObject.SetActive(false);
        GameManager.singleton.NextLevel();
        Debug.Log("level ended");
   
    }
    private void AllowCollision()
    {
        ignoreNextcollision = false;
    }
}
