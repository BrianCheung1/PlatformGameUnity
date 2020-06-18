using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public static UIHealthBar instance { get; private set; }                                                 //allows any script to call this property, private set becasue we don't want other scripts to change the values
    public Image mask;
    float originalSize;

   void Awake()
    {
        instance = this;                                                                                     //when game starts, this is stored into the instance which will return the health
    }
    // Start is called before the first frame update
    void Start()
    {
        originalSize = mask.rectTransform.rect.width;                                                        //Changes health bar to the size it is default to
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);    //sets the value of the health bar
    }
}
