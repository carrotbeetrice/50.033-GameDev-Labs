using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Mario's Transform
    public Transform endLimit; // GameObject that indicates end of map
    private float offset; // Initial x-offset between camera and Mario;
    private float startX; // Smallest x-coordinate of Camera
    private float endX; // Largest x-coordinate of Camera
    private float viewportHalfWidth;

    // Start is called before the first frame update
    void Start()
    {
        // Get coordinates of the bottom left of the viewport
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(Vector3.zero);
        viewportHalfWidth = Mathf.Abs(bottomLeft.x - this.transform.position.x);

        offset = this.transform.position.x - player.position.x;
        startX = this.transform.position.x;
        endX = endLimit.transform.position.x - viewportHalfWidth;
    }

    // Update is called once per frame
    void Update()
    {
        // Follow player unless it reaches the end of the game map
        float desiredX = player.position.x + offset;
        if (desiredX > startX && desiredX < endX) {
            this.transform.position = new Vector3(desiredX, this.transform.position.y, this.transform.position.z);
        }
    }
}
