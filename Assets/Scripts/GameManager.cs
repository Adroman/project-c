using System.Collections.Generic;
using Towers;
using Ui;
using UnityEngine;
using Variables;

public class GameManager : MonoBehaviour
{
    public IntVariable Gold;
    public int GoldInitialValue;
    
    public IntVariable Health;
    public int HealthInitialValue;
    
    public IntVariable Wave;
    public int WaveInitialValue
        ;
    public IntVariable Skills;
    public int SkillsInitialValue;
    
    public IntVariable Mana;
    public int ManaInitialValue;
    
    public IntVariable Difficulty;
    public int DifficultyInitialValue;

    public IntVariable EnemiesOnField;
    public IntVariable EnemiesWaiting;

    public List<BoolVariable> VariablesToInitialize;
    
    public GameObject TowerUi;
    public GameObject CardShopUi;
    public GameObject CardHandUi;
    public TowerManager TowerManager;
    public UiSelectedTower UiSelectedTower;

    public GameObject InGameUi;
    public GameObject GameOverUi;
    public GameObject Tiles;
    
    private void OnEnable()
    {
        Gold.Value = GoldInitialValue;
        Health.Value = HealthInitialValue;
        Wave.Value = WaveInitialValue;
        Skills.Value = SkillsInitialValue;
        Mana.Value = ManaInitialValue;
        Difficulty.Value = DifficultyInitialValue;

        foreach (var boolVariable in VariablesToInitialize)
        {
            boolVariable.Disable();
        }
    }

    public void LoseLife()
    {
        Health.SubtractValue(1);
        if (Health.Value == 0)
        {
            Debug.Log("Game over");
            GameOver();
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        InGameUi.SetActive(false);
        GameOverUi.SetActive(true);
        Tiles.SetActive(false);
    }

    public void CheckActiveEnemies()
    {
        EnemiesOnField.SubtractValue(1);
        if (EnemiesOnField.Value == 0 && EnemiesWaiting.Value == 0)
        {
            TowerUi.SetActive(false);
            CardShopUi.SetActive(true);
            CardHandUi.SetActive(true);
            TowerManager.SelectedTower = null;
            UiSelectedTower.SelectNewTower(null);
        }
    }
}