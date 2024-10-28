using UnityEngine;

public class UnitDragHandler : MonoBehaviour
{
    private Unit unit;
    private bool isDragging;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) StartDragging();
        else if (Input.GetMouseButtonUp(0)) StopDragging();

        if (isDragging) DragUnit();
    }

    private void StartDragging()
    {
        unit = FindClosestUnit();
        if (unit == null) return;

        GameManager.Instance.cardManager.DropZoneOn();
        unit.animator.SetBool("isDragged", true);
        unit.isDragging = true;
        isDragging = true;
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

    private void DragUnit()
    {
        if (!unit)
        {
            return;
        }
        
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        unit.transform.position = mousePos;
    }


    private Unit FindClosestUnit()
    {
        var range = 1f;
        var targets = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), range);
        var closestDistance = Mathf.Infinity;
        Unit closestUnit = null;
        foreach (var target in targets)
        {
            if (target.TryGetComponent<Unit>(out var unit))
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
