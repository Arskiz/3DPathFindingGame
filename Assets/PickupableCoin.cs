using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableCoin : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        switch (col.tag)
        {
            default:
                return;
            case "Player":
                OnPlayerHit();
                break;
        }
    }

    void OnPlayerHit()
    {
        CoinHandler coinHandler = FindAnyObjectByType<CoinHandler>();
        coinHandler.CollectCoin();
        Destroy(gameObject);
    }
}
