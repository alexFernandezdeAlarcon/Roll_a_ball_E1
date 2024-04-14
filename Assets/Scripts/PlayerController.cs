using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        int remaining = 15 - count; // Calcula la cantidad restante
        countText.text += "\nRemaining: " + remaining.ToString();
        if (count >= 15)
        {
            winTextObject.SetActive(true);
        }
    }
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement* speed);

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("Holes"))
        {
       
            Collider groundCollider = GetComponent<Collider>(); // Obtener el Collider del suelo
            groundCollider.enabled = false; // Desactivar el colisionador del suelo
            Invoke("RepositionBall", 1.5f);
            Invoke("ActivateGroundCollider", 1.0f);

        }
        
    }
    void RepositionBall()
    {
        rb.velocity = Vector3.zero; // Detenemos cualquier movimiento que tenga la bola
        rb.angularVelocity = Vector3.zero; // Detenemos cualquier rotación que tenga la bola
        transform.position = Vector3.zero; // Reposicionamos la bola en la posición inicial (0,0,0)
    }
    void ActivateGroundCollider()
    {
        Collider groundCollider = GetComponent<Collider>(); // Obtener el Collider del suelo
        groundCollider.enabled = true; // Volver a activar el colisionador del suelo
    }
}
