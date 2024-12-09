using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinHandler : MonoBehaviour
{
    int collectedCoins;
    InfiniteGround groundScript;
    [SerializeField] GameObject coinPrefab;

    // Start is called before the first frame update
    void Start()
    {
        groundScript = FindAnyObjectByType<InfiniteGround>();
    }

    void Update()
    {
        GameObject.Find("coinsCollected").GetComponent<TextMeshProUGUI>().text = "Coins Collected: " + collectedCoins.ToString();
    }

    public void SpawnCoins(GameObject ground)
    {
        List<Transform> coinSpawnPoints = new List<Transform>();
        Transform groundObj = ground.transform;
        for(int i = 0; i < groundObj.childCount; i++)
        {
            string prefix = "spawnpoint";
            string name = groundObj.GetChild(i).name;
            if (name.Contains(prefix))
                coinSpawnPoints.Add(groundObj.GetChild(i));
            else
                continue;
        }

        if(coinSpawnPoints.Count > 0)
        {
            foreach(Transform coinSpawnPoint in coinSpawnPoints)
            {
                GameObject coin = Instantiate(coinPrefab, coinSpawnPoint.position, Quaternion.identity, coinSpawnPoint.parent);
                coin.name = $"Coin [{coinSpawnPoints.IndexOf(coinSpawnPoint)}]";
                coin.transform.localScale = new Vector3(0.06780764f, 0.3828329f, 0.007151673f);            
            }
        }
    }

    public void CollectCoin()
    {
        collectedCoins += 1;
    }
}
