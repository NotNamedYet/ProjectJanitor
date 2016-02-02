using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace GalacticJanitor.UI
{
    public enum ProgressBarTextFormat { PERCENT, CURR_OVER_MAX, CURR_ONLY, FULL }

    public class ProgressBar : MonoBehaviour
    {

        //PUBLICS
        public RectTransform holder;
        public bool flipDirection;
        public bool verticalDisplay;

        [Header("Slider")]
        public Image sliderLayout;

        [Header("Burner")]
        public bool enableBurnEffect;
        public Image burnerLayout;
        public float burnSpeed;
        public float burnDelay;

        [Header("Text Info")]
        public bool showText;
        public Text textComponent;
        public ProgressBarTextFormat textFormat;
        public string prefix;

        //PRIVATES
        float holderSizeX;
        float holderSizeY;

        string format;
        bool needToBurn = false;

        void Start() { }
        void Awake()
        {
            InitLayouts();
            InitTextComponent();
        }

        /// <summary>
        /// Burn effect is calculated here.
        /// </summary>
        void Update()
        {
            UpdateBurner();
        }

        /// <summary>
        /// Update the visual state of layouts according to the given values
        /// -- Entry gate of this Script. Should be called from outside on Update()
        /// </summary>
        /// <param name="currentValue">The current progress value</param>
        /// <param name="maxValue">The target maximum value of the progression</param>
        public void UpdateProgress(float currentValue, float maxValue)
        {
            UpdateText(currentValue, maxValue);

            float y;
            float x;

            if (verticalDisplay)
            {
                float offsetY = currentValue * holderSizeY / maxValue;

                y = (flipDirection) ? holderSizeY - offsetY : offsetY - holderSizeY;
                x = sliderLayout.rectTransform.localPosition.x;

            }
            else
            {
                float offsetX = currentValue * holderSizeX / maxValue;

                x = (flipDirection) ? holderSizeX - offsetX : offsetX - holderSizeX;
                y = sliderLayout.rectTransform.localPosition.y;
            }

            sliderLayout.rectTransform.localPosition = new Vector3(x, y);

            CallBurn();
        }

        /// <summary>
        /// Update the atached text component (if it exists and active)
        /// </summary>
        /// <param name="currentValue">The current value to display</param>
        /// <param name="maxValue">The max value to display</param>
        void UpdateText(float currentValue, float maxValue)
        {
            if (!textComponent) return;
            textComponent.text = string.Format(format, (prefix.Length == 0) ? "" : prefix + " : ", currentValue, maxValue, Mathf.Ceil(currentValue * maxValue / 100));
        }

        /// <summary>
        /// Tell the program that burn update is needed
        /// </summary>
        void CallBurn()
        {
            if (burnDelay <= 0)
            {
                Burn();
            }
            else
            {
                Invoke("Burn", burnDelay);
            }
        }

        /// <summary>
        /// Pull the trigger of the burn effet for the next update
        /// </summary>
        /// <param name="delay">Delay the start of the effect</param>
        void Burn()
        {
            needToBurn = true;
        }

        /// <summary>
        /// Update burner layout if active and needed
        /// -- No physics
        /// </summary>
        void UpdateBurner()
        {
            if (!enableBurnEffect) return;

            if (needToBurn)
            {

                Vector3 sliderPos = sliderLayout.rectTransform.localPosition;
                Vector3 burnerPos = burnerLayout.rectTransform.localPosition;

                if (!sliderPos.Equals(burnerPos))
                {
                    float speed = GetBurnSpeed(sliderPos, burnerPos, burnSpeed / 2);
                    burnerLayout.rectTransform.localPosition = Vector3.MoveTowards(burnerPos, sliderPos, speed * Time.deltaTime);
                }
                else
                {
                    needToBurn = false;
                }
            }
        }

        /// <summary>
        /// Calculate the speed of the burn effect by distance comparison. 
        /// </summary>
        /// <param name="current">Vector from</param>
        /// <param name="target">Vector to</param>
        /// <param name="minimumSpeed">minimum speed</param>
        /// <returns></returns>
        float GetBurnSpeed(Vector3 current, Vector3 target, float minimumSpeed)
        {
            float speed = Vector3.Distance(current, target) * burnSpeed;
            return (speed > minimumSpeed) ? speed : minimumSpeed;
        }

        /// <summary>
        /// Bar and effects initialisation -> on Awake
        /// </summary>
        void InitLayouts()
        {
            //Get the holder values for calculation
            holderSizeX = holder.sizeDelta.x;
            holderSizeY = holder.sizeDelta.y;

            //Toggle burn effect
            burnerLayout.gameObject.SetActive(enableBurnEffect);
        }

        /// <summary>
        /// Text component init -> on Awake
        /// </summary>
        void InitTextComponent()
        {
            if (textComponent) textComponent.gameObject.SetActive(showText);

            switch (textFormat)
            {
                case ProgressBarTextFormat.PERCENT:
                    format = "{0}{3}%";
                    break;
                case ProgressBarTextFormat.CURR_OVER_MAX:
                    format = "{0}{1}/{2}";
                    break;
                case ProgressBarTextFormat.CURR_ONLY:
                    format = "{0}{1}";
                    break;
                case ProgressBarTextFormat.FULL:
                    format = "{0}{1}/{2} - {3}%";
                    break;
            }
        }

    } 
}
