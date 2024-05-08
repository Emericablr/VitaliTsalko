using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance; 
    public TextMeshProUGUI scoreText; 

    public UnityEvent onScoreUpdate; 

    public int score; 

    private void Awake()
    {
        instance = this; 
    }

    public void AddScore(int amount)
    {
        score += amount; 
        scoreText.text = score.ToString(); 
        onScoreUpdate.Invoke(); 
    }

    public void DecreaseScore(int amount)
    {
        score -= amount; 
        scoreText.text = score.ToString(); 
        onScoreUpdate.Invoke(); 
    }
}
