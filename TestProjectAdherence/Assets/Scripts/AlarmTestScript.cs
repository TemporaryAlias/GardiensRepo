using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Unity.Notifications.Android;
using UnityEngine.UI;

[System.Serializable]
public struct userAlarmTime
{
    public int year;
    public int month;
    public int day;
    public int hour;
    public int minute;
    public int second;
}

[System.Serializable]
public struct userAlarmsListStruct
{
    public List<userAlarmTime> listofTimes;
    public userAlarmsListStruct(List<userAlarmTime> preMadeList)
    {
        listofTimes = preMadeList;
    }
}


public class AlarmTestScript : MonoBehaviour
{

    // input fields are serialized text objects
    #region Text Input fields
    //Testing Input of the Alarm Script
    [SerializeField] Text yearInput;
    [SerializeField] Text monthInput;
    [SerializeField] Text dayInput;
    [SerializeField] Text hourInput;
    [SerializeField] Text PMAMInput;
    [SerializeField] Text minuteInput;
    [SerializeField] Button setAlarm; //button to activate function
    [SerializeField] Text medicationName;
    [SerializeField] Text medicationNamePH;
    [SerializeField] Text medicationDosage;

    #endregion
    //List of Notifications to check through
    [SerializeField] List<AndroidNotification> notificationsList; // <-------------------------------------- make a list of notifications

    //Creating a New Channel for sending notifications
    public AndroidNotificationChannel PublicChannel = new AndroidNotificationChannel()
    {
        Id = "Channel_O",
        Name = "OisinChannel",
        Importance = Importance.Default,
        Description = "Oisins Notification Channel Test"
    };


    //On Awake we are registering the notification channel
    private void Awake()
    {
        var hourInputDropdown = hourInput.GetComponentInParent<Dropdown>();
        var minuteInputDropdown = minuteInput.GetComponentInParent<Dropdown>();
        var PMAMInputDropdown = PMAMInput.GetComponentInParent<Dropdown>();
        var DosageDropdown = medicationDosage.GetComponentInParent<Dropdown>();

       
        if (PlayerPrefs.GetInt(this.gameObject.name + "Hour")>12)
        {
            hourInputDropdown.value = hourInputDropdown.options.FindIndex(option => option.text == (PlayerPrefs.GetInt(this.gameObject.name + "Hour")-12).ToString());
            
        }
        else
        {
            hourInputDropdown.value = hourInputDropdown.options.FindIndex(option => option.text == PlayerPrefs.GetInt(this.gameObject.name + "Hour").ToString());
        }
        Debug.Log(PlayerPrefs.GetInt(this.gameObject.name + "Hour"));
        minuteInputDropdown.value = minuteInputDropdown.options.FindIndex(option => option.text == PlayerPrefs.GetInt(this.gameObject.name + "Minute").ToString());
        PMAMInputDropdown.value = PMAMInputDropdown.options.FindIndex(option => option.text == PlayerPrefs.GetString(this.gameObject.name + "PMAM"));
        medicationName.text = PlayerPrefs.GetString(this.gameObject.name + "PRESCRIPTION");
        medicationNamePH.text = PlayerPrefs.GetString(this.gameObject.name + "PRESCRIPTION");
        DosageDropdown.value = DosageDropdown.options.FindIndex(option => option.text == PlayerPrefs.GetString(this.gameObject.name + "DOSAGE"));

        AndroidNotificationCenter.RegisterNotificationChannel(PublicChannel);
        notificationsList = new List<AndroidNotification>();
    }

    void Update()
    {
        for (int i =0; i<notificationsList.Count; i++)
        {
            

        }
    }

    // method that returns a new android notification using multiple inputs for the date and the medication name and dosage
    private AndroidNotification newNotification(int year, int month, int day, int hour, int minute, int second, string name, string text)
    {
        var notification = new AndroidNotification();
        notification.Title = name;
        notification.Text = text;
        DateTime fireTime = new DateTime(year, month, day, hour, minute, 00);
        notification.FireTime = fireTime;
        notification.ShouldAutoCancel=true;
        TimeSpan delay = new TimeSpan(24,00,00);
        notification.RepeatInterval = delay;
        return notification;
    }


    public userAlarmTime SetNotification(Text year, Text month, Text day, Text hour, Text minute, Text medName, Text medDosage)
    {
        userAlarmTime returnedAlarmTime = new userAlarmTime();
        //returnedAlarmTime.year = int.Parse(year.text);
        returnedAlarmTime.year = DateTime.Today.Date.Year;
        //returnedAlarmTime.month = int.Parse(month.text);
        returnedAlarmTime.month = DateTime.Today.Date.Month;
        //returnedAlarmTime.day = int.Parse(day.text);
        returnedAlarmTime.day = DateTime.Today.Date.Day;

        returnedAlarmTime.hour = int.Parse(hour.text);
        returnedAlarmTime.minute = int.Parse(minute.text);
        if (PMAMInput != null)
        {
            if (PMAMInput.text == "PM")
            {
                if (hourInput.text == "12")
                {
                    returnedAlarmTime.hour = 0;
                }
                else
                {
                    returnedAlarmTime.hour += 12;
                }
            }
        }

        return returnedAlarmTime;
    }

    public void QueueNewNotification()
    {
        if (int.TryParse(hourInput.text, out int xxxx) && int.TryParse(minuteInput.text, out int xxxxx))
        {


            AndroidNotification newNot = newNotification(SetNotification(yearInput, monthInput, dayInput, hourInput, minuteInput, null, null).year,
                                             SetNotification(yearInput, monthInput, dayInput, hourInput, minuteInput, null, null).month,
                                             SetNotification(yearInput, monthInput, dayInput, hourInput, minuteInput, null, null).day,
                                             SetNotification(yearInput, monthInput, dayInput, hourInput, minuteInput, null, null).hour,
                                             SetNotification(yearInput, monthInput, dayInput, hourInput, minuteInput, null, null).minute,
                                             00,
                                             "You should check up on your Terrarium!",
                                             DateTime.Now.ToString());

            AndroidNotificationCenter.SendNotification(newNot, PublicChannel.Id);
            notificationsList.Add(newNot);
        }
    }


    public void ModifyPlayerPrefAlarmHourMinute()
    {
        if (int.TryParse(hourInput.text, out int xx) && int.TryParse(minuteInput.text, out int x))
        {
            if(PMAMInput.text == "PM")
            {
                if (hourInput.text == "12")
                {
                    xx = 0;
                }
                else
                {
                    xx += 12;
                }
            }
            PlayerPrefs.SetInt(this.gameObject.name + "Hour", xx);
            PlayerPrefs.Save();
            PlayerPrefs.SetInt(this.gameObject.name + "Minute", x);
            PlayerPrefs.Save();
            PlayerPrefs.SetString(this.gameObject.name + "PMAM", PMAMInput.text);
            PlayerPrefs.Save();
            if (medicationName.text != null)
            {
                PlayerPrefs.SetString(this.gameObject.name + "PRESCRIPTION", medicationName.text);
                PlayerPrefs.Save();
            }

            PlayerPrefs.SetString(this.gameObject.name + "DOSAGE", medicationDosage.text);
            PlayerPrefs.Save();
        }
    }
}
