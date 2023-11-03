using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class ButtonTextColorHandler : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private Color _originColor;

    [SerializeField][Tooltip("Reference to the text component.")] private TMP_Text text;
    [Space]
    [Tooltip("Color to set when button gets selected")] public Color color = Color.black;

    private void Awake() => _originColor = text != null ? text.color : Color.white;

    public void OnSelect(BaseEventData eventData) => text.color = color;
    public void OnDeselect(BaseEventData eventData) => text.color = _originColor;
}
