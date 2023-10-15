using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBehaviour : MonoBehaviour
{
    
    private string _name;
    private string _description;
    // Start is called before the first frame update
    void Start()
    {
        _name = this.gameObject.name;
        _description = "This is a " + _name;

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.RotateAround(this.transform.position, Vector3.up, 100f * Time.deltaTime);
    }

    private void OnDestroy() {
        GameManager.Instance.CollectItem(new Item(_name, _description));
    }
}
