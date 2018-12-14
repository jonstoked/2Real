using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour {

    private Bounds bounds = new Bounds(new Vector3(0, 0, 2), new Vector3(5, 5, 2));
    private float speed = 0.12f; // 0.08f;
    public Vector3 direction;
    private BoxCollider boxCollider;
    private TrailRenderer trailRenderer;

    private bool freakingOut = false;
    private bool shouldStayInBounds = true;

    void Start () {
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.enabled = false;
        Invoke("Remove", 30);
    }

    void Update () {
        //pick random Z angle, to affect movement direction;
        float randomAngleZ = UnityEngine.Random.Range(-1f, 1f);
        direction = Quaternion.Euler(0, 0, randomAngleZ) * direction;

        transform.Translate(speed * direction * Time.deltaTime, Space.World);

        if(shouldStayInBounds) {
            HandleAtUpperOrLowerBounds();
            HandleAtLeftOrRightBounds();
        }
        
    }

    public void FreakOut() {
        if(!freakingOut) {
            freakingOut = true;
            trailRenderer.enabled = true;
            speed *= 50;
            Invoke("IgnoreBounds",2);
            Invoke("Remove", 4);
        }
    }

    private void IgnoreBounds() {
        shouldStayInBounds = false;
    }

    public void Remove () {
        var fishSpawner = GameObject.Find("FishSpawner").GetComponent<FishSpawner>();
        fishSpawner.fishes.Remove(gameObject);
        Destroy(gameObject);
    }

    private void HandleAtUpperOrLowerBounds()
    {
        if (transform.position.y > bounds.center.y + bounds.extents.y) //too high, move down
        {
            direction = Vector3.Reflect(direction, Vector3.down);
        }
        else if (transform.position.y < bounds.center.y - bounds.extents.y)
        {
            direction = Vector3.Reflect(direction, Vector3.up);
        }
    }

    private void HandleAtLeftOrRightBounds()
    {
        if (transform.position.x > bounds.center.x + bounds.extents.x)
        {
            direction = Vector3.Reflect(direction, Vector3.left);
            flipXDirection();
        }
        else if (transform.position.x < bounds.center.x - bounds.extents.x)
        {
            direction = Vector3.Reflect(direction, Vector3.right);
            flipXDirection();
        }
    }

    public void flipXDirection()
    {
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
}
