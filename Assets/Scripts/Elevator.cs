using UnityEngine;
using UnityEngine.Events;

public class Elevator : MonoBehaviour
{
    public UnityEvent bunkerButton;
    public UnityEvent exitButton;
    public UnityEvent openButton;

    private GameManager gameManager;
    private TickManager tickManager;
    private Player.Player player;

    private void Awake()
    {
        gameManager = GameManager.instanceGameManager;
        player = Player.Player.instancePlayer;
        tickManager = TickManager.instanceTickManager;
    }

    public void OnBunkerButtonClick()
    {
        if(!gameManager.gameStarted && !gameManager.gameStopped) bunkerButton.Invoke();
    }

    public void OnExitButtonClick()
    {
        exitButton.Invoke();
    }

    public void OpenElevator()
    {
        openButton.Invoke();
    }

    public void StartGame()
    {
        gameManager.gameStarted = true;
    }

    public void StopGame()
    {
        gameManager.gameStarted = false;
    }

    public void ResetLevel()
    {
        gameManager.SetTime();
        player.ResetCoffee();
        tickManager.ResetMachines();
        gameManager.gameStopped = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
