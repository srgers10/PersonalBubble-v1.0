using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour {
    public Highscore[] scores;
    public Text playerScore;
    public GameObject deathScreen;
    public Text deathMessage;
    public Camera cam;
    public Text powerUp;

    public void SetPlayerScore(int score)
    {
        playerScore.text = "Score: " + score;
        cam.enabled = (false);
    }

    public static UIManager ui;
	// Use this for initialization
	void Start () {
        ui = this;
	}

    public void DeathScreen()
    {
        deathScreen.SetActive(true);
        cam.enabled = (true);
        
    }

    public void PowerUp(bool b)
    {
        powerUp.gameObject.SetActive(b);

    }
    public void GetHighscores(Blob b)
    {
       
        for (int i = 0; i<scores.Length; i++)
        {
            if (b.score > scores[i].score)
            {
                scores[i].SetHighscore(i+1, b.name, b.score);
                return;
            }
        }
     
    }
    IEnumerator DisplayMessage(string message)
    {
        deathMessage.gameObject.SetActive(true);
        deathMessage.text = message;
        yield return new WaitForSeconds(.5f);
        deathMessage.gameObject.SetActive(false);
    }
}

[System.Serializable]
public class Highscore
{
    public int score;
    public Text mName;
    public Text mScore;

    public void SetHighscore(int pos, string name, int score)
    {
        this.score = score;
        mName.text = pos + ". " + name;
        mScore.text = ""+ score;
    }
}
