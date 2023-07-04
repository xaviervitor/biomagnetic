using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathColliderRespawn : MonoBehaviour {
    public GameObject FadeImageGameObject;
    
    private Image fadeImage;
    private Color tempColor;

    void Start() {
        fadeImage = FadeImageGameObject.GetComponent<Image>();
    }

    void OnTriggerEnter(Collider c) {
        if (c.gameObject.tag == "Player") {
            StartCoroutine(FadeAndReloadScene(0.0f, 1.0f));
        }
    }

    IEnumerator FadeAndReloadScene(float startValue, float targetValue) {
        float duration = 2f;
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
        SceneManager.LoadScene("PlayState");
    }
}
