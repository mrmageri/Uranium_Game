using Machines;
using UnityEngine;
using Random = UnityEngine.Random;


public class Pipes : Machine
{
    public int maxPercent = 1000;
    public int chance = 5;

    [Header("Valves")]
    [SerializeField] private GameObject[] leakingParticles;
    [SerializeField] private int[] defaultValveStates;
    [SerializeField] private Animator[] valveAnimators;
    private int[] valvesStates;

    private void Start()
    {
        valvesStates = new int[defaultValveStates.Length];
        for (int i = 0; i < defaultValveStates.Length; i++)
        {
            valvesStates[i] = defaultValveStates[i];
            valveAnimators[i].SetInteger("Rotation",valvesStates[i]);
        }
    }

    public override void OnClick()
    {
        //Null
    }

    public void ValveClick(int number)
    {
        int status = valvesStates[number];
        valvesStates[number] = status + 1 > 4 ? 1 : (valvesStates[number]+1);
        valveAnimators[number].SetInteger("Rotation",valvesStates[number]);
        leakingParticles[number].SetActive(valvesStates[number] != defaultValveStates[number]);
        if(CheckValves()) SetWorking();
    }
    public override void OnTick()
    {
        if ((Random.Range(0, maxPercent) <= chance) && !isBroken)
        {
            SetBroken();
            for (int i = 0; i < valvesStates.Length; i++)
            {
                valvesStates[i] = Random.Range(1, 5);
                if(valvesStates[i] != defaultValveStates[i]) leakingParticles[i].SetActive(true);
                valveAnimators[i].SetInteger("Rotation",valvesStates[i]);
            }
        }
    }

    public override void Reset()
    {
        SetWorking();
    }

    public override void ResetBroken()
    {
        SetBroken();
        for (int i = 0; i < valvesStates.Length; i++)
        {
            valvesStates[i] = Random.Range(1, 5);
            valveAnimators[i].SetInteger("Rotation",valvesStates[i]);
        }
    }

    private bool CheckValves()
    {
        for (int i = 0; i < valvesStates.Length; i++)
        {
            if (valvesStates[i] != defaultValveStates[i]) return false;
        }
        return true;
    }
}
