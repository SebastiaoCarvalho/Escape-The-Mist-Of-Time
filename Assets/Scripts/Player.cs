using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine;

public struct PlayerData {
    public Vector3 position;
    public float hp;
    public float speed;
    public int skillPoints;
}

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private GameManager gameManager;
    private float _playerSpeed = 7.0f;
    public float PlayerSpeed { get { return _playerSpeed; } set { _playerSpeed = value; } }
    private float _attackRange = 5f;
    private float _hp = 100f;
    public float HP { get { return _hp; } set { _hp = value; } }
    public bool Moved { get { return moved; } }
    public float lastHorizontalValue;
    private bool _clicked;
    public float lastVerticalValue;
    private int _skillPoints;
    public int SkillPoints { get { return _skillPoints; } set { _skillPoints = value; } }

    private bool moved = false;

    private void Start()
    {
        Debug.Log("Player position " + transform.position);
        controller = gameObject.GetComponent<CharacterController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.SetRespawnPosition(transform.position);
    }

    private void Update() {
        this.gameObject.transform.eulerAngles = Vector3.zero;  // fix rotation
    }

    void FixedUpdate()
    {
        if (gameManager.timeOut)
        {
            return;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), -9.81f, Input.GetAxis("Vertical"));
        
        controller.Move(_playerSpeed * Time.deltaTime * move);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            if (move.x != 0 || move.z != 0)
            {
                moved = true;
            }
            if (moved)
            {
                gameManager.UpdateTime(-Time.deltaTime);
            }
        }

        if (Input.GetAxis("Horizontal") == 0) {
            lastHorizontalValue = 0;
        }
        else if (Input.GetAxis("Horizontal") > 0) {
            lastHorizontalValue = 1;
        }
        else {
            lastHorizontalValue = -1;
        }

        if (Input.GetAxis("Vertical") == 0) {
            lastVerticalValue = 0;
        }
        else if (Input.GetAxis("Vertical") > 0) {
            lastVerticalValue = 1;
        }
        else {
            lastVerticalValue = -1;
        }
        if (Input.GetMouseButton((int) MouseButton.Left) && !_clicked) {
            GameObject enemy = ClosestEnemy();
            _clicked = true;
            if (enemy != null) 
                enemy.GetComponent<Enemy>().TakeDamage(10); // For now kill enemy in one hit
        }
        else if (_clicked && !Input.GetMouseButton((int) MouseButton.Left)) {
            _clicked = false;
        }
    }

    private GameObject ClosestEnemy() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject enemy in enemies) {
            Vector3 diff = enemy.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < _attackRange && curDistance < distance) {
                closest = enemy;
                distance = curDistance;
            }
        }
        return closest;
    }

    public void Respawn(Vector3 position)
    {
        controller.enabled = false;
        controller.transform.position = position;
        controller.enabled = true;
        lastHorizontalValue = 0;
        lastVerticalValue = 0;
        ResetHealth();
        moved = false;
    }

    public void ResetMove()
    {
        moved = false;
    }

    public void ResetHealth()
    {
        _hp = 100f;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Enemy")) {
            _hp -= 10;
            if (_hp <= 0) {
                gameManager.timeOut = true;
                gameManager.StartRespawn();
                gameObject.SetActive(false);
                return;
            }
            Vector3 bounceBack = transform.position - other.gameObject.transform.position;
            bounceBack.y = 0.5f; // add bounce back a little up
            bounceBack.Normalize();
            controller.Move(bounceBack * 5f);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Resource")) {
            Destroy(other.gameObject);
        }
    }
    
    public void SpendSkillPoints(int amount) {
        _skillPoints -= amount;
    }

    public void AddSkillPoint() {
        _skillPoints++;
    }

}
