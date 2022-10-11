using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOnStart : MonoBehaviour {
    
    public GameObject FadeImageGameObject;

    private Image fadeImage;
    private Color tempColor;
    
    void Start() {
        fadeImage = FadeImageGameObject.GetComponent<Image>();
        StartCoroutine(FadeIn(1.0f, 0.0f));
    }

    IEnumerator FadeIn(float startValue, float targetValue) {
        float duration = PlayerProgression.startFadeDuration;
        float time = 0;
        FadeImageGameObject.SetActive(true);
        while (time < duration) {
            tempColor = fadeImage.color;
            tempColor.a = Mathf.Lerp(startValue, targetValue, time / duration);
            fadeImage.color = tempColor;
            time += Time.deltaTime;
            yield return null;
        }
        tempColor = fadeImage.color;    
        tempColor.a = targetValue;
        fadeImage.color = tempColor;
        PlayerProgression.startFadeDuration = 2f;
        FadeImageGameObject.SetActive(false);
    }
}
