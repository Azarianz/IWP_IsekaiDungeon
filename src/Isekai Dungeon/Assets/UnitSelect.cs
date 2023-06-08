using AI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace IsekaiDungeon
{
    public class UnitSelect : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Canvas canvas;
        public Transform parentAfterDrag;
        public Transform parent;
        public Flock player_flock;
        public GameObject unitprefab;

        private GameObject previousSpawnTarget;
        private SpriteRenderer previousRenderer;

        public void OnBeginDrag(PointerEventData eventData)
        {
            parentAfterDrag = transform.parent;
            transform.SetParent(parent);
            transform.SetAsLastSibling();

        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;

            if (eventData.pointerEnter.CompareTag("SpawnTarget"))
            {
                var currentSpawnTarget = eventData.pointerEnter.gameObject;
                if (currentSpawnTarget != previousSpawnTarget)
                {
                    ResetPreviousSpawnTarget();
                    previousSpawnTarget = currentSpawnTarget;
                    previousRenderer = currentSpawnTarget.GetComponent<SpriteRenderer>();
                    previousRenderer.color = Color.green; // Change this to your desired hover color
                }
            }   
        }


        public void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.pointerEnter.CompareTag("SpawnTarget"))
            {
                GameObject go = Instantiate(unitprefab, eventData.pointerEnter.gameObject.transform.position, Quaternion.identity);
                go.transform.parent = player_flock.transform;
                Destroy(gameObject);
            }
            else
            {
                transform.SetParent(parentAfterDrag);
            }
        }

        private void ResetPreviousSpawnTarget()
        {
            if (previousRenderer != null)
            {
                previousRenderer.color = Color.white;
                previousSpawnTarget = null;
                previousRenderer = null;
            }
        }
    }
}
