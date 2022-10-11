using UnityEngine;

public class HeldObject {	
    public GameObject gameObject { get; set; }
    public Rigidbody rigidbody { get; set; }
    public float originalDrag { get; set; }
    public MeshRenderer meshRenderer { get; set; }
    public Material originalMaterial { get; set; }
    public int originalLayer { get; set; }
    public bool originalGravity { get; set; }
    public float initialPlayerToObjectDistance { get; set; }
    public float playerToObjectDistance { get; set; }
    public Quaternion originalRotation { get; set; }
    public GameObject groundIndicator { get; set; }

    public HeldObject(GameObject _gameObject) {
        gameObject = _gameObject;
    }

    private void Pickup(GameObject groundIndicatorInstance, int layerHeld, Material heldMaterial) {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        originalLayer = gameObject.layer;
        originalDrag = rigidbody.drag;
        originalGravity = rigidbody.useGravity;
        groundIndicator = groundIndicatorInstance;
        groundIndicator.GetComponent<PlaceAndScaleIndicator>().followGameObject = gameObject;
        rigidbody.useGravity = false;
        rigidbody.drag = 20;
        originalRotation = gameObject.transform.localRotation;
        CopyMaterialProperties(heldMaterial);
        // Changes the physics layer of the object to not let the player 
        // consider it as ground while the object is being held
        gameObject.layer = layerHeld;
    }

    public void PickupAtDistance(Vector3 playerPosition, GameObject groundIndicatorInstance, int layerHeld, Material heldMaterial) {
        Pickup(groundIndicatorInstance, layerHeld, heldMaterial);
        playerToObjectDistance = Vector3.Distance(gameObject.transform.position, playerPosition);
        
    }

    public void PickupAttract(GameObject groundIndicatorInstance, int layerHeld, Material heldMaterial) {
        Pickup(groundIndicatorInstance, layerHeld, heldMaterial);
        Mesh objMesh = gameObject.GetComponent<MeshFilter>().mesh;
        float averageSize = (objMesh.bounds.size.x + objMesh.bounds.size.y + objMesh.bounds.size.z) / 3;
        // Object distance is 2^x + c where x is the x, y and z average
        // bounds of the object and c is just a constant.
        float distance = Mathf.Pow(2, averageSize);
        playerToObjectDistance = distance;
		initialPlayerToObjectDistance = distance;
    }

    public void Release() {
        // Changes the physics layer of the object to let the player 
        // consider it as ground again
        gameObject.layer = originalLayer;
        // Changes object alpha back
        meshRenderer.material = originalMaterial;
        rigidbody.useGravity = originalGravity;
        rigidbody.drag = originalDrag;
        float dropThreshold = 1.0f;
        if (rigidbody.velocity.magnitude < dropThreshold) {
            // Drop object
            // Reset angular velocity only if the 
            // player places an object, as the angular velocity
            // increases the sense of force when the player is 
            // throwing objects.
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
    }

    public void CopyMaterialProperties(Material newMaterial) {
        originalMaterial = meshRenderer.material;
        Color baseColor = meshRenderer.material.GetColor("_BaseColor");
        baseColor.a = 0.75f;
        newMaterial.SetColor("_BaseColor", baseColor);
        newMaterial.SetTexture("_BaseMap", meshRenderer.material.GetTexture("_BaseMap"));
        newMaterial.SetTexture("_BumpMap", meshRenderer.material.GetTexture("_BumpMap"));
        newMaterial.SetFloat("_BumpScale", 1.0f);
        newMaterial.SetFloat("_Smoothness", meshRenderer.material.GetFloat("_Smoothness"));
        newMaterial.SetFloat("_Metallic", meshRenderer.material.GetFloat("_Metallic"));
        meshRenderer.material = newMaterial;
    }
}