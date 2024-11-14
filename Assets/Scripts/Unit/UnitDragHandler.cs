using UnityEngine;
using UnityEngine.EventSystems;

public class UnitDragHandler : MonoBehaviour
{
    private Unit unit;
    private Unit highlightedUnit;
    private bool isDragging;
    private float highlightRange = 0.3f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) StartDragging();
        else if (Input.GetMouseButtonUp(0)) StopDragging();

        if (Input.GetMouseButtonDown(1)) ReturnUnitToHand();
        
        if (isDragging) DragUnit();
        else HighlightUnitUnderCursor();
    }

    private void StartDragging()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        
        unit = FindClosestUnit();
        if (unit == null) return;
        if (!unit.animator) return;
        
        unit.animator.SetBool("isDragged", true);
        unit.isDragging = true;
        isDragging = true;
    }

    private void StopDragging()
    {
        if (!isDragging) return;
        
        unit.isDragging = false;
        isDragging = false;

        if (!unit.animator) return;
        unit.animator.SetBool("isDragged", false);
            
        if (unit.isAboveDropPoint)
        {
            CardManager.Instance.BackUnitToHand(unit);
            Destroy(unit.gameObject);
        }
    }

    private void DragUnit()
    {
        if (!unit) return;
        
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        unit.transform.position = mousePos;
    }

    private void HighlightUnitUnderCursor()
    {
        var closestUnit = FindClosestUnit();

        if (highlightedUnit != closestUnit)
        {
            if (highlightedUnit != null) highlightedUnit.SetHighlight(false);
            if (closestUnit != null) closestUnit.SetHighlight(true);
            
            highlightedUnit = closestUnit;
        }
    }

    private Unit FindClosestUnit()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var targets = Physics2D.OverlapCircleAll(mousePos, highlightRange);
        var closestDistance = Mathf.Infinity;
        Unit closestUnit = null;

        foreach (var target in targets)
        {
            if (target.TryGetComponent<Unit>(out var unit))
            {
                var distanceToTarget = Vector2.Distance(mousePos, unit.transform.position);
                if (distanceToTarget < closestDistance)
                {
                    closestDistance = distanceToTarget;
                    closestUnit = unit;
                }
            }
        }

        return closestUnit;
    }
    
    private void ReturnUnitToHand()
    {
        unit = FindClosestUnit();
        if (isDragging || unit == null) return;
        CardManager.Instance.BackUnitToHand(unit);
        isDragging = false;
        Destroy(unit.gameObject);
    }
}
