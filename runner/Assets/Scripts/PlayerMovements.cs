using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovements : MonoBehaviour
{
    [SerializeField] Rigidbody rgbody;
    [SerializeField] Camera cam;
    [SerializeField, Range(1f, 5f)] private float speed = 5f;
 
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public bool isGrounded;

    private float horizontal;
    private float vertical;
    private void Update()
    {
        GetInput();
        FixedUpdate();
    }

    private void Start() {
        jump = new Vector3(0.0f,2.0f,0.0f);
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    void FixedUpdate()
    {
        Move();    
    }

    private void GetInput()
    {
        horizontal = Input.GetAxis("Horizontal");
       // vertical = Input.GetAxis("Vertical");
    }

    private void Move()
    {
        Vector3 changeInPosition = new Vector3(horizontal, 0f,0f);
        Vector3 goToPositon = transform.position + changeInPosition * speed * Time.deltaTime;
        rgbody.MovePosition(goToPositon);
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) ) && isGrounded)
        {
            rgbody.AddForce(jump*jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }
}
