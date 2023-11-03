using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosCubeDrawer : MonoBehaviour
{
    [SerializeField] private bool useOriginTranslate = true;
    [Header("Gizmo Settings")]
    [SerializeField] private Vector2 size = Vector2.one;
    [SerializeField] private Vector2 offset = Vector2.zero;
    [Space]
    [SerializeField] private Color color = new Color(255, 255, 255, 50);

    private void OnDrawGizmos()
    {
        if (this.enabled)
        {
            Gizmos.color = color;

            if (useOriginTranslate)
                Gizmos.DrawCube((Vector2)this.transform.position + offset, this.transform.localScale);
            else
                Gizmos.DrawCube((Vector2)this.transform.position + offset, size);
        }
    }
}
