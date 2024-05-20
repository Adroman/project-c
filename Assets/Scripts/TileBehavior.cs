using Towers;
using Ui;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(CircleRenderer))]
public class TileBehavior : MonoBehaviour
{
    public TowerManager TowerManager;
    public Color PickedColor;
    public Color NonPickedColor;

    public TowerBehavior BuiltTower;
    public UiTowerInfo TowerInfo;
    
    private SpriteRenderer _spriteRenderer;
    private CircleRenderer _circleRenderer;
    private bool _selected;
    private EventSystem _eventSystem;
    
    private void Awake()
    {
        _eventSystem = FindObjectOfType<EventSystem>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _circleRenderer = GetComponent<CircleRenderer>();
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
            if (TowerManager.SelectedTower != null)
            {
                _circleRenderer.CalculateCircle(TowerManager.SelectedTower.PreviewRange);
                _circleRenderer.ShowCircle();
                _spriteRenderer.color = PickedColor;
            }
        }
        else
        {
            BuiltTower.ShowRange();
            TowerInfo.Show(BuiltTower);
        }
    }

    private void OnMouseExit()
    {
        _selected = false;
        if (BuiltTower == null)
        {
            _spriteRenderer.color = NonPickedColor;
            _circleRenderer.HideCircle();
        }
        else
        {
            BuiltTower.HideRange();
            TowerInfo.Hide();
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
                _circleRenderer.HideCircle();
            }
        }
        else
        {
            Debug.Log("Clicked on not selected");
        }
    }
}