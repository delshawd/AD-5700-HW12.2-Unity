
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    //class property can be accessed by multiple functions
    WebCamTexture feed;
    Color capturedColor;
    bool firstCapture = true;

    // Start is called before the first frame update
    void Start()
    {
        feed = new WebCamTexture(); //new webcam texture
        //get renderer component from object to which this script
        // is attached, which could be any of the shape objects on screen
        GetComponent<Renderer>().material.mainTexture = feed;
        feed.Play(); //start capturing video and drawing to texture
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseDown()
    {
        Color[] pixels = feed.GetPixels();
        feed.Stop();
        float reds = 0;
        float greens = 0;
        float blues = 0;
        int len = pixels.Length;

        foreach (Color pix in pixels)
        {
            reds += pix.r;
            greens += pix.g;
            blues += pix.b;
        }

        Color newColor = new Color(reds / len, greens / len, blues / len);

        if (firstCapture)
        {
            capturedColor = newColor;
            FindObjectOfType<Camera>().backgroundColor = capturedColor;
            firstCapture = false;
            feed.Play();
        }
        else
        {
            GetComponent<Renderer>().material.mainTexture = null; //remove camera pixels
            GetComponent<Renderer>().material.color = newColor; //set to new color
        }
    }
}
