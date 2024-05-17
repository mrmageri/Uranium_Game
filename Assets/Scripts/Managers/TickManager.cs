
using Machines;
using UnityEngine;

public class TickManager : MonoBehaviour
{
    public int workingMachines;
    private const float tickTimerMax = 1f;
    private int tick;
    private float tickTimer;
    [SerializeField] private Machine[] machines;
    [SerializeField] private GameManager gameManager;

    private bool started;

    public static TickManager instanceTickManager;

    TickManager()
    {
        instanceTickManager = this;
    }
    
    private void Awake()
    {
        tick = 0;
        workingMachines = machines.Length;
    }

    private void Update()
    {
        if(!gameManager.gameStarted) return;
        tickTimer += Time.deltaTime;
        if (tickTimer >= tickTimerMax)
        {
            tickTimer -= tickTimerMax;
            tick++;
            if (tick == 100) tick = 0;
            OnTick();
        }
    }

    public void ResetMachines()
    {
        foreach (var elem in machines)
        {
            elem.Reset();
        }
    }

    private void OnTick()
    {
        gameManager.CountSec();
        Player.Player.instancePlayer.DecreaseCoffeePerTick(tick);
        workingMachines = 0;
        foreach (var elem in machines)
        {
            elem.OnTick();
            if (!elem.isBroken) workingMachines++;
        }
        if(workingMachines <= workingMachines * 0.1f) gameManager.EndGame();
    }
}