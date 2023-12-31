using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

public class PlayerInteract : MonoBehaviour {
	private enum InteractInput {Primary, Secondary};
	public enum CrosshairColor {Pickable, Magnetic, Default};
	public static CrosshairColor CurrentCrosshairColor = CrosshairColor.Default;

	[Header("Reference objects")]
	public GameObject PlayerCameraRotation;
	public GameObject GroundIndicatorPrefab;
	public Material HeldMaterial;
	[Header("Object pull")]
	public float PullSpeed = 10.0f;
	private StarterAssetsInputs input;
	private bool isLeftMouseDown = false;
	private bool isRightMouseDown = false;
	public HeldObject heldObject = null;
	private int layerPickable;
	private int layerMagnetic;
	private int layerHeld;
	private int layerGlass;
	private int layerIgnoreRaycast;
	
	private void Start() {
		input = GetComponent<StarterAssetsInputs>();
		layerPickable = LayerMask.NameToLayer("Pickable");
		layerMagnetic = LayerMask.NameToLayer("Magnetic");
		layerHeld = LayerMask.NameToLayer("Held");
		layerGlass = LayerMask.NameToLayer("Glass");
		layerIgnoreRaycast = LayerMask.NameToLayer("IgnoreRaycast");
	}

	private void FixedUpdate() {
		PushOrPullObject();
		MoveHeldObject();
		GameObject hitGameObject = RaycastObjectInRange();
		PickupOrReleaseObject(hitGameObject);
	}

	private void PickupOrReleaseObject(GameObject hitGameObject) {
		// Detects the player input and stores the value read at the 
		// previous frame in the variable isLeftMouseDown, making the 
		// pickup/drop code only once per click.
		if (input.interact) {
			if (isLeftMouseDown) return;
			if (heldObject != null)
				ReleaseObject();
			else
				PickupObject(hitGameObject, InteractInput.Primary);
			isLeftMouseDown = input.interact; // always true
		} else {
			isLeftMouseDown = input.interact; // always false
		}

		if (input.secondaryInteract) {
			if (isRightMouseDown) return;
			if (heldObject != null)
				ReleaseObject();
			else
				PickupObject(hitGameObject, InteractInput.Secondary);
			isRightMouseDown = input.secondaryInteract; // always true
		} else {
			isRightMouseDown = input.secondaryInteract; // always false
		}
	}

	private void PushOrPullObject() {
		if (heldObject != null) {
			if (heldObject.originalLayer == layerMagnetic) {
				if (input.changeHeldObjectDistance > 0) {
					heldObject.playerToObjectDistance += 0.25f;
				} else if (input.changeHeldObjectDistance < 0) {
					heldObject.playerToObjectDistance -= 0.25f;
				}
			} else if (heldObject.originalLayer == layerPickable) {
				heldObject.playerToObjectDistance = heldObject.initialPlayerToObjectDistance;
			}
		}
	}

	private void MoveHeldObject() {
		if (heldObject != null) {
			heldObject.rigidbody.angularVelocity = Vector3.zero;
			Vector3 targetPosition = PlayerCameraRotation.transform.position + 
					PlayerCameraRotation.transform.forward * heldObject.playerToObjectDistance;
			
			float raycastDistance = Vector3.Distance(heldObject.gameObject.transform.position, targetPosition);
			Vector3 raycastDirection = targetPosition - heldObject.gameObject.transform.position;
			// Wall raycast
			RaycastHit hit;
			if (Physics.Raycast(heldObject.gameObject.transform.position, raycastDirection.normalized, out hit, raycastDistance, ~(1 << layerIgnoreRaycast))) {
				heldObject.playerToObjectDistance = Vector3.Distance(PlayerCameraRotation.transform.position, hit.point);
				targetPosition = hit.point;
			}

			float distance = Vector3.Distance(heldObject.gameObject.transform.position, targetPosition);
			Vector3 pullDirection = targetPosition - heldObject.gameObject.transform.position;
			heldObject.rigidbody.AddForce(pullDirection.normalized * distance * PullSpeed, ForceMode.VelocityChange);
		}
	}

	private GameObject RaycastObjectInRange() {
		GameObject hitGameObject;
		// Interact raycast
		RaycastHit hit;
		Ray ray = new Ray(PlayerCameraRotation.transform.position, PlayerCameraRotation.transform.forward);
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~(1 << layerGlass | 1 << layerIgnoreRaycast))) {
			hitGameObject = hit.transform.gameObject;
			if (hitGameObject.layer == layerMagnetic && PlayerProgression.hasMagneticBracelet)
				CurrentCrosshairColor = CrosshairColor.Magnetic;
			else if (hitGameObject.layer == layerPickable && hit.distance <= 2.50f)
				CurrentCrosshairColor = CrosshairColor.Pickable;
			else {
				// When a object leaves the range but the crosshair 
				// keeps looking at it
				CurrentCrosshairColor = CrosshairColor.Default;
				return null;
			}
		} else {
			// When the crosshair is pointed at an object and its pointed 
			// at another object outside of the Interactable Layers right after
			CurrentCrosshairColor = CrosshairColor.Default;
			return null;
		}
		return hitGameObject;
	}

	private void PickupObject(GameObject hitGameObject, InteractInput interaction) {
		if (!hitGameObject) return;
		heldObject = new HeldObject(hitGameObject);
		GameObject groundIndicatorInstance = (GameObject) Instantiate(GroundIndicatorPrefab);
		if (interaction == InteractInput.Secondary && hitGameObject.gameObject.layer == layerMagnetic) {
			heldObject.PickupAtDistance(transform.position, groundIndicatorInstance, layerHeld, HeldMaterial);
		} else {
			heldObject.PickupAttract(groundIndicatorInstance, layerHeld, HeldMaterial);
		}
	}

	public void ReleaseObject() {
		heldObject.Release();
		Destroy(heldObject.groundIndicator);
		heldObject = null;
	}
}