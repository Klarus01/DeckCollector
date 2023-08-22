using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Image cardImage;
    public Unit unit;
    private int cardValue = 2;
    public float unitHealth;
    public float unitMaxHealth;
    public bool isAboveSellPoint;
    public bool isAboveDropPoint;
    public Transform orginalParent;
    public Vector3 orginalPosition;
    public Slider slider;
    private float restTimer = 5f;

    private void Start()
    {
        orginalParent = transform.parent;
        orginalPosition = transform.position;
        ResetTimeCalculation();
        SetRestSlider();
    }

    private void Update()
    {
        if (restTimer > 0f)
        {
            restTimer -= Time.deltaTime;
            slider.value = restTimer;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (restTimer > 0f)
        {
            return;
        }

        transform.SetParent(GameManager.Instance.cardManager.cardInUse);
        GameManager.Instance.cardManager.DropZoneOn();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (restTimer > 0f)
        {
            return;
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (restTimer > 0f)
        {
            return;
        }

        if (isAboveSellPoint)
        {
            GameManager.Instance.GoldCount += cardValue;
            GameManager.Instance.deck.SellCard(unit);
            Destroy(gameObject);
        }
        else if (isAboveDropPoint)
        {
            transform.position = orginalPosition;
            transform.SetParent(orginalParent);
        }
        else
        {
            PlayCard();
        }

        GameManager.Instance.cardManager.DropZoneOff();
    }

    private void PlayCard()
    {
        GameManager.Instance.deck.PlayCard(unit, transform);
        Destroy(gameObject);
    }

    public void SetRestSlider()
    {
        slider.maxValue = restTimer;
        slider.value = restTimer;
    }

    private void ResetTimeCalculation()
    {
        restTimer = (unitMaxHealth - unitHealth);
        if (unitHealth.Equals(0))
        {
            restTimer *= 2f;
        }
    }
}