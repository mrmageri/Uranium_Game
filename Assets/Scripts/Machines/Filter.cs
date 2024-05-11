using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Machines
{
    public class Filter : Machine
    {
        [SerializeField] private TMP_Text screenText;
        [SerializeField] private TMP_Text captchaText;
        [SerializeField] private string defaultText;
        [SerializeField] private string brokenText;
        [SerializeField] private string errorText;

        public int maxPercent = 1000;
        public int chance = 5;

        [SerializeField] private float delay = 0.25f;

        private readonly Array keyCodes = Enum.GetValues(typeof(KeyCode));

        private bool isWritingText;

        private int currentSymbolNum = 0;

        private int[] captcha;
        private string captchaStr;

        private new void Awake()
        {
            player = Player.Player.instancePlayer;
            screenText.text = defaultText;
            captchaText.text = "";
        }

        public void ButtonInput(int num)
        {
            if(!isBroken) return;
            if(isWritingText) return;
            if (num == captcha[currentSymbolNum])
            {
                if (currentSymbolNum == 0) screenText.text = "";
                screenText.text += num;
                currentSymbolNum++;
                if (screenText.text == captchaStr)
                {
                    SetWorking();
                    screenText.text = defaultText;
                    captchaText.text = "";
                    currentSymbolNum = 0;
                    captcha = null;
                    captchaStr = "";
                }
            }
            else
            {
                screenText.text = errorText;
                currentSymbolNum = 0;
            }
        }

        public override void OnTick()
        {
            if ((Random.Range(0, maxPercent) <= chance) && !isBroken)
            {
                SetBroken();
                GenerateCaptcha(Random.Range(6, 10));
                StartCoroutine(DisplayText(brokenText, screenText));
                StartCoroutine(DisplayText(captchaStr, captchaText));
            }
        }

        public override void OnClick()
        {
            
        }

        private IEnumerator DisplayText(string new_text, TMP_Text inputText)
        {
            isWritingText = true;
            inputText.text = "";
            for (int i = 0; i < new_text.Length; i++)
            {
                yield return new WaitForSeconds(delay);
                inputText.text += new_text[i];
            }

            isWritingText = false;
        }

        private void GenerateCaptcha(int len)
        {
            captcha = new int[len];
            captchaStr = "";
            for (int i = 0; i < len; i++)
            {
                captcha[i] = Random.Range(1,4);
                captchaStr += captcha[i];
            }
        }
        
        
    }

}