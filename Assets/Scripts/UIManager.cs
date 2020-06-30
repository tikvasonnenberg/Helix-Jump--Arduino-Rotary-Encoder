using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//responsible for information display on the game screen for the user.
public class UIManager : MonoBehaviour
{
    [SerializeField] private Text ScoreDisplay;
    [SerializeField] private Text HighScoreDisplay;
    [SerializeField] private Text ScoreText;
    [SerializeField] private Text HighScoreText;
    [SerializeField] private Text LevelNum;

    //Change the Text Color
    public void changeColor()
    {
        Color color = FindObjectOfType<HelixController>().GetComponent<Renderer>().material.color;
        ScoreDisplay.color = color;
        HighScoreDisplay.color = color;
        ScoreText.color = color;
        HighScoreText.color = color;

    }

    void Update()
    {   //update the scores 
        HighScoreDisplay.text = "" + GameManager.singleton.highScore;
        ScoreDisplay.text = "" + GameManager.singleton.score;
        LevelNum.text = " "+GameManager.singleton.levelForUI;
    }
}
