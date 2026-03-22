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
        input.RESET.Quit.performed += QuitGame;
        input.RESET.Enable();
    }

    private void OnDisable()
    {
        input.RESET.Disable();
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

    private void QuitGame(InputAction.CallbackContext context)
    {
        Application.Quit();
    }
}
