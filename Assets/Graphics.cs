using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graphics : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int maxFPS = 60;
    void Start()
    {
        Application.targetFrameRate = maxFPS;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
