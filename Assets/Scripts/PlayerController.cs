using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Dependencies")]
    public Rigidbody rb;
    //public Animator anims;

    [Header("Movement")]
    public float moveSpeed = 5f;
    private float horizontalInput = 0f;
    private float verticalInput = 0f;

    [Header("State")]
    public float speed = 0;

    // Start is called before the first frame update
    void Start()
    {
        speed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        InputUpdate();
        Movement();
    }

    void Movement() {
        Vector3 velocity = new Vector3(horizontalInput, 0, verticalInput) * speed;;
        rb.velocity = velocity;
    }

    void InputUpdate() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        // anims.SetFloat("horizontal_float", horizontalInput);
        // anims.SetFloat("vertical_float", verticalInput);
    }
}
