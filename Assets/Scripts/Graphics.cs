using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graphics : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int maxFPS = 60;
    void Update()
    {
        Application.targetFrameRate = maxFPS;
    }

   
}
