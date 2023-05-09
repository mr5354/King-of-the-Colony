using UnityEngine;

public class BestScore : MonoBehaviour
{
    public static BestScore Instance { get; private set; }

    private int score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetScore(int newScore)
    {
        if (newScore > score)
        {
            score = newScore;
        }
    }

    public int GetScore()
    {
        return score;
    }
}
