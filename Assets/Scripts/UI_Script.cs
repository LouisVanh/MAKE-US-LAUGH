using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Script : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        
            
    }
    public void Options()
    {
        // @Szilard, you're welcome
        //allOtherCanvas(es).gameObject.SetActive(false);
        //OptionsCanvas.gameObject.SetActive(true);
    }
}