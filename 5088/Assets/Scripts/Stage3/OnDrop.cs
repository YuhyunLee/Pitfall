using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnDrop : MonoBehaviour, IDropHandler
{
    public bool isDrop = false;

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        isDrop = true;
    }
}
