using UnityEngine;

public class InputManager : MonoBehaviour
{
    [HideInInspector]
    public static InputManager Instance;

    public delegate void ClickDelegate();
    public event ClickDelegate ClickEvent;

    delegate void MousePosDelegate();

    private event MousePosDelegate MousePosEvent;

    public bool holding; // TODO: Add hold detection

    [Space(10)]
    
    // Shows us where the last click was or where last hold ended
    public float mouseX;
    public float mouseY;

    private InputManager()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    void Update()
    {
        
        if (ClickEvent != null && Input.GetKeyDown(KeyCode.Mouse0))
        {
            SetMousePosition();
            ClickEvent.Invoke();
        }
        
        if (MousePosEvent != null && Input.GetKeyUp(KeyCode.Mouse0))
        {
            SetMousePosition();
        }
        
    }

    void SetMousePosition()
    {
        mouseX = Input.GetAxisRaw("Horizontal");
        mouseY = Input.GetAxisRaw("Vertical");
    }
    
}
