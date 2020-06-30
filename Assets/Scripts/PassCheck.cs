using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//responsible for increasing the score according to the ball pass through the platforms
public class PassCheck : MonoBehaviour
{
    private const int SCORE_INCREASER = 2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            GameManager.singleton.addScore(SCORE_INCREASER);

            FindObjectOfType<BallController>().passed();

        }


        }

}
