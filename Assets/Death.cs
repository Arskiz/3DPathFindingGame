using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    GameObject _player;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<GameObject>();
    }


    private void OnTriggerEnter(Collider other)
    {
        string targetTag = "Respawn";
        if(other.tag == targetTag)
        {
            Die();
        }
    }

    void Die()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
