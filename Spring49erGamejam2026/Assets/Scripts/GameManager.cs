using Ginput;
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
