using AI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace IsekaiDungeon
{
    public class UnitSelect : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Canvas canvas;
        public Transform parent;
        public GameObject unitprefab;
        public Agent_Data unit_data;

        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        
        //TextMeshProUGUI

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            canvas = GetComponentInParent<Canvas>();
            parent = transform.parent;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // Optional: Implement any additional logic when the object is clicked
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            transform.parent = canvas.transform;
            // Start dragging by disabling interaction and setting the drag image
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0.6f;
            gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 1);
        }

        public void OnDrag(PointerEventData eventData)
        {
            // Update the position of the dragged object based on the input
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // Optional: Implement any logic when the drag ends (e.g., snapping, drop detection)
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
            transform.parent = parent;
            gameObject.transform.localScale = Vector3.one;
        }

    }
}
