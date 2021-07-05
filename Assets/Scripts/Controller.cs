using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class Controller : MonoBehaviour
{
    public string url = "";
    public Renderer thisRenderer;
    public Image canvasui;
    public SpriteRenderer SpriteUi;
    public RawImage rawimageui;
    public InputField txt;
    // Start is called before the first frame update
  public  void SetUi()
    {
           /* testurlhttps://i.pinimg.com/originals/9e/1d/d6/9e1dd6458c89b03c506b384f537423d9.jpg   */
        url = txt.text;
        StartCoroutine(ThreeDModelLoaderCoroutine()); 
        //mesh renderer load
        thisRenderer.material.color = Color.red;
        //ui image load
        StartCoroutine(GetImageFromWeb1(url));
        //sprite image load
        StartCoroutine(GetImageFromWeb2(url));
        StartCoroutine(ImageDownload(rawimageui));
    }

    // Update is called once per frame
    private IEnumerator ThreeDModelLoaderCoroutine()
    {
        Debug.Log("Loading ....");
        WWW wwwLoader = new WWW(url);    
        yield return wwwLoader;          
         Debug.Log("Loaded");
        thisRenderer.material.color = Color.white;               
        thisRenderer.material.mainTexture = wwwLoader.texture;
    }

    IEnumerator GetImageFromWeb1(string x)
    {
        UnityWebRequest reg = UnityWebRequestTexture.GetTexture(x);
        yield return reg.SendWebRequest();
        if (reg.isNetworkError || reg.isHttpError)
        {
            Debug.Log(reg.error);
        }
        else
        {
            Texture2D img = ((DownloadHandlerTexture)reg.downloadHandler).texture;
            canvasui.sprite = Sprite.Create(img,new Rect(0,0,297,296),Vector2.zero);
            SpriteUi.sprite= Sprite.Create(img, new Rect(0, 0, 297, 296), Vector2.zero);
        }
    }
    IEnumerator GetImageFromWeb2(string x)
    {
        UnityWebRequest reg = UnityWebRequestTexture.GetTexture(x);
        yield return reg.SendWebRequest();
        if (reg.isNetworkError || reg.isHttpError)
        {
            Debug.Log(reg.error);
        }
        else
        {
            Texture2D img = ((DownloadHandlerTexture)reg.downloadHandler).texture;
 
            SpriteUi.sprite = Sprite.Create(img, new Rect(0, 0, 297, 296), Vector2.zero);
        }
    }

    IEnumerator ImageDownload(RawImage img)
    {
        WWW www = new WWW(url);
        yield return www;
        Texture2D texure = new Texture2D(1, 1);
        texure.LoadImage(www.bytes);
        texure.Apply();
        img.texture = texure;

    }

}
