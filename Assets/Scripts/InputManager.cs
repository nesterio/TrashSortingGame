using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool clicking;
    public bool holding; // TODO: Add hold detection
    private bool readingHoldOrClick = false;

    // Shows us where the last click was or where last hold ended
    public float mouseX;
    public float mouseY;

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Mouse0))
            SetMousePosition();
    }

    void SetMousePosition()
    {
        mouseX = Input.GetAxisRaw("Horizontal");
        mouseY = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        clicking = Input.GetKey(KeyCode.Mouse0);
    }
}
