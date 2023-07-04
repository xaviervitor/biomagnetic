using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrail : MonoBehaviour {
    public List<GameObject> TrailList = new List<GameObject>();
    public List<bool> reversed;
    public Material defaultTrailMaterial;
    public Material emissiveTrailMaterial;
    public Material defaultTrailMaterialReversed;
    public Material emissiveTrailMaterialReversed;

    private PressureButton pressureButton;
    private List<MeshRenderer> meshRendererList = new List<MeshRenderer>();
    private bool pressedInPreviousFrame = false;
    
    void Start() {
        pressureButton = GetComponent<PressureButton>();
        
        foreach (GameObject trail in TrailList) {
            meshRendererList.Add(trail.GetComponent<MeshRenderer>());
        }
    }

    void Update() {
        bool isButtonPressed = pressureButton.Pressed;

        if (isButtonPressed) {
            if (pressedInPreviousFrame) return;
            for (var i = 0; i < reversed.Count; i++) {
                if (!reversed[i]) {
                    meshRendererList[i].material = emissiveTrailMaterial;
                } else {
                    meshRendererList[i].material = defaultTrailMaterialReversed;
                }
            }
            pressedInPreviousFrame = true;
        } else {
            if (!pressedInPreviousFrame) return;
            for (var i = 0; i < reversed.Count; i++) {
                if (reversed[i]) {
                    meshRendererList[i].material = emissiveTrailMaterialReversed;
                } else {
                    meshRendererList[i].material = defaultTrailMaterial;
                }
            }
            pressedInPreviousFrame = false;
        }
    }
}
