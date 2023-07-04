using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AnimatePostProcessing : MonoBehaviour {
    public float WeightMin = 0.5f;
    public float WeightMax = 1.0f;
    public float AnimationSpeed = 1.5f;

    private Volume volume;

    void Start() {
        volume = GetComponent<Volume>();    
    }

    void Update() {
        // Sin function that goes from 0 to 1 with double speed
        float doubleSinZeroToOne = (Mathf.Sin(Time.time * AnimationSpeed) + 1.0f) / 2;
        // lerps values from [0, 1] to [0.5, 1] 
        float weight = Mathf.Lerp(WeightMin, WeightMax, doubleSinZeroToOne);
        volume.weight = weight;
    }
}
