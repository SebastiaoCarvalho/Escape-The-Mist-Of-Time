using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{

    [SerializeField] private GameData gameData;
    [SerializeField] private GameObject MapIconResource;
    [SerializeField] private GameObject MapIconEnemy;
    [SerializeField] private GameObject MapIconRespawnPoint;
    [SerializeField] private GameObject MiddleOfTheMap;
    private List<GameObject> instantiatedButtons = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        instantiatedButtons.Add(GameObject.Find("MapIconPlayer"));
        DrawOnMap();
    }

    private void Update()
    {
        
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
                inicializePrefab(Instantiate(MapIconEnemy), enemyMapPos);
            }

        }

        foreach(Vector3 resources in gameData.Resources)
        {
            if ((resources.x < playerPos.x + 80 && resources.x > playerPos.x - 80) &&
                (resources.z < playerPos.z + 50 && resources.z > playerPos.z - 50))
            {
                Vector3 enemyMapPos = new Vector3((resources.x - playerPos.x) * 2.5f, (resources.z - playerPos.z) * 2.5f, 0);
                inicializePrefab(Instantiate(MapIconResource), enemyMapPos);
            }
        }

        foreach(Vector3 respawPoints in gameData.RespawnPoints)
        {
            if ((respawPoints.x < playerPos.x + 80 && respawPoints.x > playerPos.x - 80) &&
                (respawPoints.z < playerPos.z + 50 && respawPoints.z > playerPos.z - 50))
            {
                Vector3 enemyMapPos = new Vector3((respawPoints.x - playerPos.x) * 2.5f, (respawPoints.z - playerPos.z) * 2.5f, 0);
                inicializePrefab(Instantiate(MapIconRespawnPoint), enemyMapPos);
            }
        }

        foreach(GameObject button in instantiatedButtons)
        {            
            button.GetComponent<Button>().onClick.AddListener(delegate { OnClickMapIconButton(); });
        }
    }

    private void inicializePrefab(GameObject instantiated, Vector3 pos)
    {
        instantiatedButtons.Add(instantiated);
        instantiated.transform.SetParent(MiddleOfTheMap.transform, false);
        instantiated.transform.localPosition = pos;
    }

    public void OnClickMapIconButton()
    {
        //Can't use the On Click in a prefab button, for now only workd on Player icon
        GameObject buttonEvent = EventSystem.current.currentSelectedGameObject;

        foreach(GameObject button in instantiatedButtons)
        {
            if(button == buttonEvent)
            {
                //Put clicked button on top of hierarchy 
                button.transform.SetAsLastSibling();
                break;
            }
        }
    }
}
