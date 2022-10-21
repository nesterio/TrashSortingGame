using UnityEngine;

public class InputManager : MonoBehaviour
{
    [HideInInspector]
    public static InputManager Instance;

    [SerializeField]private Camera MainCamera;

    public delegate void ClickDelegate();
    public event ClickDelegate ClickEvent;

    public delegate void MousePosDelegate(Vector3 vector3);

    public event MousePosDelegate MousePosEvent;

    public delegate void ReleaseDelegate();

    public event ReleaseDelegate ReleaseEvent;

    public bool holding; // TODO: Add hold detection

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
            GetMousePosition();
            ClickEvent.Invoke();
        }

        if (ReleaseEvent != null && Input.GetKeyUp(KeyCode.Mouse0))
        {
            ReleaseEvent?.Invoke();
            
            
        }
        
        if (MousePosEvent != null)
        {
            MousePosEvent?.Invoke(GetMousePosition());
        }
        
    }

    Vector3 GetMousePosition()
    {
        var vector = new Vector3
            (Input.mousePosition.x,
                Input.mousePosition.y,
                MainCamera.transform.position.z);
        
        vector = MainCamera.ScreenToWorldPoint(vector);

        Debug.Log(vector.x + " " + vector.y);
        
        vector = new Vector3(
            vector.x,
            vector.y,
            vector.z);

        return vector;
    }
    
}
