using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using StarterAssets;

public class PlayerPause : MonoBehaviour {
    public GameObject PauseMenu;
    public GameObject FadeImageGameObject;

    private Image fadeImage;
    private Color tempColor;
    private PlayerInput playerInput;
    private StarterAssetsInputs starterAssetsInput;
    private bool isPauseButtonDown;
    private bool isUnpauseButtonDown;

    void Start() {
        starterAssetsInput = GetComponent<StarterAssetsInputs>();
        playerInput = GetComponent<PlayerInput>();
        fadeImage = FadeImageGameObject.GetComponent<Image>();
        isPauseButtonDown = false;
        isUnpauseButtonDown = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnPause(InputAction.CallbackContext context) {
        if (context.performed) {
            if (isPauseButtonDown) return;
            Pause();
        }
        isPauseButtonDown = context.performed;
    }

    public void OnUnpause(InputAction.CallbackContext context) {
        if (context.performed) {
            if (isUnpauseButtonDown) return;
            Unpause();
        }
        isUnpauseButtonDown = context.performed;
    }

    public void Pause() {
        Time.timeScale = 0;
        playerInput.SwitchCurrentActionMap("Paused");
        starterAssetsInput.cursorLocked = false;
        starterAssetsInput.cursorInputForLook = false;
        Cursor.lockState = CursorLockMode.None;
        PauseMenu.SetActive(true);
    }

    public void Unpause() {
        Time.timeScale = 1;
        playerInput.SwitchCurrentActionMap("Player");
        starterAssetsInput.cursorLocked = true;
        starterAssetsInput.cursorInputForLook = true;
        Cursor.lockState = CursorLockMode.Locked;
        PauseMenu.SetActive(false);
    }

    public void OnRestart(InputAction.CallbackContext context) {
        if (context.performed) {
            fadeImage.color = Color.white;
            Cursor.lockState = CursorLockMode.Locked;
            StartCoroutine(FadeAndReloadScene(0.0f, 1.0f));
        }
    }

    public void OnQuit(InputAction.CallbackContext context) {
        if (context.performed) {
            fadeImage.color = Color.black;
            StartCoroutine(FadeAndQuit(0.0f, 1.0f));
        }
    }

    IEnumerator FadeAndQuit(float startValue, float targetValue) {
        float duration = 1f;
        float time = 0;
        FadeImageGameObject.SetActive(true);
        while (time < duration) {
            tempColor = fadeImage.color;
            tempColor.a = Mathf.Lerp(startValue, targetValue, time / duration);
            fadeImage.color = tempColor;
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        tempColor = fadeImage.color;    
        tempColor.a = targetValue;
        fadeImage.color = tempColor;
        Application.Quit();
    }

    IEnumerator FadeAndReloadScene(float startValue, float targetValue) {
        float duration = 1f;
        float time = 0;
        FadeImageGameObject.SetActive(true);
        while (time < duration) {
            tempColor = fadeImage.color;
            tempColor.a = Mathf.Lerp(startValue, targetValue, time / duration);
            fadeImage.color = tempColor;
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        tempColor = fadeImage.color;    
        tempColor.a = targetValue;
        fadeImage.color = tempColor;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
