using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroller : MonoBehaviour
{
    public Renderer[] layers;
    public float[] speedMultiplier;
    private float previousXPositionMario;
    private float previousXPostionCamera;
    public Transform mario;
    public Transform mainCamera;
    private float[] offset; // Keeps track of each background object's material x-offset value

    // Start is called before the first frame update
    void Start()
    {
        offset = new float[layers.Length];
        for (int i = 0; i < layers.Length; i++)
        {
            offset[i] = 0.0f;
        }
        previousXPositionMario = mario.transform.position.x;
        previousXPostionCamera = mainCamera.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        // If camera has moved
        if (Mathf.Abs(previousXPostionCamera - mainCamera.transform.position.x) > 0.001f)
        {
            for (int i = 0; i < layers.Length; i++)
            {
                if (offset[i] > 1.0f || offset[i] < - 1.0f)
                {
                    offset[i] = 0.0f; // Reset offset
                }
                float newOffset = mario.transform.position.x - previousXPositionMario;
                offset[i] = offset[i] + newOffset * speedMultiplier[i];
                layers[i].material.mainTextureOffset = new Vector2(offset[i], 0);
            }
        }

        // Update previous position
        previousXPositionMario = mario.transform.position.x;
        previousXPostionCamera = mainCamera.transform.position.x;
    }
}
