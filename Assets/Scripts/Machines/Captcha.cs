using Machines;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Captcha : Machine
{
    public int maxPercent = 1000;
    public int chance = 5;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite[] possibleSprites;
    [SerializeField] private Image image;

    [SerializeField] private Transform imageTransform;
    [SerializeField] private float rotationDelta = 30f;
    [SerializeField] private Renderer lightBulb;
    [SerializeField] private Material workingMaterial;
    [SerializeField] private Material brokenMaterial;
 
    public void RotateImage(bool clockwise)
    {
        float rotationZ = 0;
        rotationZ = clockwise ? rotationDelta : -rotationDelta;
        imageTransform.Rotate(0f,0f,rotationZ);
    }

    public override void OnClick()
    {
        if (imageTransform.rotation.eulerAngles.z > 0f && imageTransform.rotation.eulerAngles.z < 30f)
        {
            SetWorking();
            lightBulb.material = workingMaterial;
            image.sprite = defaultSprite;
        }
    }

    public override void OnTick()
    {
        if ((Random.Range(0, maxPercent) <= chance) && !isBroken)
        {
            SetBroken();
            imageTransform.rotation.Set(0f, 0f, 0f, 1f);
            imageTransform.Rotate(0f,0f,Random.Range(1,11) * 30f);
            lightBulb.material = brokenMaterial;
            image.sprite = possibleSprites[Random.Range(0, possibleSprites.Length)];
        }
    }

    public override void Reset()
    {
        SetWorking();
        imageTransform.rotation.Set(0f, 0f, 0f, 1f);
        lightBulb.material = workingMaterial;
        image.sprite = defaultSprite;

    }

    public override void ResetBroken()
    {
        SetBroken();
        imageTransform.rotation.Set(0f, 0f, 0f, 1f);
        imageTransform.Rotate(0f,0f,Random.Range(1,11) * 30f);
        lightBulb.material = brokenMaterial;
        image.sprite = possibleSprites[Random.Range(0, possibleSprites.Length)];
    }
}
