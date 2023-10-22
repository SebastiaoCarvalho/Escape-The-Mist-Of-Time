using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class UpgradeManager : MonoBehaviour
{

    private GameObject _upgradeScreen;
    public GameObject UpgradePrefab;
    public Material LineMaterial;
    private Upgrade _head;
    public Upgrade Head {
        get { return _head; }
        set { _head = value; _isDirty = true; }
    }
    private bool _hasNew = false;
    public bool HasNew { get { return _hasNew; } set { _hasNew = value; } }
    [SerializeField] private GameData GameData;
    [SerializeField] private GameObject ExclamationMarkPrefab;
    private bool _isDirty = true;

    public static UpgradeManager Instance { get; set; }
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        _upgradeScreen = this.gameObject.transform.parent.gameObject;
    }

    private void Start() {
        _head = GameData.Upgrades[0];
        GameObject.Find("Points").GetComponent<TextMeshProUGUI>().text = "Points: " + GameData.Player.skillPoints;
        TestForNewUpgrades();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDirty) {
            CleanTree();
            InitializeUpgrades();
            Reload();
        }
    }

    private void InitializeUpgrades() {
        List<Upgrade> upgrades = new List<Upgrade> { _head };
        while (upgrades.Count > 0) {
            Upgrade upgrade = upgrades[0];
            upgrades.RemoveAt(0);
            upgrade.InitializePrefab(Instantiate(UpgradePrefab));
            for (int i = 0; i < upgrade.ChildCount; i++) {
                upgrades.Add(upgrade.GetChild(i));
            }
        }
    }

    private void TestForNewUpgrades() {
        List<Upgrade> upgrades = new List<Upgrade> { _head };
        while (upgrades.Count > 0) {
            Upgrade upgrade = upgrades[0];
            upgrades.RemoveAt(0);
            if (IsAffordable(upgrade) && ! upgrade.IsUnlocked) {
                Debug.Log(upgrade.Parent);
                AddNewUpgradeSign();
                return;
            }
            for (int i = 0; i < upgrade.ChildCount; i++) {
                upgrades.Add(upgrade.GetChild(i));
            }
        }
    }

    public void VisitUpgrades() {
        List<Upgrade> upgrades = new List<Upgrade> { _head };
        while (upgrades.Count > 0) {
            Upgrade upgrade = upgrades[0];
            upgrades.RemoveAt(0);
            if (IsAffordable(upgrade)) {
                upgrade.Unlock();
            }
            if (upgrade.IsPurchased) continue;
            for (int i = 0; i < upgrade.ChildCount; i++) {
                upgrades.Add(upgrade.GetChild(i));
            }
        }
    }

    private void AddNewUpgradeSign() {
        GameObject exclamationMark = Instantiate(ExclamationMarkPrefab);
        GameObject button = GameObject.Find("UpgradesButton");
        exclamationMark.transform.SetParent(button.transform, false);
        float width = button.GetComponent<RectTransform>().rect.width/2;
        float offset = exclamationMark.GetComponent<RectTransform>().rect.width;
        exclamationMark.transform.localPosition = new Vector3(width + offset, 0, 0);
        _hasNew = true;
    }

    private void Reload() {
        Rect rect = _upgradeScreen.GetComponent<RectTransform>().rect;
        GameObject upgrade = _head.Object;
        float angDiff = 180 / (_head.ChildCount + 1);
        float height = UpgradePrefab.GetComponent<RectTransform>().rect.height;
        upgrade.transform.SetParent(_upgradeScreen.transform, false);
        upgrade.transform.localPosition = new Vector3(0, rect.yMax - height/2, 0);
        for (int i = 0; i < _head.ChildCount; i++)
        {
            GameObject child = _head.GetChild(i).Object;
            Debug.Log(Quaternion.Euler(0, 0, (i + 1) * angDiff) * Vector3.down * height);
            child.transform.SetParent(_upgradeScreen.transform, false);
            child.transform.localPosition = upgrade.transform.localPosition + Quaternion.Euler(0, 0, (i + 1) * angDiff) * Vector3.left * height * 1.5f ;
            DrawUpgradeLine(upgrade, child);
        }
        _isDirty = false;
    }

    public void DrawUpgradeLine(GameObject upgrade, GameObject child) {
        GameObject line = new GameObject("Line");
        line.transform.parent = upgrade.transform.parent;
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        lineRenderer.startWidth = 6f;
        lineRenderer.endWidth = 6f;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;
        lineRenderer.SetPosition(0, upgrade.transform.position);
        lineRenderer.SetPosition(1, child.transform.position);
    }

    private void CleanTree()
    {
        foreach (Transform child in _upgradeScreen.transform)
        {
            if (! child.name.Equals("UpgradeManager") && !child.name.Equals("Points")) Destroy(child.gameObject);
        }
    }

    public void OnClick(Upgrade upgrade) {
        if (upgrade.Cost > GameData.Player.skillPoints) return;
        if (! upgrade.IsPurchased) upgrade.Purchase();
        GameData.Player = new PlayerData{
            position = GameData.Player.position,
            hp = GameData.Player.hp,
            skillPoints = GameData.Player.skillPoints - upgrade.Cost,
            speed = GameData.Player.speed,
        };
        _isDirty = true;
        GameObject.Find("Points").GetComponent<TextMeshProUGUI>().text = "Points: " + GameData.Player.skillPoints;
        InitializeUpgrades();
    }

    public bool IsBuyable(Upgrade upgrade) {
        return (upgrade.Parent == null || upgrade.Parent.IsPurchased) && !upgrade.IsPurchased && IsAffordable(upgrade);
    }

    public bool IsAffordable(Upgrade upgrade) {
        return upgrade.Cost <= GameData.Player.skillPoints;
    }
}
