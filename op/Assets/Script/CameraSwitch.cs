using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraSwitch : MonoBehaviour {


    public Camera CameraOne;
    public Camera CameraTwo;
    public Camera CameraThree;
    public Camera CameraFour;
    public Camera CameraFive;
    public Camera CameraSix;
    public Camera CameraSeven;
    public Camera CameraBoat;

    // Use this for initialization
    void Start () {
        CameraBoat.enabled = false;
        CameraTwo.enabled = false;
        CameraOne.enabled = true;
        CameraThree.enabled = false;
        CameraFour.enabled = false;
        CameraFive.enabled = false;
        CameraSix.enabled = false;
        CameraSeven.enabled = false;


    }

    // Update is called once per frame
    void Update () {
	    if(Input.GetKeyUp("1"))
        {
            ShowOne();
        }
        else if(Input.GetKeyUp("2"))
        {
            ShowTwo();
        }
        else if (Input.GetKeyUp("3"))
        {
            ShowThree();
        }
        else if (Input.GetKeyUp("4"))
        {
            ShowFour();
        }
        else if (Input.GetKeyUp("5"))
        {
            ShowFive();
        }
        else if (Input.GetKeyUp("6"))
        {
            ShowSix();
        }
        else if (Input.GetKeyUp("7"))
        {
            ShowSeven();
        }

        else if (Input.GetKeyUp("0"))
        {
            ShowBoat();
        }
        else if (Input.GetKeyUp(KeyCode.Escape))
        {
            HappyBirthday();
        }
    }

    public void ShowOne()
    {
        CameraOne.enabled = true;
        CameraTwo.enabled = false;
        CameraBoat.enabled = false;
        CameraThree.enabled = false;
        CameraFour.enabled = false;
        CameraFive.enabled = false;
        CameraSix.enabled = false;
        CameraSeven.enabled = false;
    }
    public void ShowTwo()
    {
        CameraTwo.enabled = true;
        CameraOne.enabled = false;
        CameraBoat.enabled = false;
        CameraThree.enabled = false;
        CameraFour.enabled = false;
        CameraFive.enabled = false;
        CameraSix.enabled = false;
        CameraSeven.enabled = false;
    }
    public void ShowThree()
    {
        CameraTwo.enabled = false;
        CameraOne.enabled = false;
        CameraBoat.enabled = false;
        CameraThree.enabled = true;
        CameraFour.enabled = false;
        CameraFive.enabled = false;
        CameraSix.enabled = false;
        CameraSeven.enabled = false;
    }
    public void ShowFour()
    {
        CameraTwo.enabled = false;
        CameraOne.enabled = false;
        CameraBoat.enabled = false;
        CameraThree.enabled = false;
        CameraFour.enabled = true;
        CameraFive.enabled = false;
        CameraSix.enabled = false;
        CameraSeven.enabled = false;
    }
    public void ShowFive()
    {
        CameraTwo.enabled = false;
        CameraOne.enabled = false;
        CameraBoat.enabled = false;
        CameraThree.enabled = false;
        CameraFour.enabled = false;
        CameraFive.enabled = true;
        CameraSix.enabled = false;
        CameraSeven.enabled = false;
    }
    public void ShowSix()
    {
        CameraTwo.enabled = false;
        CameraOne.enabled = false;
        CameraBoat.enabled = false;
        CameraThree.enabled = false;
        CameraFour.enabled = false;
        CameraFive.enabled = false;
        CameraSix.enabled = true;
        CameraSeven.enabled = false;
    }
    public void ShowSeven()
    {
        CameraTwo.enabled = false;
        CameraOne.enabled = false;
        CameraBoat.enabled = false;
        CameraThree.enabled = false;
        CameraFour.enabled = false;
        CameraFive.enabled = false;
        CameraSix.enabled = false;
        CameraSeven.enabled = true;
    }
    public void ShowBoat()
    {
        CameraTwo.enabled = false;
        CameraOne.enabled = false;
        CameraBoat.enabled = true;
        CameraThree.enabled = false;
        CameraFour.enabled = false;
        CameraFive.enabled = false;
        CameraSix.enabled = false;
        CameraSeven.enabled = false;
    }
    public void HappyBirthday()
    {
        SceneManager.LoadScene("Birthday");
    }

}
