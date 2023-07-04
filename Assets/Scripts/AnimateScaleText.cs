using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnimateScaleText : MonoBehaviour {
    public float targetFontSize = 30.0f;
    public float AnimationSpeed = 1.5f;
    
    private TMP_Text text;
    private float originalFontSize;

    void Start() {
        text = GetComponent<TMP_Text>();
        originalFontSize = text.fontSize;
    }

    void Update() {
        // Sin function that goes from 0 to 1 with double speed
        float doubleSinZeroToOne = (Mathf.Sin(Time.time * AnimationSpeed) + 1.0f) / 2;
        // lerps values from [0, 1] to [originalFontSize, targetFontSize] 
        float fontSize = Mathf.Lerp(originalFontSize, targetFontSize, doubleSinZeroToOne);
        text.fontSize = fontSize;
    }
}
