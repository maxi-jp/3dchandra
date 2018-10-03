using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipControllerTargetVer : MonoBehaviour
{

    public Camera camera;

    private Vector2 targetMovement;
    private Vector2 lastTargetMovement;

    public float velocity = 10.0f;
    public float rotationSpeed = 7.5f;
    public float displacementCoef = 1.0f;

    private int floorPlaneMask;

    private Vector3 mousePosition;
    private Vector3 targetLookAt;

    void Start ()
    {
        targetMovement = lastTargetMovement = transform.position;

        floorPlaneMask = LayerMask.GetMask("FloorLayer");
    }
    
    void Update ()
    {
        // update targetMovement
        if (Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f)
        {
            Vector2 currentMove = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            currentMove = currentMove.normalized * displacementCoef;

            /*targetMovement = new Vector2
            (
                currentMove.x + transform.position.x,
                currentMove.y + transform.position.y
            );*/
            targetMovement = lastTargetMovement + currentMove;
        }

        Debug.Log("transform: " + transform.position + "; targetMovement: " + targetMovement);
        transform.position = Vector2.Lerp
        (
            transform.position,
            targetMovement,
            (velocity / displacementCoef) * Time.deltaTime
        );

        lastTargetMovement = targetMovement;

        Debug.DrawLine
        (
            transform.position,
            new Vector3(targetMovement.x, targetMovement.y, transform.position.z),
            Color.blue
        );

        // rotation
        // mouse raycast to floor plane
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f, floorPlaneMask))
        {
            mousePosition = hit.point;
            targetLookAt = mousePosition - transform.position;
        }

        transform.right = Vector3.Lerp
        (
            transform.right,
            targetLookAt,
            Time.deltaTime * rotationSpeed
        );

    }

}
