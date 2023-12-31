using UnityEngine;
using UnityEngine.InputSystem;

namespace StarterAssets {
	public class StarterAssetsInputs : MonoBehaviour {
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool walk;
		public bool interact;
		public bool secondaryInteract;
		public float changeHeldObjectDistance;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		public void OnMove(InputAction.CallbackContext context) {
			MoveInput(context.ReadValue<Vector2>());
		}

		public void OnLook(InputAction.CallbackContext context) {
			if(cursorInputForLook) {
				LookInput(context.ReadValue<Vector2>());
			}
		}

		public void OnJump(InputAction.CallbackContext context) {
			JumpInput(context.performed);
		}

		public void OnSprint(InputAction.CallbackContext context) {
			SprintInput(context.action.ReadValue<float>() == 1);
		}

		public void OnWalk(InputAction.CallbackContext context) {
			WalkInput(context.action.ReadValue<float>() == 1);
		}

		public void OnInteract(InputAction.CallbackContext context) {
			InteractInput(context.performed);
		}

		public void OnSecondaryInteract(InputAction.CallbackContext context) {
			SecondaryInteractInput(context.performed);
		}

		public void OnChangeHeldObjectDistance(InputAction.CallbackContext context) {
			ChangeHeldObjectDistanceInput(context.action.ReadValue<float>());
		}

		public void MoveInput(Vector2 newMoveDirection) {
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection) {
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState) {
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState) {
			sprint = newSprintState;
		}

		public void WalkInput(bool newWalkState) {
			walk = newWalkState;
		}

		public void InteractInput(bool newInteractState) {
			interact = newInteractState;
		}

		public void SecondaryInteractInput(bool newInteractState) {
			secondaryInteract = newInteractState;
		}

		public void ChangeHeldObjectDistanceInput(float newInteractState) {
			changeHeldObjectDistance = newInteractState;
		}
		
		private void OnApplicationFocus(bool hasFocus) {
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState) {
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}