using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    
    public void TryAgain()
    {
        SceneManager.LoadScene("MultipleChoice 1");
        GameManager.Score = 0;
        GameManager.totalQuestionsNum = 0;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
        GameManager.Score = 0;
        GameManager.totalQuestionsNum = 0;
    }

}
