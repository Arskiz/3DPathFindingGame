using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    GameObject target;
    [SerializeField] Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Look at player constantly
        GetComponent<Transform>().LookAt(target.transform);
        float lerpedY = Mathf.Lerp(transform.position.y, target.transform.position.y + 2, 0.01f);
        transform.position = new Vector3(transform.position.x, lerpedY, target.transform.position.z - offset.z);
    }
}
