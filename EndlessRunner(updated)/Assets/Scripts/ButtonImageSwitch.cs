using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonImageSwitch : MonoBehaviour
{

    public Image button;

    public Sprite unlocked;
    public Sprite locked;
    // Use this for initialization
    void Start()
    {
        if (button.GetComponent<Button>().interactable)
        {
            button.GetComponent<Image>().sprite = unlocked;
        }
        else
        {
            button.GetComponent<Image>().sprite = locked;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (button.GetComponent<Button>().interactable)
        {
            button.GetComponent<Image>().sprite = unlocked;
        }
        else
        {
            button.GetComponent<Image>().sprite = locked;
        }
    }
}
