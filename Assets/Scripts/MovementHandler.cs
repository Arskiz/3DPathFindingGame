using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class MovementHandler : MonoBehaviour
{
    private NavMeshAgent agent;

    public GameObject projectilePrefab; // Ammus
    public Transform projectileSpawnPoint; // Sijainti mistä ammus ammutaan
    public float projectileSpeed = 10f; // Ammuksen nopeus
    public float projectileLifeTime = 1.4f; // Aika, jonka jälkeen ammus tuhoutuu
    private int projectileCount = 0; // Kuinka monta ammusta on käytössä
    public TextMeshProUGUI projectileCounterText; // Teksti missä näytetään panos määrä

    // Start is called before the first frame update
    void Start()
    {
        // Haetaan komponentti
        agent = GetComponent<NavMeshAgent>();
        UpdateProjectileCounterUI(); // Päivitetään alussa
    }

    // Update is called once per frame
    void Update()
    {
        // Jos hiirtä klikataan maahan
        if (Input.GetMouseButtonDown(0))
        {
            // Luodaan ray hiiren sijainnista
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Tarkistetaan osuuko ray johonkin
            if (Physics.Raycast(ray, out hit))
            {
                // Liikutetaan pelaajaa klikattuun kohteen sijaintiin
                agent.SetDestination(hit.point);
            }
        }

        // Ammutaan ammus painamalla space
        if (Input.GetKeyDown(KeyCode.Space) && projectileCount > 0)
        {
            ShootProjectile();
            projectileCount--;
            UpdateProjectileCounterUI(); // Päivitetään kun vähenee
        }
    }

    void ShootProjectile()
    {
        // Luodaan ammus prefabistä
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

        // Lisätään ammukseen liikevoima
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = projectileSpawnPoint.forward * projectileSpeed;
        }

        Destroy(projectile, projectileLifeTime); // Tuhotaan ammus ajan kuluttua
    }

    void UpdateProjectileCounterUI()
    {
        // Päivitetään UI teksti näyttämään nykyinen panos määrä
        if (projectileCounterText != null)
        {
            projectileCounterText.text = "Projectiles: " + projectileCount;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Jos törmätään objectiin tällä tagilla
        if (other.gameObject.CompareTag("Collectable"))
        {
            projectileCount += 5; // Lisätään 5 panosta
            Debug.Log("5 panosta kerätty! Yhteensä: " + projectileCount);

            UpdateProjectileCounterUI(); // Päivitetään kun lisätään

            Destroy(other.gameObject); // Tuhotaan objecti
        }
    }
}