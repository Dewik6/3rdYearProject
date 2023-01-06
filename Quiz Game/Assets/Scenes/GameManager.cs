using System.Collections;
using System.Collections.Generic;       // To enable access to lists
using UnityEngine;
using System.Linq;                      // To enable the use of the ".ToList<>" method
using UnityEngine.UI;                   // To access UI (user interface) features
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{

    public Question[] questions;     // "[]" is an array, and is used to give an list of a fixed number of questions and no need to resize during runtime
                                     // As oppoosed to using a list which will be resized during runtime

    public GameObject[] optionsButtons;    // This is a list of the available answers in the multiple choice questions

    private static List<Question> unansweredQuestions;      // List that contains the unanswered questions. Once a question is answered, it is removed so it will not come up again

    public Question currentQuestion;                       // The question the user is currently answering

    [SerializeField]                                // "[SerializeField]" so that it is accessable in the inspector
    private Text questionText;                        // Where the current currentQuestion is stored and then used to be displayed on the question panel



    [SerializeField]
    public Text trueAnswer;                      // Where the answers to the questions are stored depending on both answers available
    [SerializeField]
    public Text falseAnswer;
    [SerializeField]
    public Text multiChoiceResult;
    [SerializeField]
    public Text questionInfo;

    [SerializeField]
    public Animator animator;                   // This is making reference to the anitmations

    [SerializeField]           // 1f = 1second
    private float questionDelay = 1f;                          // Delay for transitioning from one question to another
    public float infoDelay = 1f;

    [SerializeField]
    public Text ScoreText;

    public static int Score;
    public static int totalQuestionsNum;

    public GameObject infoPanel;




    // Start is called before the first frame update (when a new scene starts up or when a scene is reloaded)
    void Start()
    {

        if (unansweredQuestions == null || unansweredQuestions.Count == 0)      // when the quiz is loaded up, the unanswered questions list wil be equal to null or 0. So the list fills up with the questions, but only once when the quiz first loads up
        {
            unansweredQuestions = questions.ToList<Question>();                 // ".ToList<>" enters the Questions array into the unansweredQuestions list
        }

        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2)
        {
            infoPanel.SetActive(false);
        }
        
        ChooseRandomQuestion();
        totalQuestionsNum += unansweredQuestions.Count;

    }

    // This function is used to generate a random question from the list of questions
    void ChooseRandomQuestion()
    {
               
        if (unansweredQuestions.Count > 0)
        {                                            // "unansweredQuestions.Count" is the number of elements in the unanswered Questions list
            int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);       // This gives a random number from 0 to the number of unanswered questions in the list. This gives a random index that points to a random element in the list and is stored as an integer
            currentQuestion = unansweredQuestions[randomQuestionIndex];             // "currentQuestion" is now equal to a random element/random index of the unansweredQuestions list

            questionText.text = currentQuestion.fact;                   // The actual text that will be displayd in the question panelof the quiz
            questionText.fontSize = currentQuestion.questionFont;
        
            MultiChoiceAnswers();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)         // Checking if the Game Over scene is active
        {
            FinalScore();
        }
        else
        {
            Debug.Log("Out of Questions!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
        
    }

    // This method is used to transition between scenes (different questions) in the game
    IEnumerator TransitionToNextQuestion()                                // "IEnumarator" allows us to wait a certain amount of time within the method before something new happens
    {
        unansweredQuestions.Remove(currentQuestion);                      // Once the chosen random question is answered, it is then removed from the unansweredQuestions list. The current question is then removed by an element and not an index

        yield return new WaitForSeconds(questionDelay);                   // Defines how long to wait

        ChooseRandomQuestion();
        animator.SetTrigger("NextQuestion");
        
    }
    
    IEnumerator QuestionsInformationPanel()
    {
        yield return new WaitForSeconds(infoDelay);

        infoPanel.SetActive(true);

        questionInfo.text = currentQuestion.info;
        questionInfo.fontSize = currentQuestion.infoFontSize;         
    }

    public void ExitInfoButton()
    {
        StartCoroutine(TransitionToNextQuestion());
    }



    public void FinalScore()
    {
        
        ScoreText.text = Score.ToString() + "/" + totalQuestionsNum;
        Debug.Log(ScoreText.text = Score.ToString() + "/" + totalQuestionsNum);
        
    }

    public void Correct()                           // For when the answer is correct
    {
        Score += 1;  
        StartCoroutine(QuestionsInformationPanel());
        
    }

    public void Wrong()                           // For when the answer is wrong
    {
        StartCoroutine(QuestionsInformationPanel());
    }

    void MultiChoiceAnswers()
    {
        for (int i = 0; i < optionsButtons.Length; i++)
        {
            optionsButtons[i].GetComponent<AnswerScript>().isCorrect = false;
            optionsButtons[i].GetComponentInChildren<Text>().text = currentQuestion.ansOptions[i];

            if(currentQuestion.correctAnswer == i + 1)
            {
                
                optionsButtons[i].GetComponent<AnswerScript>().isCorrect = true;
            }
            
            
        }
        
    }


}
