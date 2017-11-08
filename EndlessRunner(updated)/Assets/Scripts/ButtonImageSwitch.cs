using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonImageSwitch : MonoBehaviour
{

    public Image button;
    public Button skin1;
    public Button skin2;
    public Button skin3;
    public Sprite unlocked;
    public Sprite locked;
    public PlayerController thePlayer;
    public bool pressed1;
    public bool pressed2;
    public bool pressed3;
    // Use this for initialization
    void Start()
    {
        thePlayer = (PlayerController)FindObjectOfType<PlayerController>();
        if (button.GetComponent<Button>().interactable)
        {
            button.GetComponent<Image>().sprite = unlocked;
        }
        else
        {
            button.GetComponent<Image>().sprite = locked;
        }
        /*
        skin1.onClick.AddListener(taskOnClick1);
        skin2.onClick.AddListener(taskOnClick2);
        skin3.onClick.AddListener(taskOnClick3);
       */
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
        /*
        skin1.onClick.AddListener(taskOnClick1);
        skin2.onClick.AddListener(taskOnClick2);
        skin3.onClick.AddListener(taskOnClick3);
      */
    }
   

    //This doesn't work. Doesn't actually change the player object values from endless1,2,3. 
    /*
    public void taskOnClick1()
    {
        Debug.Log("Pressed 1");
        PlayerController.isRRowdy = true;
        PlayerController.isCRowdy = false;
        PlayerController.isARowdy = false;
    }
    public void taskOnClick2()
    {
        Debug.Log("pressed 2");

        PlayerController.isRRowdy = false;
        PlayerController.isCRowdy = false;
        PlayerController.isARowdy = true;
    }
    public void taskOnClick3()
    {
        Debug.Log("pressed 3");
        PlayerController.isRRowdy = false;
        PlayerController.isCRowdy = true;
        PlayerController.isARowdy = false;

    }
    */
}
