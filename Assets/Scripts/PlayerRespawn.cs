using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour {
    public GameObject FadeImageGameObject;

    private Image fadeImage;
    private MeshCollider deathPlaneCollider;
    private Color tempColor;

    void Start() {
        PlayerProgression.UpdatePlayerTransform(transform);
        fadeImage = FadeImageGameObject.GetComponent<Image>();
    }

    public void OnRespawn(InputAction.CallbackContext context) {
        if (context.performed) {
            StartCoroutine(FadeAndReloadScene(0.0f, 1.0f));
        }
    }

    IEnumerator FadeAndReloadScene(float startValue, float targetValue) {
        float duration = 0.5f;
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
        PlayerProgression.startFadeDuration = 0.5f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
