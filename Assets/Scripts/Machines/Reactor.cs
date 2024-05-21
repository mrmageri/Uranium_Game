using Items;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Machines
{
    public class Reactor : Machine
    {
        [Header("UI")]
        [SerializeField] private Slider scale;
        [SerializeField] private TMP_Text textField;
        [SerializeField] private string doorWarning;
        
        [Header("Items")]
        [SerializeField] private GameObject emptyBucket;
        [SerializeField] private Transform spawnTransform;

        [Header("Door")] 
        public UnityEvent onDoorInteraction;
        
        private GameObject currentObj;
        
        [Header("Steps and Uranium")]
        [SerializeField] int maxUranium = 10;
        private int uranium = 10;
        [SerializeField] private int maxAdd = 5;
        private int addedAmount = 0;
        private bool isAddingUranium = false;

        private bool isClosed = true;

        [SerializeField] private int tickRepeatDefault = 8;
        private int tickRepeat = 0;
        private int tick = 0;


        private new void Awake()
        {
            player = Player.Player.instancePlayer;
            playerGraber = player.playerGraber;
            scale.maxValue = maxUranium;
            uranium = maxUranium;
            tickRepeat = tickRepeatDefault;
            DisplayInfo();
        }

        private void OnTriggerEnter(Collider other)
        {
            currentObj = other.gameObject;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == currentObj)
            {
                currentObj = null;
            }
        }

        public override void OnClick()
        {
            if (isClosed)
            {
                if (uranium < maxUranium && !isAddingUranium && currentObj.TryGetComponent(out Item item) && item.itemTag == ItemTag.BucketUranium)
                {
                    isAddingUranium = true;
                    tickRepeat = tickRepeatDefault / 2;
                }
                textField.text = "";
            }
            else
            {
                textField.text = doorWarning;
            }
        }

        public override void OnTick()
        {
            tick++;
            if (tick % tickRepeat == 0 && tick != 0)
            {
                tick = 0;
                if (isAddingUranium)
                {
                    if (addedAmount + 1 <= maxAdd && uranium + 1 <= maxUranium)
                    {
                        addedAmount++;
                        uranium++;
                        if(!isBroken && uranium == maxUranium) SetWorking();
                        DisplayInfo();
                    }
                    if (addedAmount == maxAdd || uranium == maxUranium)
                    {
                        isAddingUranium = false;
                        tickRepeat = tickRepeatDefault;
                        Destroy(currentObj);
                        currentObj = Instantiate(emptyBucket, spawnTransform.position, Quaternion.identity);
                    }
                }
                else
                {
                    if (uranium - 1 >= 0)
                    {
                        uranium--;
                        if(uranium == 0) SetBroken();
                        DisplayInfo(); 
                    }
                }
            }
        }

        public override void Reset()
        {
            SetWorking();
            uranium = maxUranium;
            DisplayInfo();
        }

        public void DoorInteraction()
        {
            if (isAddingUranium) return;
            
            onDoorInteraction.Invoke(); 
            isClosed = !isClosed;
        }
        
        public override void ResetBroken()
        {
            SetBroken();
        }

        private void DisplayInfo()
        {
            scale.value = uranium;
        }
        
    }
}
