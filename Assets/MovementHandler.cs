using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    [SerializeField] float speed = 10f; // Pelaajan nopeus
    [SerializeField] Transform cameraTransform; // Kameran transform, m��ritett�v� Unity Editorissa
    private void Start()
    {
        cameraTransform = transform.Find("Main Camera");
    }

    void Update()
    {
        // Haetaan sy�tteet k�ytt�j�lt�
        float horizontal = Input.GetAxis("Horizontal"); // Sivuliike (A/D tai nuolin�pp�imet)
        float vertical = Input.GetAxis("Vertical");     // Liike eteen/taakse (W/S tai nuolin�pp�imet)

        // Lasketaan liikesuunta suhteessa kameran forward- ja right-vektoreihin
        Vector3 forward = cameraTransform.forward; // Kameran suuntaan osoittava vektori
        Vector3 right = cameraTransform.right;     // Kameran oikealle osoittava vektori

        // Nollataan Y-komponentti, jotta liike tapahtuu vain tasossa
        forward.y = 0f;
        right.y = 0f;

        // Normalisoidaan vektorit, jotta nopeus on tasainen
        forward.Normalize();
        right.Normalize();

        // Lasketaan liikkeen suunta
        Vector3 moveDirection = forward * vertical + right * horizontal;

        // Siirret��n pelaajaa
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }
}
