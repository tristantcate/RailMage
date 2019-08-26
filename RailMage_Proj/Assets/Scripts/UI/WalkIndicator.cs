using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkIndicator : MonoBehaviour
{

    [SerializeField] GameObject headIcon;
    [SerializeField] RectTransform startPos;
    [SerializeField] RectTransform endPos;

    void Start()
    {
    }

    void Update()
    {
        headIcon.transform.position = new Vector2(
            headIcon.transform.position.x, 
            Mathf.Lerp(startPos.position.y + startPos.sizeDelta.y/2, endPos.position.y, GameManager.instance.NormalisedDistanceToEnd())
            );
    }
}
