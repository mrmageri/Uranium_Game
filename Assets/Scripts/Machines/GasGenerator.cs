using Items;
using Machines;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GasGenerator : Machine
{
    public int maxPercent = 1000;
    public int chance = 5;
    [SerializeField] private Renderer indicatorRederer;
    [SerializeField] private Material brokenMaterial, workingMaterial;
    [SerializeField] private GameObject gasBalloon;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject emptyBalloon;
    private bool _hasCylinder;
    private Animator _animator;

    private void Start()
    {
        _animator = TryGetComponent(out Animator anim) ? anim : null;
    }

    public override void OnClick()
    {
        if (playerGraber.heldObj != null && playerGraber.heldObj.TryGetComponent(out Item item) &&
            item.itemTag == requiredTag)
        {
            playerGraber.DestroyItem();
            _hasCylinder = true;
            //Add gas adding animation
            //_animator.SetTrigger("");
            gasBalloon.SetActive(true);
            SetWorking();
            indicatorRederer.material = workingMaterial;
        }
    }

    public void ReleaseBalloon()
    {
        if (_hasCylinder)
        {
            gasBalloon.SetActive(false);
            Instantiate(emptyBalloon, spawnPoint.position, quaternion.identity);
            _hasCylinder = false;
        }
    }
    
    public override void OnTick()
    {
        if ((Random.Range(0, maxPercent) <= chance) && !isBroken)
        {
            SetBroken();
            indicatorRederer.material = brokenMaterial;
        }
    }
    
    public override void Reset()
    {
        SetWorking();
        indicatorRederer.material = workingMaterial;
    }
    
    public override void ResetBroken()
    {
        SetBroken();
        indicatorRederer.material = brokenMaterial;
    }
}
