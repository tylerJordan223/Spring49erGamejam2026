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
}
