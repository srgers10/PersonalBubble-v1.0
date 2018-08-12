using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchLevel : MonoBehaviour {

	public void Switch(string levelName)
    {
        if(levelName != "Exit")
        {
            SceneManager.LoadScene(levelName);
        }
        else
        {
            Application.Quit();
        }
    }
}
