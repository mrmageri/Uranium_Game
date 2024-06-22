using Machines;
using UnityEngine;
using Random = UnityEngine.Random;


public class Pipes : Machine
{
    public int maxPercent = 1000;
    public int chance = 5;

    [Header("Valves")]
    [SerializeField] private GameObject[] leakingParticles;
    [SerializeField] private Animator[] valveAnimators;
    //Valves
    private int[] defaultStates;
    private int[] currentStates;

    private new void Awake()
    {
        defaultStates = new int[leakingParticles.Length];
        currentStates = new int[leakingParticles.Length];
        for (int i = 0; i < defaultStates.Length; i++)
        {
            defaultStates[i] = Random.Range(1, 5);
            currentStates[i] = defaultStates[i];
            valveAnimators[i].SetInteger("Rotation",currentStates[i]);
        }
    }

    public override void OnClick()
    {
        //Null
    }

    public void ValveClick(int number)
    {
        int status = currentStates[number];
        currentStates[number] = status + 1 > 4 ? 1 : (currentStates[number]+1);
        valveAnimators[number].SetInteger("Rotation",currentStates[number]);
        leakingParticles[number].SetActive(currentStates[number] != defaultStates[number]);
        if(CheckValves()) SetWorking();
    }
    public override void OnTick()
    {
        if ((Random.Range(0, maxPercent) <= chance) && !isBroken)
        {
            SetBroken();
            for (int i = 0; i < currentStates.Length; i++)
            {
                currentStates[i] = Random.Range(1, 5);
                if(currentStates[i] != defaultStates[i]) leakingParticles[i].SetActive(true);
                valveAnimators[i].SetInteger("Rotation",currentStates[i]);
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
        for (int i = 0; i < currentStates.Length; i++)
        {
            currentStates[i] = Random.Range(1, 5);
            valveAnimators[i].SetInteger("Rotation",currentStates[i]);
        }
    }

    private bool CheckValves()
    {
        for (int i = 0; i < currentStates.Length; i++)
        {
            if (currentStates[i] != defaultStates[i]) return false;
        }
        return true;
    }
}
