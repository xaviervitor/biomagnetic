using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeChildrenRigidbodyNeverSleep : MonoBehaviour {
    void Start() {
        foreach (Transform child in transform) {
            child.GetComponent<Rigidbody>().sleepThreshold = 0.0f;
        }
    }
}
