using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    
    public InputAction MoveAction;
    public float speed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = MoveAction.ReadValue<Vector2>();
        Debug.Log(move);
        Vector2 position = (Vector2)transform.position + move * speed * Time.deltaTime;
        transform.position = position;
    }
}
