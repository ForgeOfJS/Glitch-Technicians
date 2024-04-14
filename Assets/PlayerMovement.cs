using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.GetComponent<PlayerHealth>().isDead) return;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (x > 0f || z > 0f)
        {
            timer += Time.deltaTime;
            if (timer > .5f)
            {
                int random = Random.Range(0, AudioManager.Instance.metalFootsteps.Length-1);
                transform.GetComponent<AudioSource>().PlayOneShot(AudioManager.Instance.metalFootsteps[random], .5f);
                timer = 0f;
            }
        }

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            int random = Random.Range(0, AudioManager.Instance.jump.Length-1);
            transform.GetComponent<AudioSource>().PlayOneShot(AudioManager.Instance.jump[random], .5f);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
