using UnityEngine;
using UnityEngine.Events;

public class Elevator : MonoBehaviour
{
    public UnityEvent surfaceButton;
    public UnityEvent bunkerButton;
    public UnityEvent exitButton;
    public UnityEvent openButton;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.instanceGameManager;
    }

    public void OnSurfaceButtonClick()
    {
        if (gameManager.GetTime() == 0) surfaceButton.Invoke();
    }

    public void OnBunkerButtonClick()
    {
        if(!gameManager.gameStarted) bunkerButton.Invoke();
    }

    public void OnExitButtonClick()
    {
        exitButton.Invoke();
    }

    public void OpenElevator()
    {
        if(!gameManager.gameStarted) openButton.Invoke();
    }

    public void StartGame()
    {
        gameManager.gameStarted = true;
    }

    public void StopGame()
    {
        gameManager.gameStarted = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
