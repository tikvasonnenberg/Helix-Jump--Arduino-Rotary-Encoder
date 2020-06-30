using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//functions like a controller, responsible for the game flow
public class GameManager : MonoBehaviour
{
    private const int SET_SCORE = 0;
    public int highScore;
    public int score;

    public int currentLevel = 0;
    public int levelForUI = 1;

    public static GameManager singleton;

    // Start is called before the first frame update
    void Awake()
    {
        //create a single GameManager
        if (singleton == null)
        {
            singleton = this;
        }
        else if (singleton != this)
            Destroy(gameObject);
        highScore = PlayerPrefs.GetInt("HighScore");
    }

    //this function advances the player to the next level 
    public void NextLevel()
    {
        currentLevel++;
        levelForUI++;

        if (currentLevel <= FindObjectOfType<HelixController>().allLevels.Count - 1)
        {

            FindObjectOfType<BallController>().ResetBall();

            //enable trail to start again
            FindObjectOfType<BallController>().GetComponent<TrailRenderer>().enabled = true;

            //set game objects for new level
            FindObjectOfType<CameraController>().resetCamera();
            FindObjectOfType<changeBGController>().changeBG(currentLevel);
            FindObjectOfType<BallController>().jump = true;
            FindObjectOfType<HelixController>().LoadLevel(currentLevel);
        }
        else
        {
                   SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

    //this function resets the level
    public void RestartLevel()
    {
       
        singleton.score = SET_SCORE;
       

        FindObjectOfType<BallController>().ResetBall();

        FindObjectOfType<CameraController>().resetCamera();
        FindObjectOfType<HelixController>().LoadLevel(currentLevel);
    }

    //this functions adds score and updates the highest level
    public void addScore(int scoreToAdd)
    {
        score += scoreToAdd;

        if(score > highScore)
        {
            highScore = score;
            //store the high score for another sessions of the game
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
    
}
