using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monitor : MonoBehaviour
{
    #region variables
    AudioManager audioMan;
    public RawImage display;
    public GameObject noCamToShowAdvice;
    public GameObject[] camArray;
    public Button[] buttons;
    public RenderTexture[] renders;
    public Text locationDisplay;
    public string[] locationNames;
    #endregion

    #region startup
    private void Start()
    {
        audioMan = FindObjectOfType<AudioManager>();

        for (int i = 0; i < buttons.Length; i++)
        {
            Button button = buttons[i];

            GameObject camera = GetCamera(button);
            RenderTexture render = GetRenderTexture(button);
            bool isCamEnabled = !button.gameObject.name.Equals("6");
            string locName = locationNames[i];

            CamInfo buttonCamInfo = CreateCamInfo(render, camera, isCamEnabled, locName);

            SetButton(buttonCamInfo, button);
        }
        buttons[0].ClickButton();
    }
    GameObject GetCamera(Button desiredButton)
    {
        foreach (GameObject cam in camArray)
        {
            if (cam.name.Equals(desiredButton.gameObject.name)) return cam;
        }
        return null;
    }
    RenderTexture GetRenderTexture(Button desiredButton)
    {
        foreach (RenderTexture render in renders)
        {
            if (render.name.Equals(desiredButton.gameObject.name)) return render;
        }
        return null;
    }
    CamInfo CreateCamInfo(RenderTexture content, GameObject camObj, bool isCam, string loc)
    {
        return new CamInfo { contentToDisplay = content, camObject = camObj, isCamEnabled = isCam, locationName = loc };
    }
    void SetButton(CamInfo camInf, Button button)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(delegate { SelectCamToShow(camInf); });
    }
    #endregion

    #region functionalities
    public void SelectCamToShow(CamInfo camInfo)
    {
        audioMan.Play("Monitor");
        switch (camInfo.isCamEnabled)
        {
            case true:
                DisableAllCameras(camInfo.camObject);
                SetDisplay(new Color32(255, 255, 255, 255), camInfo.contentToDisplay, camInfo.locationName);
                noCamToShowAdvice.SetActive(false);
                break;
            case false:
                DisableAllCameras(null);
                SetDisplay(new Color32(0, 0, 0, 255), null, camInfo.locationName);
                noCamToShowAdvice.SetActive(true);
                break;
        }
    }
    void SetDisplay(Color32 colorToShow, RenderTexture renderTex, string locationName)
    {
        display.texture = renderTex;
        display.color = colorToShow;
        locationDisplay.text = locationName;
    }
    void DisableAllCameras(GameObject exception)
    {
        foreach (GameObject camera in camArray)
        {
            if (exception != null)
            {
                camera.SetActive(camera == exception);
                continue;
            }
            camera.SetActive(false);
        }
    }
    #endregion
}

[System.Serializable]
public struct CamInfo
{
    public RenderTexture contentToDisplay;
    public bool isCamEnabled;
    public string locationName;
    public GameObject camObject;
}

