using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public GameObject player;
    public GameObject gameCamera;
    public GameData gameData;

    public float remainingTimeAlive = 10.0f;
    public float slowDownTimeEffect = 1.0f;
    public bool timeOut = false;
    public bool smokeClosed = false;
    public bool safePlace = false;

    private SmokeController smoke;
    private Player playerScript;
    public YouDiedPanel diedPanel;
    [SerializeField] private Vector3 respawnPosition;

    private Dictionary<string, ItemMenu> _itemMenus = new Dictionary<string, ItemMenu>();
     private List<Task> _toDoTasks;
    private List<Task> _inProgressTasks;
    private List<Task> _completedTasks;
    private List<IObserved> _observeds;

    public static GameManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeText = GameObject.Find("Time").GetComponent<TextMeshProUGUI>();
        smoke = GameObject.Find("SmokeCloud").GetComponent<SmokeController>();
        playerScript = player.GetComponent<Player>();
        _itemMenus = new Dictionary<string, ItemMenu>()
        {
            {"Stone", new ItemMenu("Stone", 3)}
        };

        _toDoTasks = new List<Task>(){
            new Task("Task D", "survive you dumbass", "ToDoRegion"),
            new Task("Task C", "Just die please", "ToDoRegion")
        };
        _inProgressTasks = new List<Task>() {
            new Task("Task B", "kill 'Mr Capeta'", "InProgressRegion")
        };
        _completedTasks = new List<Task>() {
            new Task("Task A", "simply exist", "CompletedRegion")
        };
        _observeds = new List<IObserved>
        {
            GameObject.Find("Enemy").GetComponent<Enemy>()
        };
        _observeds[0].AddObserver(_inProgressTasks[0]);


        gameData = ScriptableObject.CreateInstance<GameData>();
        gameData.player = playerScript;

    }

    public void UpdateTime(float timeDifference)
    {
        // do not allow time to pass if player is near thing
        remainingTimeAlive += timeDifference * slowDownTimeEffect;
        
        if (remainingTimeAlive <= 0)
        {
            Debug.Log("respawning");
            timeOut = true;
            smoke.CloseSmoke();
            diedPanel.Open();
            StartCoroutine(DelayRespawn(2.2f));
            timeText.text = "Time: " + "0.00";
        }
        else{
            string result= string.Format("{0:0.00}", remainingTimeAlive );
            timeText.text = "Time: " + result;
        }
    }

    public void SetRespawnPosition(Vector3 position)
    {
        respawnPosition = position;
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
        playerScript.Respawn(respawnPosition);
        gameCamera.GetComponent<FollowPlayer>().ResetOffset();
        smoke.OpenSmoke();
        diedPanel.Close();
        timeOut = false;
    }

    public void ResetTime()
    {
        remainingTimeAlive = 10.0f;
        UpdateTime(0.0f);
        playerScript.ResetMove();
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
    }

    public void CompleteTask(Task task) {
        _inProgressTasks.Remove(task);
        _completedTasks.Add(task);
        task.Region = "CompletedRegion";
        Debug.Log("completed");
    }
}
