using Items;
using Machines;
using UnityEngine;

public class TV : Machine
{
    public int maxPercent = 200;
    public int chance = 1;
    
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameObject[] smoke;
    private Renderer _renderer;
    private int spriteCount = 0;
    protected void Start()
    {
        gameObject.TryGetComponent(out Renderer rend);
        _renderer = rend;
        var materials = _renderer.materials;
        materials[1] = new Material(materials[1]);
        _renderer.materials[1].SetTexture("_MainTex",sprites[spriteCount].texture);
    }

    public override void OnClick()
    {
        SetTexture();
        if(!isBroken) return;
        if (playerGraber.heldObj != null && playerGraber.heldObj.TryGetComponent(out Item item) &&
            item.itemTag == requiredTag)
        {
            SetWorking();
            foreach (var elem in smoke)
            {
                elem.SetActive(false);
            }
        }
    }

    public override void OnTick()
    {
        if ((Random.Range(0, maxPercent) <= chance) && !isBroken)
        {
            SetBroken();
            foreach (var elem in smoke)
            {
                elem.SetActive(true);
            }
        }
    }

    public override void Reset()
    {
        SetWorking();
        foreach (var elem in smoke)
        {
            elem.SetActive(false);
        }
    }

    public override void ResetBroken()
    {
        SetBroken();
        foreach (var elem in smoke)
        {
            elem.SetActive(true);
        }
    }
    private void SetTexture()
    {
        spriteCount = spriteCount + 1 >= sprites.Length ? 0 : spriteCount + 1;
        _renderer.materials[1].SetTexture("_MainTex",sprites[spriteCount].texture);
    }
}
