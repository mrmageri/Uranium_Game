
using Machines;
using UnityEngine;
using Random = UnityEngine.Random;

public class Jigsaw : Machine
{
    public int maxPercent = 200;
    public int chance = 1;
    [SerializeField] private Material[] materials;
    [SerializeField] private MeshRenderer[] meshes;
    [SerializeField] private AudioClip[] audioClips;
    private AudioSource audioSource;
    //combinations
    private int[] points  = new int[9];
    private int[] combination  = new int[9];
    private const int MAXStage = 9;
    //interaction
    private bool isShowing = false;
    private bool isInteractable = true;

    private new void Awake()
    {
        if (TryGetComponent(out AudioSource au)) audioSource = au;
        points  = new int[9];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = 0;
            meshes[i].material = materials[points[i]];
        }
    }

    public override void OnClick()
    {
        
    }

    public override void OnTick()
    {
        if (Random.Range(0, maxPercent) <= chance && !isBroken)
        {
            GenerateCombination();
            SetBroken();
        }
    }

    public override void Reset()
    {
        points  = new int[9];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = 0;
            meshes[i].material = materials[points[i]];
        }
    }

    public override void ResetBroken()
    {
        GenerateCombination();
    }

    public void OnJigsawClick(int num)
    {
        if(!isInteractable) return;
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.Play();
        if (points[num] + 1 < materials.Length)
        {
            points[num] += 1; 
        }
        else
        {
            points[num] = 0;
        }
        meshes[num].material = materials[points[num]];
        int count = 0;
        for (int i = 0; i < points.Length; i++)
        {
            if (points[i] == combination[i]) count++;
        }
        if (count == MAXStage)
        {
            SetWorking();
            Reset();
        }
    }

    public void ShowCombination()
    {
        if(!isBroken) return;
        isShowing = !isShowing;
        if (isShowing)
        {
            Reset();
            isInteractable = true;
        }
        else
        {
            for (int i = 0; i < combination.Length; i++)
            {
                meshes[i].material = materials[combination[i]];
            }
            isInteractable = false;
        }
    }

    private void GenerateCombination()
    {
        isInteractable = false;
        Reset();
        combination  = new int[9];
        for (int i = 0; i < combination.Length; i++)
        {
            combination[i] = Random.Range(1,materials.Length);
            meshes[i].material = materials[combination[i]];
        }
    }
}
