using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipController : MonoBehaviour
{

    private Rigidbody2D rigidbody;

    public Camera camera;

    public float displacementPower = 10.0f;
    public float maxVelocity = 10.0f;
    public float rotationSpeed = 7.5f;
    public float coef = 1.0f;
    public float coef2 = 1.0f;

    private int floorPlaneMask;

    private Vector3 mousePosition;
    private Vector3 targetLookAt;

    void Awake ()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        floorPlaneMask = LayerMask.GetMask("FloorLayer");
    }

    // Use this for initialization
    void Start ()
    {
        
    }
    
    // Update is called once per frame
    void Update ()
    {
        
    }

    void FixedUpdate ()
    {
        Debug.Log(Input.GetAxis("Horizontal"));
        // movement
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movement = movement.normalized;
        /*if (Input.GetAxis("Horizontal") != 0f)
        {
            rigidbody.AddForce(new Vector2(Input.GetAxis("Horizontal") * displacementPower, 0));
        }
        if (Input.GetAxis("Vertical") != 0f)
        {
            rigidbody.AddForce(new Vector2(0, Input.GetAxis("Vertical") * displacementPower));
        }*/
        /*if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0f)
        {
            movement = movement * displacementPower * Time.deltaTime;
            rigidbody.AddForce(movement);
        }

        Vector2 moveDiff = movement - (rigidbody.velocity * coef);
        rigidbody.AddForce(moveDiff * coef2);*/

        // YELLOW debug draw the current velocity of the rigidbody
        Debug.DrawLine(transform.position,
            (transform.position + new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, 0)),
            Color.yellow);

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0f)
        {
            movement = movement * displacementPower * Time.deltaTime;
            
            // BLUE debug draw the movement selected by the player
            Debug.DrawLine(transform.position,
                (transform.position + new Vector3(movement.x, movement.y, 0)),
                Color.blue);
        }
        
        Vector2 moveDiff = movement - (rigidbody.velocity * coef);

        // RED debug draw the difference between the current velocity and the movement selected
        Debug.DrawLine(transform.position,
            (transform.position + new Vector3(moveDiff.x, moveDiff.y, 0) * coef2),
            Color.red);

        Vector2 contraForce = movement + moveDiff * coef2;
        rigidbody.AddForce(contraForce);

        // GREEN debug draw the final force added to the rigidbody
        Debug.DrawLine(transform.position,
            (transform.position + new Vector3(contraForce.x, contraForce.y, 0)),
            Color.green);





        // velocity clamp
        float actualVelX = rigidbody.velocity.x;
        float actualVelY = rigidbody.velocity.y;
        actualVelX = Mathf.Clamp(actualVelX, -maxVelocity, maxVelocity);
        actualVelY = Mathf.Clamp(actualVelY, -maxVelocity, maxVelocity);

        rigidbody.velocity = new Vector2(actualVelX, actualVelY);

        /*Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        movement = movement.normalized * maxVelocity * Time.deltaTime;

        rigidbody.MovePosition(transform.position + movement);*/

        // rotation
        // mouse raycast to floor plane
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f, floorPlaneMask))
        {
            mousePosition = hit.point;
            targetLookAt = mousePosition - transform.position;
        }
        
        transform.right = Vector3.Lerp(transform.right, targetLookAt, Time.deltaTime * rotationSpeed);

    }

}
