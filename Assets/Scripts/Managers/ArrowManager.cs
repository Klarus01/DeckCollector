using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Camera mainCamera;

    private List<GameObject> arrows = new();

    private void Update()
    {
        UpdateArrows();
    }

    private void UpdateArrows()
    {
        foreach (var arrow in arrows)
        {
            Destroy(arrow);
        }
        arrows.Clear();

        var minBounds = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        var maxBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        foreach (var tombstone in TombstoneManager.Instance.tombstones)
        {
            if (tombstone == null) continue;

            var tombstonePos = tombstone.transform.position;

            if (tombstonePos.x < minBounds.x || tombstonePos.x > maxBounds.x ||
                tombstonePos.y < minBounds.y || tombstonePos.y > maxBounds.y)
            {
                CreateArrow(tombstonePos);
            }
        }
    }

    private void CreateArrow(Vector3 tombstonePos)
    {
        var direction = (tombstonePos - mainCamera.transform.position).normalized;

        var arrowPos = mainCamera.transform.position + direction * (mainCamera.orthographicSize * 0.9f);

        var arrow = Instantiate(arrowPrefab, arrowPos, Quaternion.identity);
        arrows.Add(arrow);

        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
}