using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset = new Vector3(-3.0f, 0.0f, -12.0f);
	public float smoothTime = 0.5f;
    public float xlookaheadValue = 3.0f;
    public float zlookaheadValue = 8.0f;

	private Vector3 velocity = Vector3.zero;

    private float lastxComponent = 0;
    private float lastzComponent = 0;

    private float xBoost = 0;
    private float zBoost = 0;

    public float bost1;
    public float bost2;

    void Start() {
        transform.position = transform.position + offset;
    }

	void FixedUpdate() {
		Vector3 goalPos = player.transform.position;
		goalPos.y = transform.position.y;

        float xComponent = player.GetComponent<Player>().lastHorizontalValue * xlookaheadValue;
        if (player.GetComponent<Player>().lastHorizontalValue == 0) {
            xBoost = 0.0f;
        }
        if (lastxComponent == 0) {
            xBoost = player.GetComponent<Player>().lastHorizontalValue * bost1;
            xComponent += xBoost;
        }
        xComponent = xComponent == 0 ? lastxComponent : xComponent;
        lastxComponent = xComponent;
        xComponent += xBoost;

        float zComponent = player.GetComponent<Player>().lastVerticalValue * zlookaheadValue;
        if (player.GetComponent<Player>().lastHorizontalValue == 0) {
            zBoost = 0.0f;
        }
        if (lastxComponent == 0) {
            zBoost = player.GetComponent<Player>().lastVerticalValue * bost2;
            zComponent += zBoost;
        }
        zComponent = zComponent == 0 ? lastzComponent : zComponent;
        lastzComponent = zComponent;
        zComponent += zBoost;

        Vector3 lookahead = new Vector3(xComponent, 0.0f, zComponent);

		transform.position = Vector3.SmoothDamp (transform.position, goalPos + offset + lookahead, ref velocity, smoothTime);
	}
}
