using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerBody; // Pelaajan (kapselin) transform
    public float mouseSensitivity = 100f; // Hiiren herkkyys
    private float xRotation = 0f; // Kameran pystysuuntainen kierto

    void Start()
    {
        // Lukitaan hiiri pelin keskelle ja piilotetaan se
        playerBody = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Haetaan hiiren liike arvot
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // P�ivitet��n kameran pystysuuntainen kierto
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Rajoitetaan kierto, ettei kamera mene ymp�ri

        // Liikutetaan kameraa pystysuunnassa
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Py�ritet��n pelaajaa vaakasuunnassa
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
