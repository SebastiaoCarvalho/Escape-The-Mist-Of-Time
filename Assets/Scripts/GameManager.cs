using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI slowText;
    private GameObject player;
    public GameObject gameCamera;
    [SerializeField] public GameData gameData;
    
    public float remainingTimeAlive = 10.0f;
    public float slowDownTimeEffect = 1.0f;
    public bool timeOut = false;
    public bool smokeClosed = false;
    public bool safePlace = false;

    private SmokeController smoke;
    private Player playerScript;
    private bool _timeChanged;
    public YouDiedPanel diedPanel;
    public YouDiedPanel easterEggPanel;
    [SerializeField] private Vector3 respawnPosition;

    private Dictionary<string, ItemMenu> _itemMenus = new Dictionary<string, ItemMenu>();
    [SerializeField] private List<Enemy> _enemies;
    public List<Enemy> Enemies { get { return _enemies; } }

    [SerializeField] private List<ResourceBehaviour> _resources;
    [SerializeField] private GameObject _resourcePrefab;

    public List<Upgrade> _upgrades;

    private List<IObserved> _observeds;

    [SerializeField] private GameObject respawnPointsPrefab;
    public static GameManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
        slowDownTimeEffect = gameData.TimeScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Debug.Log(player.GetInstanceID());
        playerScript = player.GetComponent<Player>();
        timeText = GameObject.Find("Time").GetComponent<TextMeshProUGUI>();
        smoke = GameObject.Find("SmokeCloud").GetComponent<SmokeController>();
        playerScript = player.GetComponent<Player>();
        _itemMenus = new Dictionary<string, ItemMenu>()
        {
            {"Stone", new ItemMenu("Stone", 3)}
        };

        // Complete data on beginning
        UpdatePlayer();
        UpdateEnemies();
        UpdateRespawnPoints();
        gameData.Resources.ForEach(resource => {
            GameObject obj = Instantiate(_resourcePrefab, resource, Quaternion.identity);
            _resources.Add(obj.GetComponent<ResourceBehaviour>());
        });

    }

    private void UpdatePlayer() {
        player.GetComponent<CharacterController>().enabled = false;
        playerScript.gameObject.transform.position = gameData.Player.position;
        respawnPosition = gameData.Player.position;
        player.GetComponent<CharacterController>().enabled = true;
        Debug.Log("Set Player position " + playerScript.gameObject.transform.position);
        playerScript.HP = gameData.Player.hp;
        playerScript.PlayerSpeed = gameData.Player.speed;
        playerScript.SkillPoints = gameData.Player.skillPoints;
        gameCamera.GetComponent<FollowPlayer>().ResetOffset();
    }

    private void UpdateEnemies() {
        _enemies = new List<Enemy>();
        Debug.Log(gameData.Enemies.Count);
        foreach (EnemyData enemyData in gameData.Enemies) {
            Debug.Log("enemyData: " + enemyData.position);
            GameObject enemy = Instantiate(enemyData.prefab, enemyData.position, Quaternion.identity);
            if (enemyData.position == new Vector3(123, 6.5f, 490)) {
                enemy.SetActive(false);
            }
            enemy.GetComponent<Enemy>().Hp = enemyData.hp;
            enemy.GetComponent<Enemy>().GivesUpgrade = enemyData.givesUpgrade;
            enemyData.observers?.ForEach(observer => enemy.GetComponent<Enemy>().AddObserver(observer));
            if (enemy.GetComponent<Enemy>() == null) Debug.LogError("Enemy is null");
            _enemies.Add(enemy.GetComponent<Enemy>());
        }
    }

    private void UpdateRespawnPoints()
    {
        foreach(Vector3 respawnPointPos in gameData.RespawnPoints)
        {
            if (respawnPointPos != new Vector3(120, 6.5f, 490))
                Instantiate(respawnPointsPrefab, respawnPointPos, Quaternion.identity);
        }
    }

    public void UpdateTime(float timeDifference)
    {
        // do not allow time to pass if player is near thing
        remainingTimeAlive += timeDifference * slowDownTimeEffect;
        _timeChanged = true;
    }

    public void LateUpdate()
    {
        if (slowDownTimeEffect == 1.0f)
        {
            slowText.text = "";
        }
        else
        {
            slowText.text = "Slow: " + (int) (1/slowDownTimeEffect) + "x";
        }
        
        if (!_timeChanged)
        {
            return;
        }

        if (remainingTimeAlive <= 0)
        {
            Debug.Log("respawning");
            timeOut = true;
            if (playerScript.PressingSpace) {
                StartEasterEgg();
            }
            else {
                StartRespawn();
            }
            timeText.text = "Time: " + "0.00";
        }
        else{
            string result= string.Format("{0:0.00}", remainingTimeAlive );
            timeText.text = "Time: " + result;
        }
        hpText.text = "HP: " + Mathf.Max(0, playerScript.HP);
        _timeChanged = false;
    }

    public void SetRespawnPosition(Vector3 position)
    {
        if (!timeOut)
        {
            respawnPosition = position;
        }
    }

    public void StartRespawn()
    {
        smoke.CloseSmoke();
        diedPanel.Open();
        StartCoroutine(DelayRespawn(2.2f));
    }

    public void StartEasterEgg()
    {
        smoke.CloseSmoke();
        easterEggPanel.Open();
        StartCoroutine(DelayPlay(2.2f));
    }

    IEnumerator DelayPlay(float duration)
    {
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        
        AllowPlayerToMove();
    }

    public void AllowPlayerToMove()
    {
        ResetTime();
        if (!player.activeSelf) 
        {
            player.SetActive(true);
        }
        playerScript.ResetMove();
        smoke.OpenSmoke();
        easterEggPanel.Close();
        timeOut = false;
    }


    IEnumerator DelayRespawn(float duration)
    {
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        
        RespawnPlayer();
    }

    public void RespawnPlayer()
    {
        ResetTime();
        if (!player.activeSelf) 
        {
            player.SetActive(true);
        }
        playerScript.Respawn(respawnPosition);
        gameCamera.GetComponent<FollowPlayer>().ResetOffset();
        smoke.OpenSmoke();
        diedPanel.Close();
        timeOut = false;
    }

    public void ResetTime()
    {
        remainingTimeAlive = 10.0f;
        _timeChanged = true;
        playerScript.ResetMove();
    }

    public void SwitchCheckpoint()
    {
        ResetTime();
        playerScript.ResetHealth();
    }
        
    public void CollectItem(Item item) {
        if (_itemMenus.ContainsKey(item.Name))
        {
            _itemMenus[item.Name].AddItem(item);
        }
        else
        {
            _itemMenus.Add(item.Name, new ItemMenu(item.Name, 1));
            Debug.Log("Added " + item.Name + " to inventory");
        }
        slowDownTimeEffect = 0.5f; // to simplify, assume all items are Time Stones for now
    }

    public void CompleteTask(Task task) {
        task.FollowUpTasks.ForEach(t => gameData.ToDoTasks.Add(t));
        gameData.InProgressTasks.Remove(task);
        gameData.CompletedTasks.Add(task);
        task.Region = "CompletedRegion";
        Debug.Log("completed");
    }

    public void OpenMenu()
    {
        SaveData();
        SceneManager.LoadSceneAsync(1);
    }

    private void SaveData() {
        gameData.TimeScale = slowDownTimeEffect;
        gameData.Player = new PlayerData{
            position = playerScript.gameObject.transform.position,
            hp = playerScript.HP,
            skillPoints = playerScript.SkillPoints,
            speed = playerScript.PlayerSpeed
        };
        List<EnemyData> previous = gameData.Enemies;
        List<Vector3> resources = new List<Vector3>();
        _resources.ForEach(resource => { if (resource) resources.Add(resource.transform.position); });
        gameData.Resources = resources;
        gameData.Enemies = new List<EnemyData>();
        Debug.Log("Enemies count: " + _enemies.Count);
        Debug.Log("Enemies2 : " + previous.Count);
        for (int i = 0; i < _enemies.Count; i++) {
            Debug.Log("is enemy null" + i +_enemies[i]);
            if (_enemies[i] != null) {
                gameData.Enemies.Add(new EnemyData{
                    prefab = previous[i].prefab,
                    position = _enemies[i].transform.position,
                    hp = _enemies[i].Hp,
                    givesUpgrade = _enemies[i].GivesUpgrade,
                    observers = previous[i].observers
                });
            }
        }
    }

}