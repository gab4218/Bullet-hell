using System;
using System.Collections;
using TMPro;
using UnityEngine;

public enum DateTimeLocalizationSelector
{
    Local, UtcLoc
}

public class StaminaManager : MonoBehaviour
{

    [SerializeField] private int _maxStamina = 5;
    public int currentStamina;
    [SerializeField] private float _timePerStamina = 300f;
    [SerializeField] private DateTimeLocalizationSelector _localization;
    [SerializeField] private GameObject _adButton;
    [SerializeField] private TMP_Text _staminaText, _timerText;
    [SerializeField] private string _notifTitle, _notifText;
    [SerializeField] IconSelector _smallIcon = IconSelector.icon_0;
    [SerializeField] IconSelector _largeIcon = IconSelector.icon_1;

    private bool _charging;

    private DateTime _nextTime, _lastTime;

    public static StaminaManager instance;

    private int _staminaID;
    private int _christmasID;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        LoadData();
        StartCoroutine(ChargeStamina());
    }

    private IEnumerator ChargeStamina()
    {
        _charging = true;
        UpdateTexts();
        _adButton.SetActive(true);
        while (currentStamina < _maxStamina)
        {
            DateTime currentTime = Localizator(_localization);
            DateTime nextTime = _nextTime;

            bool staminaToAdd = false;

            while (currentTime > nextTime)
            {
                if (currentStamina >= _maxStamina) break;

                currentStamina++;

                staminaToAdd = true;

                UpdateTexts();

                DateTime timeToAdd = nextTime;

                if (_lastTime > nextTime)
                    timeToAdd = _lastTime;

                nextTime = AddTime(timeToAdd, _timePerStamina);
            }

            if (staminaToAdd == true)
            {
                _nextTime = nextTime;
                _lastTime = currentTime;
            }

            UpdateTexts();
            SaveData();

            yield return null;
        }
        _charging = false;
        UpdateTexts();
        SaveData();
        _adButton.SetActive(false);
    }


    private void UpdateTexts()
    {

        if (currentStamina >= _maxStamina)
        {
            _timerText.text = "Full";
        }
        else
        {

            TimeSpan time = _nextTime - Localizator(_localization);

            _timerText.text = $"{time.Minutes.ToString("00")}:{time.Seconds.ToString("00")}";
        }

        _staminaText.text = $"{currentStamina}/{_maxStamina}";
    }


    public bool UseStamina()
    {
        if (currentStamina - 1 >= 0)
        {
            currentStamina--;


            //NotificationManager.Instance.CancelNotification(_staminaID);
            DisplayNotification();
            UpdateTexts();

            if (!_charging)
            {
                _nextTime = AddTime(Localizator(_localization), _timePerStamina);
                StartCoroutine(ChargeStamina());
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddStamina(int s)
    {
        currentStamina = Mathf.Min(_maxStamina, currentStamina + s);
        _nextTime = AddTime(Localizator(_localization), _timePerStamina);
        StartCoroutine(ChargeStamina());
        UpdateTexts();
    }

    void DisplayNotification()
    {
        TimeSpan time = _nextTime - Localizator(_localization);
        float calc = _maxStamina - (currentStamina + 1) * _timePerStamina + (float)time.TotalSeconds + 1;
        DateTime fireTime = AddTime(Localizator(_localization), calc);
        //_staminaID = NotificationManager.Instance.DisplayNotification(_notifTitle, _notifText, _smallIcon, _largeIcon, fireTime);
    }

    DateTime AddTime(DateTime time, float add) => time.AddSeconds(add);

    private DateTime Localizator(DateTimeLocalizationSelector val)
    {
        switch (val)
        {
            case DateTimeLocalizationSelector.Local:
                return DateTime.Now;
            case DateTimeLocalizationSelector.UtcLoc:
                return DateTime.UtcNow;
            default:
                return DateTime.UtcNow;
        }
    }

    void SaveData()
    {
        var c = new DateTime(Localizator(_localization).Year, 12, 24, 12, 0, 0);
        TimeSpan t = c - Localizator(_localization);
        //_christmasID = NotificationManager.Instance.DisplayNotification("It's Christmas!", "Come join us in our special Christmas event!", IconSelector.icon_0, IconSelector.icon_1, Localizator(_localization).AddHours(t.Hours));
        PlayerPrefs.SetInt("currentStamina", currentStamina);
        PlayerPrefs.SetInt("notifStamina", _staminaID);
        PlayerPrefs.SetInt("notifChristmas", _christmasID);
        PlayerPrefs.SetString("nextStamina", _nextTime.ToString());
        PlayerPrefs.SetString("lastStamina", _lastTime.ToString());
    }

    void LoadData()
    {
        currentStamina = PlayerPrefs.GetInt("currentStamina", _maxStamina);
        if (PlayerPrefs.HasKey("notifStamina")) _staminaID = PlayerPrefs.GetInt("notifStamina");
        if (PlayerPrefs.HasKey("notifChristmas")) _christmasID = PlayerPrefs.GetInt("notifChristmas");
        _nextTime = StringToDateTime(PlayerPrefs.GetString("nextStamina"));
        _lastTime = StringToDateTime(PlayerPrefs.GetString("lastStamina"));
        //NotificationManager.Instance.CancelNotification(_christmasID);
    }

    DateTime StringToDateTime(string data)
    {
        if (string.IsNullOrEmpty(data))
            return Localizator(_localization);
        else
            return DateTime.Parse(data);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus) SaveData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
