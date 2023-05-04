using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PublishButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Sprite defaultSprite;
    public Sprite hoverSprite;
    public Sprite pressedSprite;

    private Image buttonImage;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.sprite = defaultSprite;
    }

    public void OnPointerEnter(PointerEventData eventData = null)
    {
        buttonImage.sprite = hoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData = null)
    {
        buttonImage.sprite = defaultSprite;
    }

    public void OnPointerDown(PointerEventData eventData = null)
    {
        buttonImage.sprite = pressedSprite;
    }

    public void OnPointerUp(PointerEventData eventData = null)
    {
        buttonImage.sprite = hoverSprite;
    }
}
