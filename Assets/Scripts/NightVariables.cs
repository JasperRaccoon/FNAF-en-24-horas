using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.LWRP;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class NightVariables : MonoBehaviour
{
    #region Variables
    int hour = 0, energy = 0, power = 0;
    public float powerDecreaseNum;

    public Slider energySlider;
    public Text hourText, powerText;
    public GameObject WinScreen;

    public VolumeProfile poweroutProf;
    Volume camVolume;

    AudioManager audioManager;
    LevelsManager levelsManager;
    #endregion

    #region Setters
    public void SetHour(int sumVal)
    {
        hour += sumVal;
        int displayHour = (hour == 0) ? 12 : hour;
        hourText.text = string.Format("{0} AM", displayHour);
        if (hour == 6)
        {
            StartCoroutine(Win());
        }
    }
    public void SetEnergy(int sumVal)
    {
        energy += sumVal;
        energySlider.value = energy;
    }
    public void SetPower(int sumVal)
    {
        power += sumVal;
        powerText.text = string.Format("Energia: {0}%", power);
        if (power == 0)
        {
            StartCoroutine(Powerout());
        }
    }
    #endregion

    #region StartUp
    private void Start()
    {
        levelsManager = FindObjectOfType<LevelsManager>();
        audioManager = FindObjectOfType<AudioManager>();
        camVolume = Camera.main.GetComponent<Volume>();

        audioManager.Play("TVstatic");
        audioManager.Play(levelsManager.GetNight().name);

        SetHour(0);
        SetEnergy(1);
        SetPower(100);
        StartCoroutine(PowerDecrease());
        StartCoroutine(Clock());
    }

    IEnumerator PowerDecrease()
    {
        while (true)
        {
            yield return new WaitForSeconds(powerDecreaseNum / energy);
            SetPower(-1);
            if (power == 0) break;
        }
    }
    IEnumerator Clock()
    {
        while (true)
        {
            yield return new WaitForSeconds(89f);
            SetHour(1);
            if (hour == 6) break;
        }
    }
    IEnumerator Win()
    {
        foreach (Animatronic animatronic in FindObjectsOfType<Animatronic>())
            animatronic.StopCoroutine(animatronic.PrepareForJumpscare());
        StopCoroutine(Powerout());
        StopCoroutine(Lose());

        WinScreen.SetActive(true);
        audioManager.Play("Win");
        yield return new WaitForSeconds(15);
        levelsManager.LoadNextNight();
    }
    IEnumerator Powerout()
    {
        audioManager.Play("Powerout");
        camVolume.profile = poweroutProf;
        DeactivateAllTasks();
        SetEnergy(-(int)energySlider.value);

        yield return new WaitForSeconds(10);

        audioManager.Play("MusicBox");
        float randomSecs = UnityEngine.Random.Range(14f, 20f);

        Freddy freddy = FindObjectOfType<Freddy>();
        StartCoroutine(freddy.PoweoutJumpscare(randomSecs));

        yield return new WaitForSeconds(randomSecs);

        audioManager.Stop("MusicBox");
    }
    void DeactivateAllTasks()
    {
        Activable[] activables = FindObjectsOfType<Activable>();
        foreach (Activable activable in activables)
        {
            if (activable.isActived)
            {
                if (!(string.IsNullOrEmpty(activable.soundName))) activable.audioMan.Stop(activable.soundName);
                activable.ChangeObjState();
            }
            activable.enabled = false;
            Destroy(activable);
        }
    }
    public IEnumerator Lose()
    {
        yield return new WaitForSeconds(3f);
        levelsManager.Lose();
    }
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) StartCoroutine(Win());
    }
}
