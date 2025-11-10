using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public Image image;
    public bool isDraggable = true;
    [HideInInspector] public Transform parentAfterDrag;
    public Item item;
    public NewBehaviourScript NewBehaviourScript;

    private void Start()
    {
        image = GetComponent<Image>();
        item = new Item();
        NewBehaviourScript = GetComponent<NewBehaviourScript>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (isDraggable)
        {
            Debug.Log("OnBeginDrag");
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            image.raycastTarget = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDraggable)
        {
            Debug.Log("OnDrag");
            transform.position = Input.mousePosition;
        }
   }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDraggable)
        {
            Debug.Log("OnEndDrag");
            image.raycastTarget = true;
        }
        transform.SetParent(parentAfterDrag);
    }

}
