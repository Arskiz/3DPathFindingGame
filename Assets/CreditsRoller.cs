using UnityEngine;

public class CreditsRoller : MonoBehaviour
{
    [SerializeField] GameObject mainObject;
    [SerializeField] GameObject settingsObject;
    [SerializeField] GameObject VisibilityCheckObj;
    [SerializeField] RectTransform creditsRollObject;
    bool rollStarted;

    [SerializeField] Vector2 endRollPos;
    public Vector2 startPos;
    [SerializeField] float RollSpeed;
    // Start is called before the first frame update
    void Start()
    {
        startPos = creditsRollObject.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        rollStarted = creditsRollObject.gameObject.activeSelf;

        if(rollStarted)
        {
            if(creditsRollObject.anchoredPosition.y < endRollPos.y)
            {
                Roll();
            }
            if (creditsRollObject.anchoredPosition.y > endRollPos.y)
            {
                CloseCredits();
                Debug.Log($"Credits Closed. \nRoll object: {creditsRollObject.anchoredPosition.y} & EndRollPos: {endRollPos.y}");
            }
        }

        
    }

    public void CloseCredits()
    {
        creditsRollObject.anchoredPosition = startPos;
        VisibilityCheckObj.SetActive(false);
        settingsObject.SetActive(true);
        mainObject.SetActive(true);
    }

    void Roll()
    {
        creditsRollObject.anchoredPosition += Vector2.up * RollSpeed * Time.deltaTime;
    }
}
