using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject OnScreenControls;

    public static bool jumpButtonDown;
    // Start is called before the first frame update
    void Start()
    {
        CheckPlatform();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckPlatform()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
            case RuntimePlatform.IPhonePlayer:
            case RuntimePlatform.WindowsEditor:
                OnScreenControls.SetActive(true);
                break;
            default:
                OnScreenControls.SetActive(false);
                break;
        }
    }

    public void OnJumpButton_Down()
    {
        jumpButtonDown = true;
    }

    public void OnJumpButton_Up()
    {
        jumpButtonDown = false;
    }
}
