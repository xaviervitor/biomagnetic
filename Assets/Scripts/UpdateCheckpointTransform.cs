using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCheckpointTransform : MonoBehaviour {
    void OnTriggerEnter(Collider c) {
        if (c.gameObject.tag == "Player") {
            PlayerProgression.UpdateCheckpointTransform(transform);
            Destroy(gameObject);
        }
    }
}
