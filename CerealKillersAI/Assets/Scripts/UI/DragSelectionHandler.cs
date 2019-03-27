using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragSelectionHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler {

    [SerializeField]
    Image selectionBoxImage;

    Vector2 startPosition;
    Rect selectionRect;

    public void OnBeginDrag(PointerEventData eventData) {
        if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl)) {
            Unit.DeselectAll(new BaseEventData(EventSystem.current));
        }
        selectionBoxImage.gameObject.SetActive(true);
        startPosition = eventData.position;
        selectionRect = new Rect();
    }

    public void OnDrag(PointerEventData eventData) {
        if (eventData.position.x < startPosition.x) {
            selectionRect.xMin = eventData.position.x;
            selectionRect.xMax = startPosition.x;
        } else {
            selectionRect.xMax = eventData.position.x;
            selectionRect.xMin = startPosition.x;
        }

        if (eventData.position.y < startPosition.y) {
            selectionRect.yMin = eventData.position.y;
            selectionRect.yMax = startPosition.y;
        } else {
            selectionRect.yMax = eventData.position.y;
            selectionRect.yMin = startPosition.y;
        }

        selectionBoxImage.rectTransform.offsetMin = selectionRect.min;
        selectionBoxImage.rectTransform.offsetMax = selectionRect.max;
    }


    public void OnEndDrag(PointerEventData eventData) {
        selectionBoxImage.gameObject.SetActive(false);
        foreach (Unit unit in Unit.allMySelectabkes) {
            if (selectionRect.Contains(Camera.main.WorldToScreenPoint(unit.transform.position))) {
                unit.OnSelect(eventData);
            }
        }
        Unit.GenerateLeader();
    }

    public void OnPointerClick(PointerEventData eventData) {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        float myDistance = 0;

        foreach (RaycastResult result in results) {
            if(result.gameObject == gameObject) {
                myDistance = result.distance;
                break;
            }
        }

        GameObject nextObject = null;
        float maxDistance = Mathf.Infinity;
        foreach (RaycastResult result in results) {
            if(result.distance > myDistance && result.distance < maxDistance) {
                nextObject = result.gameObject;
                maxDistance = result.distance;
            }
        }
        if (nextObject) {
            ExecuteEvents.Execute<IPointerClickHandler>(nextObject, eventData, (x, y) => { x.OnPointerClick((PointerEventData)y); });
        }
    }
}
