using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    GameObject target;
    [SerializeField] Vector3 offset;
    [SerializeField] float fadeDuration;
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.tag != "Player")
        {
            StartCoroutine(FadeOut(other.gameObject));
        }
    }

    private IEnumerator FadeOut(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer == null) yield break;

        Material material = renderer.material;
        SetMaterialTransparent(material);

        Color color = material.color;
        float startAlpha = color.a;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float blend = t / fadeDuration;
            color.a = Mathf.Lerp(startAlpha, 0f, blend);
            material.color = color;
            yield return null;
        }

        // Lopuksi piilota kokonaan
        color.a = 0f;
        material.color = color;
        if(obj != null)
            obj.SetActive(false);
    }

    private void SetMaterialTransparent(Material material)
    {
        material.SetFloat("_Surface", 1); // 1 = Transparent, 0 = Opaque
        material.SetInt("_ZWrite", 0);
        material.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
        material.renderQueue = 3000;
    }

    private void SetMaterialOpaque(Material material)
    {
        material.SetFloat("_Surface", 0); // Opaque
        material.SetInt("_ZWrite", 1);
        material.DisableKeyword("_SURFACE_TYPE_TRANSPARENT");
        material.renderQueue = 2000;
    }
}
