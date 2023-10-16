using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{

    private GameObject _upgradeScreen;
    public GameObject UpgradePrefab;
    public Material LineMaterial;
    public Upgrade _head;
    private bool _isDirty = true;

    public static UpgradeManager Instance { get; set; }
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        _upgradeScreen = this.gameObject.transform.parent.gameObject;
    }

    private void Start() {

    }

    // Update is called once per frame
    void Update()
    {
        if (_isDirty) {
            CleanTree();
            Reload();
        }
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
        lineRenderer.material = LineMaterial;
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
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
            if (child.name.Equals("Line")) Destroy(child.gameObject);
        }
    }

    public void OnClick(Upgrade upgrade) {
        if (! upgrade.IsPurchased) upgrade.Purchase();
    }
}
