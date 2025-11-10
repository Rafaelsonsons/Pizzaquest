using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class slotCentral : MonoBehaviour, IDropHandler
{
    
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount <= 6){
            GameObject dropped = eventData.pointerDrag;
            DragDrop draggableItem = dropped.GetComponent<DragDrop>();
            draggableItem.parentAfterDrag = transform;
            draggableItem.transform.position = transform.position;
            draggableItem.isDraggable = false;
            draggableItem.transform.localScale = transform.localScale;
            draggableItem.NewBehaviourScript.PerformAction();
        }
    }
}
