using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteGround : MonoBehaviour
{
    [SerializeField] GameObject[] groundPrefabs; // Array, joka sis‰lt‰‰ kaikki erilaiset maapalat (5 erilaista)
    [SerializeField] List<float> groundLengths = new List<float>();
    [SerializeField] float moveSpeed = 5f; // Maan liikkumisnopeus

    [HideInInspector]public GameObject[] groundPieces; // Kaksi maapalaa, jotka vuorottelevat

    // Start is called before the first frame update
    void Start()
    {
        // Alustetaan kaksi maapalaa
        groundPieces = new GameObject[2];
        for (int i = 0; i < 2; i++)
        {
            int randomIndex = Random.Range(0, groundPrefabs.Length); // Valitaan satunnainen maapala
            groundPieces[i] = Instantiate(groundPrefabs[randomIndex], new Vector3(0, 0, i * groundLengths[i]), Quaternion.identity);
            CoinHandler cH = FindAnyObjectByType<CoinHandler>();
            cH.SpawnCoins(groundPieces[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Liikutetaan kumpaakin maapalaa eteenp‰in
        for (int i = 0; i < groundPieces.Length; i++)
        {
            groundPieces[i].transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

            // Kun maapala on kulkenut kokonaan ohi, siirret‰‰n se takaisin eteen ja vaihdetaan satunnaisesti
            if (groundPieces[i].transform.position.z < -groundLengths[i])
            {
                Destroy(groundPieces[i]); // Poistetaan vanha maapala

                int randomIndex = Random.Range(0, groundPrefabs.Length); // Satunnainen maapala
                float newZ = groundPieces[i == 0 ? 1 : 0].transform.position.z + groundLengths[i];
                groundPieces[i] = Instantiate(groundPrefabs[randomIndex], new Vector3(0, 0, newZ), Quaternion.identity);
                CoinHandler cH = FindAnyObjectByType<CoinHandler>();
                cH.SpawnCoins(groundPieces[i]);
            }
        }
    }
}
