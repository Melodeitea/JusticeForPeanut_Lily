using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControler : MonoBehaviour
{
    public float sensitivityX = 2.0f; // Sensibilité du mouvement horizontal
    public float sensitivityY = 2.0f; // Sensibilité du mouvement vertical
    public float minY = -60.0f; // Limite basse de l'angle vertical
    public float maxY = 60.0f; // Limite haute de l'angle vertical
    public Transform playerBody; // Le corps du joueur pour le mouvement horizontal

    private float rotationX = 0.0f; // Angle de rotation autour de l'axe X (vertical)
    private float rotationY = 0.0f; // Angle de rotation autour de l'axe Y (horizontal)

    public Transform targetPos;
    public Transform cam;

    private Vector2 lookInput; // Valeurs de mouvement de la souris


    // Start is called before the first frame update
    void Start()
    {
        // Verrouiller le curseur au centre de l'écran et le rendre invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }



    // Update is called once per frame
    void Update()
    {
        // Calculer la rotation horizontale
        rotationY += lookInput.x * sensitivityX;

        // Calculer la rotation verticale
        rotationX -= lookInput.y * sensitivityY;
        rotationX = Mathf.Clamp(rotationX, minY, maxY); // Limiter l'angle vertical

        //Update Position
        transform.position = targetPos.position;

        //Update Rotation Y
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotationY, transform.eulerAngles.z);

        //Update Rotation X
        cam.localEulerAngles = new Vector3(rotationX, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
}
