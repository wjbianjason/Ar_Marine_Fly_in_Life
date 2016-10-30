using UnityEngine;
using System.Collections;

public class displayWebcam : MonoBehaviour
{
    public WebCamTexture cameraTexture;
    public GameObject planbObj;
    public string cameraName = "";
    private bool isPlay = false;
    public Texture2D texImage;
    private int imWidth = 640;
    private int imHeight = 480;
    // Use this for initialization  
    void Start()
    {     
        StartCoroutine(Test());
    }

    // Update is called once per frame  
    void Update()
    {

    }

    IEnumerator Test()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {   

            WebCamDevice[] devices = WebCamTexture.devices;
            cameraName = devices[0].name;
            cameraTexture = new WebCamTexture(cameraName, 400, 300, 15);
            cameraTexture.Play();
            planbObj = GameObject.Find("Quad");
            texImage = new Texture2D(cameraTexture.width, cameraTexture.height);
            
            isPlay = true;
        }
    }

    void OnGUI()
    {
        if (isPlay)
        {
            texImage.SetPixels(cameraTexture.GetPixels());
            for (int v = 0; v < cameraTexture.width; ++v)
            {
                for (int j = 0; j < cameraTexture.height; ++j)
                {
                    float b = texImage.GetPixel(v, j).b * 255;
                    float g = texImage.GetPixel(v, j).g * 255;
                    float r = texImage.GetPixel(v, j).r * 255;
                    if ((b-r) > 30 || (g-r) > 30)
                    {
                        Color color = new Color(0, 0, 0,0);
                        texImage.SetPixel(v, j, color);
                    }
                }
            }

                texImage.Apply();
            //GUI.DrawTexture(new Rect(0, 0, 400, 300), texImage, ScaleMode.StretchToFill);
            planbObj.GetComponent<Renderer>().material.mainTexture = texImage;
        }
    }
}