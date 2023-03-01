using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SnakeMovement : MonoBehaviourPun
{
    public float moveSpeed = 1.0f;
    public float rotSpeed = 0.05f;

    private Vector3 moveDirection = Vector3.zero;
    private Collider platformCollider;
    private float xBound;
    private float zBound;

    private Vector2 startPosition;

    private void Awake()
    {
        // Get the collider of the platform object
        platformCollider = GameObject.FindWithTag("Platform").GetComponent<Collider>();

        // Calculate the x and z bounds of the platform based on its size
        xBound = platformCollider.bounds.size.x / 2;
        zBound = platformCollider.bounds.size.z / 2;
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
            // Check for touch input
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    // Record the start position of the touch
                    startPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    // Calculate the movement direction based on the difference between the start and end positions
                    Vector2 delta = touch.position - startPosition;
                    if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                    {
                        // Move left or right
                        //moveDirection = delta.x > 0 ? Vector3.right : Vector3.left;
                        if(delta.x >  0)
                        {
                            if (moveDirection != Vector3.left)
                            {
                                // Swipe right
                                moveDirection = Vector3.right;
                            }
                        }
                        else
                        {
                            if (moveDirection != Vector3.right)
                            {
                                // Swipe left
                                moveDirection = Vector3.left;
                            }
                        }
                    }
                    else
                    {
                        // Move up or down
                        moveDirection = delta.y > 0 ? Vector3.forward : Vector3.back;
                        if (delta.x > 0)
                        {
                            if (moveDirection != Vector3.back)
                            {
                                // Swipe right
                                moveDirection = Vector3.forward;
                            }
                        }
                        else
                        {
                            if (moveDirection != Vector3.forward)
                            {
                                // Swipe left
                                moveDirection = Vector3.back;
                            }
                        }
                    }
                }
            }
            else
            {
                // Check for keyboard input as a fallback
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (moveDirection != Vector3.right)
                    {
                        // Swipe left
                        moveDirection = Vector3.left;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (moveDirection != Vector3.left)
                    {
                        // Swipe right
                        moveDirection = Vector3.right;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (moveDirection != Vector3.back)
                    {
                        // Swipe up
                        moveDirection = Vector3.forward;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (moveDirection != Vector3.forward)
                    {
                        // Swipe down
                        moveDirection = Vector3.back;
                    }
                }
            }
            if (moveDirection == Vector3.zero)
            {
                transform.position += Vector3.forward * (moveSpeed / 2) * Time.deltaTime;

                // Restrict the snake's movement within the platform bounds
                float clampedX = Mathf.Clamp(transform.position.x, -xBound, xBound);
                float clampedZ = Mathf.Clamp(transform.position.z, -zBound, zBound);
                transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
                //moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")).normalized;
            }

            if (moveDirection != Vector3.zero)
            {
                // Move the snake in the current direction
                transform.position += moveDirection * moveSpeed * Time.deltaTime;

                // Restrict the snake's movement within the platform bounds
                float clampedX = Mathf.Clamp(transform.position.x, -xBound, xBound);
                float clampedZ = Mathf.Clamp(transform.position.z, -zBound, zBound);
                transform.position = new Vector3(clampedX, transform.position.y, clampedZ);

                // Rotate the snake in the direction of movement
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), rotSpeed);
            }
                
        }
    }
}