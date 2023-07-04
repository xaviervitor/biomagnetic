using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitBeforeColliderEnable : MonoBehaviour {
    
    public float WaitSeconds = 0;

    private new Collider collider;
    void Start() {
        collider = GetComponent<Collider>();
        StartCoroutine(EnableColliderAfterSeconds());
    }

    IEnumerator EnableColliderAfterSeconds() {
        yield return new WaitForSeconds(WaitSeconds);
        collider.enabled = true;
    }
}
