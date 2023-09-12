using UnityEngine;

public class UnitDragHandler : MonoBehaviour
{
    private float range = 1f;
    private Unit unit;
    private bool isDragging;

    private void Update()
    {
        transform.position = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            StartDragging();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopDragging();
        }

        if (isDragging)
        {
            MiddleDragging();
        }
    }

    private void StartDragging()
    {
        unit = CheckClosestUnit();

        if (unit == null)
        {
            return;
        }

        GameManager.Instance.cardManager.DropZoneOn();
        unit.animator.SetBool("isDragged", true);
        unit.isDragging = true;
        isDragging = true;
    }

    private void MiddleDragging()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -1f;
        unit.transform.position = mousePos;
    }

    private void StopDragging()
    {
        if (isDragging)
        {
            unit.isDragging = false;
            isDragging = false;
            unit.animator.SetBool("isDragged", false);

            if (unit.isAboveDropPoint)
            {
                GameManager.Instance.cardManager.BackUnitToHand(unit);
                Destroy(unit.gameObject);
            }

            GameManager.Instance.cardManager.DropZoneOff();
        }
    }

    private Unit CheckClosestUnit()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), range);
        float closestDistance = Mathf.Infinity;
        Unit closestUnit = null;
        foreach (Collider2D target in targets)
        {
            if (target.TryGetComponent<Unit>(out Unit unit))
            {
                float distanceToTarget = Vector2.Distance(transform.position, unit.transform.position);
                if (distanceToTarget < closestDistance)
                {
                    closestDistance = distanceToTarget;
                    closestUnit = unit;
                }
            }
        }

        return closestUnit;
    }

}
