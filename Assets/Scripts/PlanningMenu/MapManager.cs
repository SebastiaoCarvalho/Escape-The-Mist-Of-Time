using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    [SerializeField] private GameData gameData;
    [SerializeField] private GameObject MapIconPrefab;
    [SerializeField] private GameObject MiddleOfTheMap;
    private List<GameObject> instantiatedButtons = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        DrawOnMap();
    }


    private void DrawOnMap()
    {
        //Check if enemy in gameData is in a box of x<80 and y<50 of player
        Vector3 playerPos = gameData.Player.position;

        foreach (EnemyData enemy in gameData.Enemies)
        {
            if ((enemy.position.x < playerPos.x + 80 && enemy.position.x > playerPos.x - 80) &&
                (enemy.position.z < playerPos.z + 50 && enemy.position.z > playerPos.z - 50))
            {
                Vector3 enemyMapPos = new Vector3((enemy.position.x - playerPos.x) * 2.5f, (enemy.position.z - playerPos.z) * 2.5f, 0);
                inicializePrefab(Instantiate(MapIconPrefab), "Enemy", enemyMapPos);
            }

        }

        foreach(Vector3 resources in gameData.Resources)
        {
            if ((resources.x < playerPos.x + 80 && resources.x > playerPos.x - 80) &&
                (resources.z < playerPos.z + 50 && resources.z > playerPos.z - 50))
            {
                Vector3 enemyMapPos = new Vector3((resources.x - playerPos.x) * 2.5f, (resources.z - playerPos.z) * 2.5f, 0);
                inicializePrefab(Instantiate(MapIconPrefab), "Resource", enemyMapPos);
            }
        }

        foreach(Vector3 respawPoints in gameData.RespawnPoints)
        {
            if ((respawPoints.x < playerPos.x + 80 && respawPoints.x > playerPos.x - 80) &&
                (respawPoints.z < playerPos.z + 50 && respawPoints.z > playerPos.z - 50))
            {
                Vector3 enemyMapPos = new Vector3((respawPoints.x - playerPos.x) * 2.5f, (respawPoints.z - playerPos.z) * 2.5f, 0);
                inicializePrefab(Instantiate(MapIconPrefab), "Checkpoint", enemyMapPos);
            }
        }

        /*
        foreach(Item items in gameData.Items)
        {
            if ((items..x < playerPos.x + 80 && items.position.x > playerPos.x - 80) &&
                 (items.position.z < playerPos.z + 50 && items.position.z > playerPos.z - 50))
            {
                Vector3 enemyMapPos = new Vector3((items.position.x - playerPos.x) * 2.5f, (items.position.z - playerPos.z) * 2.5f, 0);
                inicializePrefab(Instantiate(MapIconPrefab), "Enemy", enemyMapPos);
            }
        }*/
    }

    private void inicializePrefab(GameObject instantiated, string text, Vector3 pos)
    {
        instantiatedButtons.Add(instantiated);
        instantiated.transform.SetParent(MiddleOfTheMap.transform, false);
        instantiated.transform.localPosition = pos;
        instantiated.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
    }
}
