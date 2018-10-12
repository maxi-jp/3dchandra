using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;  // instance reference to this object
    public GameObject playerShip;  // reference to the player ship
    public UIController UI;    // UI controller
    private int playerScore;   // Player current score

    public int PlayerScore
    {
        get
        {
            return playerScore;
        }

        set
        {
            playerScore = value;
            UI.UpdateScore();
        }
    }

    // Use this for initialization
    void Start ()
    {
        instance = this;
        playerScore = 0;
    }
    
    // Update is called once per frame
    void Update ()
    {
        
    }

}
