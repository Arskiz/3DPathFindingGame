using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    private Rigidbody rb;
    private bool isGrounded;
    public int jumpsRemaining = 5;
    float timeLasted = 0.0f;

    public float rayCastDistance = 1.2f; // Raycast et‰isyys maan havaitsemiseen

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        GroundCheck();
        Jump();
        TimeLasted();
    }

    void TimeLasted()
    {
        timeLasted += Time.deltaTime;
        TextMeshProUGUI timeLastedText;
        timeLastedText = GameObject.Find("timeLasted").GetComponent<TextMeshProUGUI>();
        timeLastedText.text = $"Time Lasted: {timeLasted.ToString("F1")}";
    }

    void Move()
    {
        float move = Input.GetAxis("Horizontal") * moveSpeed;
        Vector3 movement = new Vector3(move, rb.velocity.y, 0);
        rb.velocity = movement;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            if (jumpsRemaining > 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Nollaa pystysuuntaisen liikkeen ennen hyppy‰
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false; // Estet‰‰n useampi hyppy kerralla
                jumpsRemaining -= 1;
            }
        }
        TextMeshProUGUI jumpRemainingText;
        jumpRemainingText = GameObject.Find("jumpsremaining").GetComponent<TextMeshProUGUI>();
        jumpRemainingText.text = $"Jumps Remaining: {jumpsRemaining}";
    }

    void GroundCheck()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, rayCastDistance))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }
        else
        {
            isGrounded = false;
        }
    }
}
