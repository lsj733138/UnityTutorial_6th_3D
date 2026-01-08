using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Farm
{
    public class FadeEvent : MonoBehaviour
    {
        private Image fadeImage;
        public static event Action<float, Color, bool> fadeAction;
        
        private void Awake()
        {
            fadeImage = transform.Find("Fade Image").GetComponent<Image>();
        }

        private void OnEnable()
        {
            fadeAction += Fade;
        }

        private void OnDisable()
        {
            fadeAction -= Fade;
        }

        public static void FadeInvoke(float fadeTime, Color fadeColor, bool isFade)
        {
            fadeAction?.Invoke(fadeTime, fadeColor, isFade);
        }

        private void Fade(float fadeTime, Color fadeColor, bool isFade)
        {
            fadeImage.gameObject.SetActive(true);
            StartCoroutine(FadeRoutine(fadeTime, fadeColor, isFade));
        }
        
        private IEnumerator FadeRoutine(float fadeTime, Color fadeColor, bool isFade)
        {
            float timer = 0f;
            float percent = 0f;

            while (percent <= 1f)
            {
                timer += Time.deltaTime;
                percent = timer / fadeTime;

                float value = isFade ? percent : percent - 1;
                fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, value);
                yield return null;
            }
            
            if (!isFade)
                fadeImage.gameObject.SetActive(false);
        }
    }
}