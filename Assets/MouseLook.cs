using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSens = 100f;
    public Transform playerBody;
    float xRotation = 0f;
    bool deathTriggered = false;
    public PauseMenu pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = pauseMenu.isCursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !pauseMenu.isCursorLocked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleCursorLock();
        }

        if (transform.GetComponentInParent<PlayerHealth>().isDead && !deathTriggered)
        {
            deathTriggered = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (deathTriggered) return;

        if (pauseMenu.isCursorLocked)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
        
    }

    void ToggleCursorLock()
    {
        // Toggle cursor lock state based on PauseMenu
        pauseMenu.isCursorLocked = !pauseMenu.isCursorLocked;
        Cursor.lockState = pauseMenu.isCursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !pauseMenu.isCursorLocked;
    }
}
