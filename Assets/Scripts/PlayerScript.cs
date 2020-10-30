using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerScript : MonoBehaviour
{
    #region Variables
    public float sensitivity;

    Camera cam;

    PlayerState state;
    UnityAction updateAction;
    #endregion

    #region Setters
    public void SetPlayerState(PlayerState newState)
    {
        state = newState;
        switch (state)
        {
            case PlayerState.normal:
                updateAction = delegate { PlayerRotation(GetMouseX()); };
                break;
            case PlayerState.showingUI:
                updateAction = null;
                break;
        }
    }
    #endregion

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
        SetPlayerState(PlayerState.normal);
    }

    #region UpdateMethods
    void FixedUpdate()
    {
        updateAction?.Invoke();
    }
    void PlayerRotation(float mouseX)
    {
        Vector3 rot = transform.eulerAngles;
        if (rot.y > 23) rot.y -= 360;

        rot.y += (mouseX * sensitivity);
        rot.y = Mathf.Clamp(rot.y, -23, 23);

        transform.eulerAngles = rot;
    }
    float GetMouseX()
    {
        float mousePosInScreen = cam.ScreenToViewportPoint(Input.mousePosition).x, result = 0;

        if (mousePosInScreen <= 0.23f) result = -1;
        else if (mousePosInScreen >= 0.77) result = 1;

        return result;
    }
    #endregion
}
public enum PlayerState
{
    normal,
    showingUI
}
