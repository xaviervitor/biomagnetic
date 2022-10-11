using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgression : MonoBehaviour {

	// make this static so it's visible across all instances
	public static PlayerProgression instance = null;

    public static bool hasMagneticBracelet;
    public static bool firstLoadSpawn;
    public static Vector3 checkpointPosition;
    public static Quaternion checkpointRotation;

    public static float startFadeDuration = 2f;

	// singleton pattern
	void Awake() {
		if (instance == null) {
			instance = this;
            Init();
			DontDestroyOnLoad(gameObject);
		} else if (instance != this) {
            Destroy(gameObject);
		}
	}

    public static void Init() {
        firstLoadSpawn = true;
        hasMagneticBracelet = false;
    }

    public static void UpdateCheckpointTransform(Transform checkpoint) {
        checkpointPosition = checkpoint.position;
        checkpointRotation = checkpoint.rotation;
    }

    public static void UpdatePlayerTransform(Transform playerTransform) {
        if (firstLoadSpawn) {
            firstLoadSpawn = false;
        } else {
            playerTransform.position = checkpointPosition;
            playerTransform.rotation = checkpointRotation;
        }
    }
}
