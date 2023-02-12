using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadScene("SampleSceneCannon");
    }
    public void QuitGame(){
        Application.Quit();
    }
    public void CreditsPage(){
        SceneManager.LoadScene("CreditsScene");
    }
    public void BackToMenu(){
        SceneManager.LoadScene("Menu");
    }
    public void VictoryScreen(){
        SceneManager.LoadScene("VictoryScene");
    }
    public void DefeatScreen(){
        SceneManager.LoadScene("DefeatScene");
    }
}

