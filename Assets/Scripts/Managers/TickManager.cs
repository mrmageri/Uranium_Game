
using System.Collections.Generic;
using Machines;
using Managers;
using UnityEngine;

public class TickManager : MonoBehaviour
{
    public int workingMachines;
    private const float tickTimerMax = 1f;
    private int tick;
    private float tickTimer;
    public List<Machine> machines = new List<Machine>();
    //public Machine[] machines;
    [SerializeField] private GameManager gameManager;

    private bool started;
    private AchievementsManager achievementsManager;

    public static TickManager instanceTickManager;

    TickManager()
    {
        instanceTickManager = this;
    }
    
    private void Awake()
    {
        tick = 0;
        workingMachines = machines.Count;
        achievementsManager = AchievementsManager.achievementsManager;
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
    
    public void AddMachine(Machine newMachine)
    {
        machines.Add(newMachine);
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
        if(workingMachines <= 1) gameManager.EndGame();
    }
}