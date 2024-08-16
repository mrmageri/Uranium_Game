using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace Managers
{
    public class SettingsManager : MonoBehaviour
    {
        [HideInInspector]public float brightness;
        [HideInInspector]public float volume;
        [HideInInspector]public float sensitivityX;
        [HideInInspector]public float sensitivityY;
        private PlayerRotation playerRotation;
        private readonly float defaultBrightness = 0f;
        private readonly float defaultVolume = 1f;
        private readonly float defaultSensitivityX = 125f;
        private readonly float defaultSensitivityY  = 125f;
        
        [SerializeField] private Slider brightnessSlider;
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private Slider sensitivityXSlider;
        [SerializeField] private Slider sensitivityYSlider;
        
        [SerializeField] private TMP_Text brightnessText;
        [SerializeField] private TMP_Text volumeText;
        [SerializeField] private TMP_Text sensitivityXText;
        [SerializeField] private TMP_Text sensitivityYText;

        [SerializeField] VolumeProfile volumeProfile;
        private ColorAdjustments colorAdjustments;
        private SettingsSaver settingsSaver;

        public static SettingsManager iSettingsManager;
        
        SettingsManager()
        {
            iSettingsManager = this;
        }

        private void Awake()
        {
            settingsSaver = (SettingsSaver) FindFirstObjectByType(typeof(SettingsSaver));
            if(volumeProfile.TryGet(out ColorAdjustments colAdjustments)) colorAdjustments = colAdjustments;
            playerRotation = Player.Player.instancePlayer.playerRotation;

            
            float br = 0f;
            float vl = 0f;
            float sX = 0f;
            float sY = 0f;
            if (settingsSaver.LoadSetting())
            {
                br = brightness;
                vl = volume;
                sX = sensitivityX;
                sY = sensitivityY;
            }
            else
            {
                br = colorAdjustments.postExposure.value;
                vl = AudioListener.volume;
                sX = playerRotation.sensX;
                sY = playerRotation.sensY;
            }
            //TODO make it for all players

            brightnessText.text = br.ToString("0.##");
            brightnessSlider.value = br;
            
            volumeText.text = vl.ToString("0.##");
            volumeSlider.value = vl;
            
            sensitivityXText.text = sX.ToString("0");
            sensitivityXSlider.value = sX;
            
            sensitivityYText.text = sY.ToString("0");
            sensitivityYSlider.value = sY;
        }

        public void ResetValues()
        {
            brightness = defaultBrightness;
            colorAdjustments.postExposure.value = brightness;
            brightnessText.text = colorAdjustments.postExposure.value.ToString("0.##");
            brightnessSlider.value = colorAdjustments.postExposure.value;
            
            sensitivityY = sensitivityYSlider.value;
            playerRotation.sensY = sensitivityY;
            sensitivityYText.text = sensitivityY.ToString("0");
            
            sensitivityX =defaultSensitivityX;
            playerRotation.sensX = sensitivityX;
            sensitivityXText.text = playerRotation.sensX.ToString("0");
            sensitivityXSlider.value = sensitivityX;
            
            sensitivityY =defaultSensitivityY;
            playerRotation.sensY = sensitivityY;
            sensitivityYText.text = playerRotation.sensY.ToString("0");
            sensitivityYSlider.value = sensitivityY;
            
            volume = defaultVolume;
            volumeText.text = volume.ToString("0.##");
            volumeSlider.value = volume;
            AudioListener.volume = volume;
            
            settingsSaver.SaveSetting();
        }

        public void BlockPlayer()
        {
            playerRotation.BlockRotation();
        }
        
        public void UnblockPlayer()
        {
            playerRotation.BlockRotation(false);
            settingsSaver.SaveSetting();
        }

        public void SetSensitivityX()
        {
            sensitivityX = sensitivityXSlider.value;
            playerRotation.sensX = sensitivityX;
            sensitivityXText.text = sensitivityX.ToString("0");
        }
        
        public void SetSensitivityY()
        {
            sensitivityY = sensitivityYSlider.value;
            playerRotation.sensY = sensitivityY;
            sensitivityYText.text = sensitivityY.ToString("0");
        }

        public void SetVolume()
        {
            volume = volumeSlider.value;
            AudioListener.volume = volume;
            volumeText.text = volume.ToString("0.##");
        }
        
        public void SetBrightness()
        {
            brightness = brightnessSlider.value;
            colorAdjustments.postExposure.value = brightness;
            brightnessText.text = brightness.ToString("0.##");
        }
    }
}
