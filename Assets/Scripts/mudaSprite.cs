using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NewBehaviourScript : MonoBehaviour
{
    public Sprite inicial;
    public Sprite final;

    public Image image;

    void Start()
    {
        image = GetComponent<Image>();
        // Set the initial sprite
        image.sprite = inicial;
    }

    // Call this method when Action 1 occurs
    public void PerformAction()
    {
        image.sprite = final;
    }
}
