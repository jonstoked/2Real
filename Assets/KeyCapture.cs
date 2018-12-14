using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class KeyCapture : MonoBehaviour
{
    WebCamTexture webCamTexture;
    public GameObject plane;
    public GameObject mainCamera;
    bool inProgress = false;

    // Use this for initialization
    void Start()
    {
        webCamTexture = new WebCamTexture();
        WebCamDevice[] devices = WebCamTexture.devices;
        webCamTexture.deviceName = devices[0].name;
        for (int i = 0; i < devices.Length; i++)
        {
            if(devices[i].name.Contains("Kinect"))
            {
                webCamTexture.deviceName = devices[i].name;
            }
            Debug.Log(devices[i].name);
        }
        plane.GetComponentInChildren<Renderer>().enabled = false;
        plane.GetComponentInChildren<Renderer>().material.mainTexture = webCamTexture;
        webCamTexture.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("space") && inProgress == false)
        {
            inProgress = true;
            AlternatePhoto();
        }
    }

    public void AlternatePhoto()
    {

        Debug.Log("GETTING READY");
        showCamera(true);
        Invoke("StopRenderer2", 7);
    }

    void StopRenderer2()
    {
        showCamera(false);
        TakePhoto();
    }

    public void showCamera(bool show)
    {
        plane.GetComponentInChildren<Renderer>().enabled = show;
    }

    public void TakePhoto()
    {

        Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();

        //Encode to a PNG
        byte[] bytes = photo.EncodeToPNG();
        //Write out the PNG. Of course you have to substitute your_path for something sensible
        DateTime dt = DateTime.Now;
        string datetime = dt.ToString("yyyyMMddTHHmmssZ");

        string filepath = Application.persistentDataPath + "/" + datetime + ".png";
        string screenfilepath = Application.persistentDataPath + "/" + "SS" + datetime + ".png";
        Debug.Log("PHOTOS FilePath: " + filepath);
        File.WriteAllBytes(filepath, bytes);
        CaptureCamera(screenfilepath);

    }

    void CaptureCamera(string filepath)
    {
        int resWidth = 1920;
        int resHeight = 1080;
        Camera camera = mainCamera.GetComponent<Camera>();
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        camera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        camera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        camera.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        byte[] bytes = screenShot.EncodeToPNG();
        File.WriteAllBytes(filepath, bytes);
        inProgress = false;
    }
}
