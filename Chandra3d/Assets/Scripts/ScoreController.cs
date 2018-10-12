using UnityEngine;
using UnityEngine.UI;

// Control score UI components
public class ScoreController : MonoBehaviour {

    public Text scorePointsTxt;  // score points text at UI

    // sets score at UI
	public void setScore(int newScore)
    {
        scorePointsTxt.text = newScore.ToString();
    }        
}
