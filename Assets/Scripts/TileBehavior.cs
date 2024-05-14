using Towers;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class TileBehavior : MonoBehaviour
{
    public TowerManager TowerManager;
    public Color PickedColor;
    public Color NonPickedColor;

    public TowerBehavior BuiltTower;

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
        if (BuiltTower == null)
        {
            _spriteRenderer.color = PickedColor;
        }
        else
        {
            BuiltTower.ShowRange();
        }
    }

    private void OnMouseExit()
    {
        _selected = false;
        if (BuiltTower == null)
        {
            _spriteRenderer.color = NonPickedColor;
        }
        else
        {
            BuiltTower.HideRange();
        }
    }

    private void OnMouseDown()
    {
        if (_selected)
        {
            if (BuiltTower != null)
            {
                BuiltTower = TowerManager.UpgradeTower(this);
            }
            else
            {
                BuiltTower = TowerManager.BuildTower(this);
                _spriteRenderer.color = NonPickedColor;
            }
        }
        else
        {
            Debug.Log("Clicked on not selected");
        }
    }
}