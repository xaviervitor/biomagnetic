using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class UpdatePauseMenuText : MonoBehaviour {
    public GameObject Player;
    public TMP_Text TextObject;
    
    private PlayerInput playerInput;

    private string keyboardMouseMessage;
    private string gamepadMessage;

    void Start() {
        playerInput = Player.GetComponent<PlayerInput>();
        playerInput.controlsChangedEvent.AddListener(OnControlsChanged);
    
        keyboardMouseMessage = "<color=#11D669>Esc</color> to <color=#11D669>Resume</color>\n<color=#00C9FF>R</color> to <color=#00C9FF>Restart</color>\n<color=#FF3C23>Q</color> to <color=#FF3C23>Quit</color>";
        gamepadMessage = "<color=#11D669>START</color> to <color=#11D669>Resume</color>\n<color=#00C9FF>SELECT</color> to <color=#00C9FF>Restart</color>\n<color=#FF3C23>L2+R2</color> to <color=#FF3C23>Quit</color>";
        UpdateText();
    }

    public void OnControlsChanged(PlayerInput input) {
        UpdateText();
    }

    private void UpdateText() {
        if (playerInput.currentControlScheme == "KeyboardMouse") {
            TextObject.text = keyboardMouseMessage;
        } else if (playerInput.currentControlScheme == "Gamepad") {
            TextObject.text = gamepadMessage;
        }
    }
}
