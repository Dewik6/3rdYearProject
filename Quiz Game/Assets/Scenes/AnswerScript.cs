using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public GameManager gameManager;

    public void Answer()
    {
        gameManager.animator.SetTrigger("Answer");
        if (isCorrect)
        {
            Debug.Log("CORRECT");
            gameManager.Correct();
            gameManager.multiChoiceResult.text = "CORRECT";

        }   
        else
        {
            Debug.Log("WRONG");
            gameManager.Wrong();
            gameManager.multiChoiceResult.text = "WRONG";
        }
               
    }
    
    // The UserSelect methods below are set to the UI of the "True" and "False" buttons
    // Depending on the current question and how the user answered, it will indicate whether the selected answer is correct
    public void UserSelectTrue()                        // This is the method if the answer to the question is True
    {
        gameManager.animator.SetTrigger("True");        // reference to the triggers that set off the annimations

        if (gameManager.currentQuestion.isTrue)
        {
            Debug.Log("CORRECT!");
            gameManager.trueAnswer.text = "CORRECT";
            gameManager.falseAnswer.text = "WRONG";
            gameManager.Correct();
        }
        else
        {
            Debug.Log("WRONG!");
            gameManager.trueAnswer.text = "WRONG";
            gameManager.falseAnswer.text = "CORRECT";
            gameManager.Wrong();
        }

    }
    public void UserSelectFalse()
    {
        gameManager.animator.SetTrigger("False");

        if (!gameManager.currentQuestion.isTrue)        // This is the method if the answer to the question is False
        {
            Debug.Log("CORRECT!");
            gameManager.trueAnswer.text = "WRONG";
            gameManager.falseAnswer.text = "CORRECT";
            gameManager.Correct();
        }
        else
        {
            Debug.Log("WRONG!");
            gameManager.trueAnswer.text = "CORRECT";
            gameManager.falseAnswer.text = "WRONG";
            gameManager.Wrong();
        }

    }
}
