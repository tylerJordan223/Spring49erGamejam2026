using Ginput;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //this is a file to handle any overhead in the game

    public static GameManager instance;

    private void Awake()
    {
        if(instance)
        {
            DestroyImmediate(this);
        }
        instance = this;
    }

    [SerializeField] GameObject poof;
    public GameObject gameOverCanvas;
    public TextMeshProUGUI score_text;

    private int score;

    private float timer;
    private int timeScore;

    private void Start()
    {
        timer = 0f;
        timeScore = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if((int)Mathf.Round(timer) > timeScore)
        {
            timeScore += 1;
            AddScore(100);
        }

        if(!gameOverCanvas.activeSelf)
        {
            score_text.text = "SCORE: " + score;
        }
    }

    public void AddScore(int value)
    {
        score += value;
    }

    public void SpawnPoof(Transform t)
    {
        GameObject go = Instantiate(poof);
        go.transform.position = t.position;
    }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
    }
}
