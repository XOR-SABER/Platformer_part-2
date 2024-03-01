using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    public float acceleration = 10f;
    public float maxSpeed = 12f;
    public float jumpImpulse = 20f;
    public float jumpBoost = 5f;
    public float jumpDelay = 1f;
    public bool isGrounded = false;
    public bool isHittingBlock = false;
    public UIManager UIman;

    private float jumpCoolDown;
    private Rigidbody rbody;
    private Collider col;
    private Animator player;
    private Transform marioTransform;
    public bool isBlockHit = false;


    void Start() {
        rbody = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        player = GetComponentInChildren<Animator>();

        marioTransform = transform.Find("Mario");
    }
    void Update() {
        float horiz = Input.GetAxis("Horizontal");
        float halfHeight = col.bounds.extents.y + 0.2f;
        Vector3 startPoint = transform.position;
        Vector3 groundEndPoint = startPoint + Vector3.down * halfHeight;
        Vector3 upEndPoint = startPoint + Vector3.up * halfHeight;
        isGrounded = Physics.Raycast(startPoint, Vector3.down, halfHeight);
        Ray ray = new Ray(startPoint, Vector3.up);
        RaycastHit hit;
        if(isGrounded) isBlockHit = false;
        isHittingBlock = Physics.Raycast(ray, out hit, halfHeight);
        if (!isGrounded && isHittingBlock && !isBlockHit)
        {
            string tag = hit.transform.tag;
            switch (tag)
            {
                case "Question":
                    isBlockHit = true;
                    hit.collider.gameObject.GetComponent<QuestionScript>().click();
                    UIman.AddPoints(100);
                    UIman.AddCoin(1);
                    break;
                case "Brick":
                    isBlockHit = true;
                    Destroy(hit.collider.gameObject);
                    UIman.AddPoints(100);
                    break;
                case "Star":
                    isBlockHit = true;
                    Debug.Log("STAR GET!");
                    Destroy(hit.collider.gameObject);
                break;
                default:
                    break;
            }
        }
        if(isGrounded) {
            Ray check = new Ray(startPoint, Vector3.down);
            if(Physics.Raycast(check, out hit, halfHeight)) {
                string tag = hit.transform.tag;
                if(tag == "Water") {
                    Debug.Log("Mario Should be dead fr!");
                }
            }
        }
        
        // Color lineColor = isGrounded ? Color.red : Color.blue;
        // Debug.DrawLine(startPoint, groundEndPoint,lineColor, 0f, false);
        // Debug.DrawLine(startPoint, upEndPoint, lineColor, 0f, false);
        rbody.velocity += Vector3.right * horiz * Time.deltaTime * acceleration;
        
        if(isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            rbody.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
            jumpCoolDown = jumpDelay;
        }
        else if (!isGrounded && Input.GetKey(KeyCode.Space) && jumpCoolDown > 0f ) {
            if(rbody.velocity.y > 0) rbody.AddForce(Vector3.up * jumpBoost, ForceMode.Impulse);
        }

        if(rbody.velocity.magnitude > maxSpeed) {
            rbody.velocity = rbody.velocity.normalized * maxSpeed;
        }
        if(Input.GetKey(KeyCode.LeftShift)) {
            maxSpeed = 15f;
        } else {
            maxSpeed = 8f;
        }
        if(jumpCoolDown > 0f) {
            jumpCoolDown -= Time.deltaTime;
        }
        player.SetFloat("Speed", rbody.velocity.magnitude);
        player.SetBool("Jump", !isGrounded);
        float yaw = (rbody.velocity.x > 0) ? 90 : -90;
        marioTransform.transform.rotation = Quaternion.Euler(0f, yaw, 0f);
    }
}
