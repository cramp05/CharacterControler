using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Raycast_1 : MonoBehaviour
{
    InputAction _clickInput;
    InputAction _positionInput;
    Vector2 _mousePosition;

    void Awake()
    {
        _clickInput = InputSystem.actions["Attack"];
        _positionInput = InputSystem.actions["MousePosition"];
    }

    void Update()
    {     
        
        if(_clickInput.WasPressedThisFrame())
        {
            ShootRaycast();
        }
    }

    void ShootRaycast()
    {
        _mousePosition = _positionInput.ReadValue<Vector2>();

        Ray ray = Camera.main.ScreenPointToRay(_mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if(hit.collider.name == "objeto1")
            {
                SceneManager.LoadScene("Escena 1");
            }

            if(hit.collider.CompareTag("Tag1"))
            {

            }

            if(hit.collider.gameObject.layer == 3)
            {

            }

        }
    }
}
