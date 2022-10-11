using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class SpawnJenga : MonoBehaviour {
    public GameObject Player;
    public GameObject JengaPrefab;
    public GameObject BigJengaPrefab;
    public Vector3 jengaPosition = new Vector3(0f, 1.5f, 5f);
    public Vector3 bigJengaPosition = new Vector3(0f, 1.5f, -20f);

    private GameObject jengaInstance;
    private GameObject bigJengaInstance;
    private PressureButton pressureButton;
    private bool pressedInPreviousFrame = false;
    private PlayerInteract playerInteract;

    void Start() {
        pressureButton = GetComponent<PressureButton>();
        playerInteract = Player.GetComponent<PlayerInteract>();
        InstantiateJenga();
    }

    void Update() {
        if (pressureButton.Pressed) {
            if (pressedInPreviousFrame) return;
            if (playerInteract.heldObject != null) {
                playerInteract.ReleaseObject();
            }
            if (jengaInstance) Destroy(jengaInstance);
            InstantiateJenga();
            if (bigJengaInstance) Destroy(bigJengaInstance);
            InstantiateBigJenga();
            pressedInPreviousFrame = true;
        } else {
            if (pressedInPreviousFrame) {
                pressedInPreviousFrame = false;
            }
        }
    }

    void InstantiateJenga() {
        jengaInstance = Instantiate(JengaPrefab, jengaPosition, Quaternion.identity);
    }

    void InstantiateBigJenga() {
        bigJengaInstance = Instantiate(BigJengaPrefab, bigJengaPosition, Quaternion.identity);
    }
}
