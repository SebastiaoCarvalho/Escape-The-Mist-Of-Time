using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameData GameData;
    [SerializeField] private GameObject MrCapetaPrefab;
    [SerializeField] private GameObject SpikeyPrefab;

    void Awake()
    {
        GameData.DebugData();
        //In a "real game" we would want GameData to be persistent, but for testing, we need to reset it
        if (GameData.IsClean)
        {
            InitializeGameData();
        }
        Debug.Log(GameData.Player.position);
    }

    private void InitializeGameData() {
        GameData.ToDoTasks = new List<Task>(){
            
        };
        GameData.InProgressTasks = new List<Task>() {
            new Task("Task B", "Kill 'Mr Capeta'", "InProgressRegion", new List<Task>() { new Task("Task E", "Kill 'Mr Pala Pala'", "ToDoRegion") } )
        };
        GameData.CompletedTasks = new List<Task>() {
            
        };

        var head = new Upgrade("Speed boost", "Boost your speed", 1, () => { 
            GameData.Player = new PlayerData {
                hp = GameData.Player.hp,
                skillPoints = GameData.Player.skillPoints,
                position = GameData.Player.position,
                speed = GameData.Player.speed * 1.5f
            };
        });
        GameData.Upgrades = new List<Upgrade>()
        {
            head,
            new Upgrade("Mind kill", "Boost your psychic powers", 2, () => {}, head),
            new Upgrade("Flying", "Spread your wings", 2, () => {}, head)
        };

        GameData.Items= new List<Item>()
        {
            new Item("Stone", "This is a stone"),
            new Item("Stone", "This is a stone"),
            new Item("Stone", "This is a stone"),
        };
        GameData.Enemies = new List<EnemyData>();
        EnemyData enemy1 = new EnemyData
        {
            prefab = MrCapetaPrefab,
            position = new Vector3(130, 6.5f, 444),
            hp = 30,
            givesUpgrade = true,
            observers = new List<IObserver>() {GameData.InProgressTasks[0]}
        };
        GameData.Enemies.Add(enemy1);
        EnemyData enemy2 = new EnemyData
        {
            prefab = SpikeyPrefab,
            position = new Vector3(84, -8.1f, 425),
            hp = 10,
            givesUpgrade = false
        };
        GameData.Enemies.Add(enemy2);
        EnemyData enemy3 = new EnemyData
        {
            prefab = SpikeyPrefab, // this  won't be active so prefab is irrelevant
            position = new Vector3(123, 6.5f, 490),
            hp = 50,
            givesUpgrade = true
        };
        GameData.Enemies.Add(enemy3);
        GameData.Player = new PlayerData
        {
            position = new Vector3(25, -6.97f, 391),
            hp = 100,
            speed = 7,
            skillPoints = 0
        };
        GameData.Resources = new List<Vector3>()
        {
            new Vector3(70, -7.5f, 400)
        };
        GameData.RespawnPoints = new List<Vector3>()
        {
            new Vector3(30, -7, 390),
            new Vector3(80, -7, 434),
            new Vector3(140, -7, 448),
            new Vector3(120, 6.5f, 490)
        };
        
    }

    public void Clean() {
        GameData.CleanData();
    }

}
