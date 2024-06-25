using System;
using System.Collections;
using System.Collections.Generic;
using Machines;
using Managers;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Computer : MonoBehaviour
{
    public TMP_Text textField;
    public TMP_Text tvTextField;

    [SerializeField] private Color defaultColor;
    [SerializeField] private Color errorColor;

    [Header("Machines")] 
    [SerializeField] private Machine[] machines;
    [SerializeField] private string[] machineNames;
    
    [Header("Texts")]
    [SerializeField] private string welcomeText;
    [SerializeField] private string helpText;
    [SerializeField] private string achieveHelpText;
    [SerializeField] private string coffeeText;
    [SerializeField] private string commandErrorText;
    
    
    [SerializeField] private Player.Player player;
    private AchievementsManager achievementsManager;
    
    
    [SerializeField] private float delay = 0.25f;
    
    
    private readonly Array keyCodes = Enum.GetValues(typeof(KeyCode));
    
    private int userNumber = 0;
    
    
    private bool isOnComputerText;
    private bool isWritingText;
    private bool isActive;
    
    public static Computer instanceComputer;
        
    Computer()
    {
        instanceComputer = this;
    }

    private void Awake()
    {
        achievementsManager = AchievementsManager.achievementsManager;
        userNumber = Random.Range(1, 1000);
    }

    private void Start()
    {
        List<Machine> tickManagerMachines = TickManager.instanceTickManager.machines;
        int count = 0;
        for (int i = 0; i < tickManagerMachines.Count; i++)
        {
            if (tickManagerMachines[i].countInComputer)
            {
                count++;
            }
        }
        machines = new Machine[count];
        machineNames = new string[count];
        count = 0;
        for (int i = 0; i < tickManagerMachines.Count; i++)
        {
            if (tickManagerMachines[i].countInComputer)
            {
                machines[count] = tickManagerMachines[i];
                machineNames[count] = tickManagerMachines[i].machineName;
                count++;
            }
        }
        UpdateWorkingMachinesNumber();
    }

    private void Update()
    {
        if(!isActive) return;
        if (Input.anyKeyDown && !isWritingText)
        {
            foreach (KeyCode keyCode in keyCodes)
            {
                if (Input.GetKey(keyCode))
                {
                    textField.color = defaultColor;
                    InputText(Input.inputString, keyCode);
                    break;
                }
            }
        }
    }

    public void Activate()
    {
        player.StopPlayer();
        isActive = true;
        StartCoroutine(DisplayText(welcomeText + userNumber));
    }
    public void Deactivate()
    {
        textField.color = defaultColor;
        StopCoroutine(DisplayText(""));
        player.ActivatePlayer();
        isActive = false;
        textField.text = "_";
    }
    
    public void UpdateWorkingMachinesNumber()
    {
        int count = 0;
        foreach (var elem in machines)
        {
            if (!elem.isBroken) count++;
        }
        tvTextField.text = Convert.ToString(count);
    }
    
    private IEnumerator DisplayText(string new_text)
    {
        isWritingText = true;
        isOnComputerText = true;
        textField.text = "";
        for (int i = 0; i < new_text.Length; i++)
        {
            yield  return new WaitForSeconds(delay);
            if(!isActive) break;
            textField.text += new_text[i];
        }
        isWritingText = false;
    }

    private void InputText(string symbol, KeyCode keyCode)
    {
        if (isOnComputerText)
        {
            textField.text = "";
            isOnComputerText = false;
        }

        if (keyCode == KeyCode.Return)
        {
            GetText();
        }
        if (keyCode == KeyCode.Delete || keyCode == KeyCode.Backspace)
        {
            if (textField.text.Length > 0)
            {
                string text = textField.text.Remove(textField.text.Length - 1);
                textField.text = text;
            }
        }
        else
        {
            textField.text += symbol;
        }
    }
    
    private void GetText()
    {
        if (textField.text.Contains(".sta"))
        {
            isOnComputerText = true;
            int count = -1;
            string machineName = "";

            for (int i = 0; i < machineNames.Length; i++)
            {
                if (textField.text.Contains(machineNames[i]))
                {
                    machineName = machineNames[i];
                    count = i;
                    break;
                }
            }

            if (count == -1)
            {
                if (textField.text.Contains(" h"))
                {
                    string text = "";
                    foreach (var elem in machineNames)
                    {
                        text += ", " + elem;
                    }
                    StartCoroutine(DisplayText("Allowed machines:" + text));
                }
                else
                {
                    StartCoroutine(DisplayText(commandErrorText));
                    textField.color = errorColor;
                }
            }
            else
            {
                string machineStatusStr = "";
                if (machines[count].isBroken)
                {
                    machineStatusStr = "\nIs broken...";
                }
                else
                {
                    machineStatusStr = "\nIs working...";
                }
                StartCoroutine(DisplayText("C:/machines/" + machineName + ".exe" + machineStatusStr));
            }
            return;
        }
        if (textField.text.Contains(".ach"))
        {
            if (textField.text.Contains(" h"))
            {
                StartCoroutine(DisplayText(achieveHelpText));
                return;
            }
            if (textField.text.Contains(" m"))
            {
                StartCoroutine(DisplayText(achievementsManager.achievements.Count.ToString()));
                return;
            }
            if ((FindNumber() - 1)  >= achievementsManager.achievements.Count || FindNumber() <= 0)
            {
                StartCoroutine(DisplayText(commandErrorText));
                textField.color = errorColor; ;
                return;
            }
            isOnComputerText = true;
            StartCoroutine(DisplayText(achievementsManager.achievements[FindNumber() - 1].achDescription));
        }
        if (textField.text.Contains(".h"))
        {
            isOnComputerText = true;
            StartCoroutine(DisplayText(helpText));
            return;
        }
        if (textField.text.Contains(".cof"))
        {
            isOnComputerText = true;
            StartCoroutine(DisplayText(coffeeText));
            return;
        }


        if (textField.text != "" && textField.text != "_")
        {
            StartCoroutine(DisplayText(commandErrorText));
            textField.color = errorColor;
        }
    }
    
    private int FindNumber()
    {
        string number = "";
        for (int i = 0; i < textField.text.Length;i++)
        {
            if (Char.IsDigit(textField.text[i]) && (textField.text[i-1] == ' ' || Char.IsDigit(textField.text[i-1])))
            {
                number += textField.text[i];
            }
        }
        return number == "" ? -1 : Convert.ToInt32(number);
    }
}
