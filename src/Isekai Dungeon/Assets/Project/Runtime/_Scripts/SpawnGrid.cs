using AI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace IsekaiDungeon
{
    public class SpawnGrid : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
    {
        private Image sprite;

        private void Start()
        {
            sprite = GetComponent<Image>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(eventData.dragging)
            {
                sprite.color = Color.green;
                Debug.Log("OnPointerEnter SpawnGrid");
            }

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.dragging)
            {
                sprite.color = Color.white;
                Debug.Log("OnPointerExit SpawnGrid");
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("OnDropped SpawnGrid");
            GameObject dropped = eventData.pointerDrag;
            UnitSelect item = dropped.GetComponent<UnitSelect>();

            GameObject go = Instantiate(item.unitprefab, GameManager.Instance.playerTeam.transform);
            
            Agent_AI agentAI = go.GetComponent<Agent_AI>();

            agentAI.SetUnitData(item.unit_data);

            go.transform.position = transform.position;
            Destroy(dropped);
            gameObject.SetActive(false);

        }
    }
}
