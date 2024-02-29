using System.Collections;
using System.Collections.Generic;
using JetBrains.Rider.Unity.Editor;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    public float acceleration = 10f;
    public float maxSpeed = 12f;
    public float jumpImpulse = 20f;
    public bool isGrounded = false;

    private Rigidbody rbody;
    private Collider col;

    void Start() {
        rbody = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }
    void FixedUpdate() {
        Vector3 startPoint = transform.position;
        col = GetComponent<Collider>();
        float halfHeight = col.bounds.extents.y;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, halfHeight);
        float horiz = Input.GetAxis("Horizontal");
        
        rbody.velocity += Vector3.right * horiz * Time.deltaTime * acceleration;
        
        // Vector3 endPoint = startPoint + Vector3.down *2f;

        
        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            rbody.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
        }
        if (rbody.velocity.x > maxSpeed) {
            Vector3 newVel = rbody.velocity;
            newVel.x = maxSpeed;
            rbody.velocity = newVel;
        }

        
    }
}
