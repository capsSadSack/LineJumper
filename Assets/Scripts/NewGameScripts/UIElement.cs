using UnityEngine;
using UnityEngine.EventSystems;

public class UIElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool MouseOverUI { get; private set; } = false;


    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseOverUI = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseOverUI = false;
    }
}
