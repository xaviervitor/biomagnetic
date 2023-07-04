using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpenPortalDoor : MonoBehaviour {
    public GameObject ButtonPressurePlate1;
    public GameObject ButtonPressurePlate2;
    public GameObject ButtonPressurePlate3;
    public GameObject FadeImageGameObject;

    private PressureButton pressureButton1;
    private PressureButton pressureButton2;
    private PressureButton pressureButton3;

    private Image fadeImage;
    private Color tempColor;

    private bool activated = false;
    private Vector3 newScale = new Vector3(1f, 1f, 1f);
    private float targetScaleY = 0.5f;
    private float doorSpeed = 0.5f;
    
    void Start() {
        pressureButton1 = ButtonPressurePlate1.GetComponent<PressureButton>();
        pressureButton2 = ButtonPressurePlate2.GetComponent<PressureButton>();
        pressureButton3 = ButtonPressurePlate3.GetComponent<PressureButton>();
        fadeImage = FadeImageGameObject.GetComponent<Image>();
    }

    void Update() {
        if (!activated) {
            bool isButtonPressed1 = !pressureButton1.Pressed;
            bool isButtonPressed2 = !pressureButton2.Pressed;
            bool isButtonPressed3 = !pressureButton3.Pressed;

            if (isButtonPressed1 && isButtonPressed2 && isButtonPressed3) {
                targetScaleY = 0.05f;
            } else {
                targetScaleY = 0.5f;
            }
        } else {
            targetScaleY = 0.5f;
        }
        
        // Loop through all children
        float increment = Time.deltaTime * doorSpeed;
        foreach (Transform child in transform) {
            if (targetScaleY - child.localScale.y > increment) {
                newScale.y = child.localScale.y + increment;
            } else if (child.localScale.y - targetScaleY > increment) {
                newScale.y = child.localScale.y - increment;
            } else {
                newScale.y = targetScaleY;
            }
            child.localScale = newScale;
        }
    }

    void OnTriggerEnter(Collider c) {
        if (c.gameObject.tag == "Player") {
            activated = true;
            StartCoroutine(FadeOut(0.0f, 1.0f));
        }        
    }

    IEnumerator FadeOut(float startValue, float targetValue) {
        yield return new WaitForSeconds(2);
        FadeImageGameObject.SetActive(true);
        float duration = 4;
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
        SceneManager.LoadScene("EndState");
    }
}
