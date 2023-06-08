using UnityEngine;
using UnityEngine.EventSystems;

namespace IsekaiDungeon
{
    public class SpawnGrid : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            GameObject dropped = eventData.pointerDrag;
            UnitSelect item = dropped.GetComponent<UnitSelect>();
            Instantiate(item.unitprefab, transform.position, Quaternion.identity);
            Destroy(dropped);
        }
    }
}
