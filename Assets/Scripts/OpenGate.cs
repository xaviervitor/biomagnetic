using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGate : MonoBehaviour {
    public GameObject Gate;
    public Vector3 openedPosition = new Vector3(0f, 2.5f, 0f);
    public float pullForce = 30f;
    public bool reversed = false;

    private Rigidbody gateRigidbody;
    private PressureButton pressureButton;

    void Start() {
        pressureButton = GetComponent<PressureButton>();
        gateRigidbody = Gate.GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        bool isButtonPressed = pressureButton.Pressed;
        if (reversed) isButtonPressed = !isButtonPressed;

        if (isButtonPressed) {
            if (Gate.transform.localPosition.y < openedPosition.y) {
                gateRigidbody.AddForce(Gate.transform.up * pullForce, ForceMode.Force);
            }
        }
    }
}
