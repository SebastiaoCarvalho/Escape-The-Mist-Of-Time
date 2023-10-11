using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    //private GameObject _mapRegion;
    [SerializeField] public GameObject Map;

    public static MenuManager Instance { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        //_mapRegion = GameObject.Find("MapRegion");
           
        ReloadMap();
    }

    // Update is called once per frame
    void Update()
    {
        ReloadMap();
    }

    private void ReloadMap()
    {

    }


    
}