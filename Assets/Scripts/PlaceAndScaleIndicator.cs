using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceAndScaleIndicator : MonoBehaviour
{
    public LayerMask InteractLayers;
    public GameObject followGameObject { get ; set; }
    private Vector3 groundPosition;
    private Vector3 currentScale;

    private RaycastHit hit;
    private RaycastHit backHit;
    
    void Start() {
        groundPosition = transform.position;
        // Scales the x and z scale of the indicator to be relative to 
        // the object's size.
        Mesh objMesh = followGameObject.GetComponent<MeshFilter>().mesh;
        float averageSize = (objMesh.bounds.size.x + objMesh.bounds.size.y + objMesh.bounds.size.z) / 3;
        currentScale = transform.localScale;
        currentScale.x = averageSize * 0.75f;
        currentScale.z = averageSize * 0.75f;
    }

    void FixedUpdate() {
        // Raycasts in the down direction to find the nearest ground.
        Physics.Raycast(followGameObject.transform.position, -transform.up, out hit, InteractLayers);
    }

    void LateUpdate() {
        // Updates the ground position again in the LateUpdate 
        // function to get the last updated followGameObject position
        // and make movement smooth.
        groundPosition.x = followGameObject.transform.position.x;
        groundPosition.y = hit.point.y;
        groundPosition.z = followGameObject.transform.position.z;
        transform.position = groundPosition;
        currentScale.y = hit.distance;
        transform.localScale = currentScale;
    }
}
