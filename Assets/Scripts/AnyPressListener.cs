using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class AnyPressListener : MonoBehaviour {
    public GameObject FadeImageGameObject;

    private Image fadeImage;
    private Color tempColor;

    void Start() {
        fadeImage = FadeImageGameObject.GetComponent<Image>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnMove(InputAction.CallbackContext context) {
        StartCoroutine(FadeOut(0.0f, 1.0f));
    }

    IEnumerator FadeOut(float startValue, float targetValue) {
        FadeImageGameObject.SetActive(true);
        float duration = 2;
        float time = 0;
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
        PlayerProgression.firstLoadSpawn = true;
        SceneManager.LoadScene("PlayState");
    }
}
