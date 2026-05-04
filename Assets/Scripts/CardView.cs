using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public CardData data;
    public Image image;

    private Canvas cardCanvas;
    private Vector3 originalScale;
    private const float hoverScale = 1.2f;
    void Start()
    {
        cardCanvas = GetComponent<Canvas>();
        cardCanvas.overrideSorting = true;
        originalScale = transform.localScale;
    }

    public void Init(CardData cardData)
    {
        data = cardData;
        image.sprite = data.image;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!GameManager.Instance.isClicable || !GameManager.Instance.canPlay) return;
        cardCanvas.sortingOrder = 100;

        transform.localScale = originalScale * hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!GameManager.Instance.isClicable) return;
        cardCanvas.sortingOrder = 0;
        transform.localScale = originalScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!GameManager.Instance.isClicable || !GameManager.Instance.canPlay) return;
        GameManager.Instance.OnCardSelected(this);
    }
}
