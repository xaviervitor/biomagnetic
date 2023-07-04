using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class ShowTextTutorial : MonoBehaviour {
    public GameObject Player;
    public GameObject BackgroundImageGameObject;
    public TMP_Text TextObject;
    [TextArea]
    public string Message = "";
    [TextArea]
    public string GamepadMessage = "";
    public float ShowForSeconds = 4;
    private PlayerInput playerInput;
    private Image backgroundImage;

    private Color showColor = new Color(0f, 0f, 0f, 0.5f); 
    private Color hideColor = new Color(0f, 0f, 0f, 0f);
    
    void Start() {
        backgroundImage = BackgroundImageGameObject.GetComponent<Image>();
        playerInput = Player.GetComponent<PlayerInput>();
        playerInput.controlsChangedEvent.AddListener(OnControlsChanged);
    }

    void OnTriggerEnter(Collider c) {
        if (c.gameObject.tag == "Player")
            StartCoroutine(StartText());
    }

    IEnumerator StartText() {
        UpdateText();
        yield return new WaitForSeconds(ShowForSeconds);
        HideText();
        Destroy(gameObject);
	}

    private void UpdateText() {
        if (playerInput.currentControlScheme == "KeyboardMouse") {
            TextObject.text = Message;
        } else if (playerInput.currentControlScheme == "Gamepad") {
            TextObject.text = GamepadMessage;
        }
        ShowBackgroundImage(backgroundImage);
    }

    private void HideText() {
        if (IsThisMessageTheCurrentMessage()) {
            TextObject.text = "";
            HideBackgroundImage(backgroundImage);
        }
    }

    public void OnControlsChanged(PlayerInput input) {
        if (IsThisMessageTheCurrentMessage()) {
            UpdateText();
        }
    }

    private bool IsThisMessageTheCurrentMessage() {
        return TextObject.text == Message || TextObject.text == GamepadMessage;
    }

    private void ShowBackgroundImage(Image background) {
        background.color = showColor;
    }

    private void HideBackgroundImage(Image background) {
        background.color = hideColor;
    }
}
