using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls every UI component
public class UIController : MonoBehaviour {

    public ScoreController score;  // control score display

    // updates score with player current score
    public void UpdateScore()
    {
        score.setScore(GameController.instance.PlayerScore);
    }
}
