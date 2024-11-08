using System;
using System.Collections;
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
    private Color originalColor;
    public Slider slider;
    private float restTimer;

    private void Start()
    {
        orginalParent = transform.parent;
        orginalPosition = transform.position;
        originalColor = cardImage.color;

        RestTimeCalculation();
        SetRestSlider();
    }

    private void Update()
    {
        if (!(restTimer > 0f)) return;
        restTimer -= Time.deltaTime;
        slider.value = restTimer;
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
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isAboveSellPoint)
        {
            SellCard();
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

    private void SellCard()
    {
        GameManager.Instance.GoldCount += cardValue;
        GameManager.Instance.ShopCost /= 1.25f;
        GameManager.Instance.deck.SellCard(unit, this);
        Destroy(gameObject);
    }

    private void PlayCard()
    {
        GameManager.Instance.deck.PlayCard(unit, transform, this);
        Destroy(gameObject);
    }

    private void SetRestSlider()
    {
        slider.maxValue = restTimer;
        slider.value = restTimer;
    }

    private void RestTimeCalculation()
    {
        restTimer = (unitMaxHealth - unitHealth) * 0.75f;
        if (unitHealth.Equals(0))
        {
            restTimer *= 1.5f;
        }

        if(!unitMaxHealth.Equals(unitHealth)) FlashGold();
    }

    private void FlashGold()
    {
        CoroutineRunner.StartCoroutine(FlashGoldCoroutine());
    }

    private IEnumerator FlashGoldCoroutine()
    {
        Debug.Log(unit.name + " " + restTimer);
        while (restTimer > 0)
        {
            Debug.Log("running " + unit.name);
            yield return new WaitForEndOfFrame();
        }
        
        Debug.Log(unit.name + " flashing");

        cardImage.color = new Color(1f, 0.92f, 0.016f, 1f);
        yield return new WaitForSeconds(0.5f);
        cardImage.color = originalColor;
    }
}