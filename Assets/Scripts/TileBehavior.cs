using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class TileBehavior : MonoBehaviour
{
    public Color PickedColor;
    public Color NonPickedColor;

    private SpriteRenderer _spriteRenderer;
    private bool _selected;
    private EventSystem _eventSystem;
    
    private void Awake()
    {
        _eventSystem = FindObjectOfType<EventSystem>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _selected = false;
        _spriteRenderer.color = NonPickedColor;
    }

    private void OnMouseEnter()
    {
        _selected = true;
        _spriteRenderer.color = PickedColor;
    }

    private void OnMouseExit()
    {
        _selected = false;
        _spriteRenderer.color = NonPickedColor;
    }

    private void OnMouseDown()
    {
        if (_selected)
        {
            if (_eventSystem.IsPointerOverGameObject())
            {
                Debug.Log("Clicked over UI");
            }
            else
            {
                Debug.Log("Clicked on selected");
            }
        }
        else
        {
            Debug.Log("Clicked on not selected");
        }
    }
}