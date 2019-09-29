using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GesturePromptWidgetManager : MonoBehaviour
{
    public static GesturePromptWidgetManager _instance { get; set; }
    public GameObject widgetObject;
    public Image gestureImage;
    public List<Sprite> gestureSpriteList;
    public Text widgetText;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void ToggleWidgetObject(bool condition)
    {
        widgetObject.SetActive(condition);
    }

    public void ModifyWidget(int imageIndex, string text)
    {
        gestureImage.sprite = gestureSpriteList[imageIndex];
        widgetText.text = text;
    }
}
