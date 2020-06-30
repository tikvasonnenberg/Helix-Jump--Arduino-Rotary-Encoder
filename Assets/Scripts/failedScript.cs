using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class failedScript : MonoBehaviour
{
    RectTransform m_rect;
        
    public Transform p;
    private void Start()
    {
        m_rect = GetComponent<RectTransform>();

        m_rect.anchoredPosition = new Vector2(p.localPosition.x, p.localPosition.y);

    }
    public void enableVisual()

    {
        m_rect = GetComponent<RectTransform>();

        m_rect.anchoredPosition = new Vector2(p.localPosition.x, p.localPosition.y);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
