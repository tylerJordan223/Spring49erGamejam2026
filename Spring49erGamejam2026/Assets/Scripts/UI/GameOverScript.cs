using Ginput;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public GameObject restart;

    private GameInput input;

    private void Start()
    {
        restart.SetActive(true);
        StartCoroutine(FlashText());
    }

    private void OnEnable()
    {
        input = new GameInput();
        input.RESET.Restart.performed += RestartGame;
        input.RESET.Restart.Enable();
    }

    private IEnumerator FlashText()
    {
        while(true)
        {
            restart.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            restart.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void RestartGame(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
