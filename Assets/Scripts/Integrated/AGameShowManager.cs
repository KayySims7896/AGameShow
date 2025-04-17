using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameShowManager : MonoBehaviour
{
    private int currentLevel = 1;
    private int correctAnswers = 0;
    private int requiredCorrectAnswers = 2; // Adjust as needed

    private Dictionary<int, List<Question>> levels;

    void Start()
    {
        InitializeQuestions();
    }

    void InitializeQuestions()
    {
        levels = new Dictionary<int, List<Question>>();

        levels[1] = new List<Question>() {
            new Question("What is 1 + 1?", new[]{"2"}),
            new Question("What shape has four sides?", new[]{"square"}),
            new Question("Which of the following is a mammal?", new[]{"otter"}),
        };

        levels[2] = new List<Question>() {
            new Question("What subject is the most creative?", new[]{"art"}),
            new Question("Which of the following have four legs?", new[]{"dog"}),
            new Question("How many days are in a week?", new[]{"7", "seven"}),
            new Question("Which is the correct horse?", new[]{"horse"}),
        };

        levels[3] = new List<Question>() {
            new Question("Which is the correct 7?", new[]{"7", "seven"}),
            new Question("If you saw your loved ones tied to separate train tracks, who are you saving?", new[]{"a", "b", "loved one"}),
        };
    }

    public bool ParseAnswer(string playerInput, Question currentQuestion)
    {
        playerInput = playerInput.ToLower();
        bool isCorrect = currentQuestion.acceptableAnswers.Any(answer => playerInput.Contains(answer));

        if (isCorrect)
        {
            correctAnswers++;
            Debug.Log("Correct answer! Total correct: " + correctAnswers);

            if (correctAnswers >= requiredCorrectAnswers)
            {
                AdvanceLevel();
            }

            return true;
        }
        else
        {
            Debug.Log("Incorrect answer. Game Over.");
            GameOver();
            return false;
        }
    }

    void AdvanceLevel()
    {
        correctAnswers = 0;
        currentLevel++;

        if (!levels.ContainsKey(currentLevel))
        {
            Debug.Log("Congratulations! You've completed all levels.");
            EndGame();
        }
        else
        {
            Debug.Log("Proceeding to level: " + currentLevel);
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over! Better luck next time.");
        // Implement game-over logic here
    }

    void EndGame()
    {
        Debug.Log("Thanks for playing! You've won the game.");
        // Implement end-game logic here
    }
}

[System.Serializable]
public class Question
{
    public string questionText;
    public string[] acceptableAnswers;

    public Question(string questionText, string[] acceptableAnswers)
    {
        this.questionText = questionText;
        this.acceptableAnswers = acceptableAnswers.Select(a => a.ToLower()).ToArray();
    }
}
