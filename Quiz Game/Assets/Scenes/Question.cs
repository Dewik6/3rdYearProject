using UnityEngine;

[System.Serializable]   // Notifies Unity and the system that this class can be saved and can store info
                        // Also allows to the information within this class to be edited  within the 'Inspector' in Unity
public class Question       // A class where the logic and info about the questions can be stored
{
    
    public string fact;             // "string" indicating the it is going to be some kind of text
    public int questionFont = 12;
    public bool isTrue;             // "bool" indicating whether the fact/question is true of false

    public string[] ansOptions;       // List of options for multiple choice questions
    public int correctAnswer;         // Correct answer for the multiple choie questions
                                      // correctAnswer set as an integer as the correct answer in the list of options will be an index of that list
    [TextArea]
    public string info;
    public int infoFontSize = 12;
}
