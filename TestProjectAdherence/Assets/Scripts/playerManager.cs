using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Notifications.Android;

public class playerManager : MonoBehaviour
{
    public int TESTINGINT;
    public float WAITTIMEFLOAT;

    public List<GameObject> comboInventButtons;
    public List<GameObject> plantInventButtons;
    public AudioSource output;
    [SerializeField] AudioClip growing;
    [SerializeField] AudioClip planting;
    [SerializeField] AudioClip firstgrowth;
    [SerializeField] AudioClip watering;
    [SerializeField] AudioClip harvesting;

    #region Alarm System UI
    [Header("Alarm System UI")]
    [SerializeField] GameObject AlarmObject;
    [SerializeField] List<GameObject> AlarmObjectsList;

    [SerializeField] GameObject ALARM_UI_CANVAS;
    [SerializeField] List<DateTime> dateTimes = new List<DateTime>();

    [SerializeField] Text hourInput;
    [SerializeField] Text minuteInput;
    [SerializeField] Text PMAMINPUT;
    #endregion

    #region Planting Icons
    [Header("Planting Icons")]
    public GameObject PotToAlter;
    public GameObject PlantToAdvance;
    public GameObject plantingIcon;
    public GameObject harvestingIcon;
    public GameObject growingIcon;
    [SerializeField] Transform plantingIconZero, harvestIconZero, growingIconZero;
    #endregion

    #region Growing Period Stuff
    [Header("Growing Period Activation Variables")]
    [SerializeField] bool GrowingPeriod;
    [SerializeField] bool buttonEnable = true;
    [SerializeField] String storedH = "StoredTimeHour", storedM= "StoredTMinute";
    [SerializeField] Button GrowingPeriodButton;
    [SerializeField] int PeriodLength;

    #endregion

    #region UI Panels
    [Header("List of Pot Objects")]
    public List<GameObject> POTSLIST = new List<GameObject>();

    [Header("Planting Canvas UI")]
    [SerializeField] GameObject INVENTORY_UI_CANVAS;
    
    [Header("Combination Canvas UI")]
    [SerializeField] GameObject COMBO_UI_CANVAS;

    [Header("Menu Canvas UI")]
    [SerializeField] GameObject MENU_UI_CANVAS;

    [Header("Med Canvas UI")]
    [SerializeField] GameObject MED_UI_CANVAS;

    [Header("Ask Canvas UI")]
    [SerializeField] GameObject ASK_UI_CANVAS;

    [Header("Clear Data Ask Canvas UI")]
    [SerializeField] GameObject CLEAR_UI_CANVAS;
    #endregion

    #region Logging Info Display Ui
    [Header("Logging Info Display UI")]
    [SerializeField] GameObject LOG_UI_CANVAS;
    [SerializeField]GameObject DisplayInfoPanel;
    [SerializeField] Text DisplayInfoText;
    [SerializeField] Text DisplayHour;
    [SerializeField] Text DisplayMinute;
    [SerializeField] Text MedNameText;
    [SerializeField] List<GameObject> MonthPanels;
    [SerializeField] GameObject MonthPanelsParent;

    #endregion
    

    [Header("Tutorial")]
    [SerializeField] TutorialBehaviour tutorialManager;

    public void TryProgessTutorial () {
        if (PlayerPrefs.GetString("Seen Tutorial") != "TRUE") {
            tutorialManager.StartNextPopup();
        }
    }

    //list of the species of flowers i.e "Daffodil"
    [Header("List of Flower Names")]
    public List<string> FlowerNames;
    
    //gizmo debugging
    Ray movingObjectRay;
    
    private static playerManager _instance;

    public static playerManager Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject go = new GameObject("playerManager");
                go.AddComponent<playerManager>();
            }
            return _instance;
        }
    }
    
    private void Awake()
    {
        
        _instance = this;
        if(PlayerPrefs.GetInt("DaffodilSeed")<1)
        {
            PlayerPrefs.SetInt("DaffodilSeed", 1);
        }
    }
    
    private void FixedUpdate()
    {
        TouchManagement();
        foreach(GameObject button in comboInventButtons)
        {
            if(PlayerPrefs.GetInt(button.name+"Flower")>=1)
            {
                button.GetComponentInChildren<Text>().text = PlayerPrefs.GetInt(button.name + "Flower").ToString();
                button.SetActive(true);
                
            }
            else if (PlayerPrefs.GetInt(button.name + "Flower") < 1)
            {
                button.GetComponentInChildren<Text>().text = PlayerPrefs.GetInt(button.name + "Flower").ToString();
                button.SetActive(false);
                
            }
        }
        foreach (GameObject button in plantInventButtons)
        {
            if (PlayerPrefs.GetInt(button.name + "Seed") >= 1)
            {
                button.SetActive(true);
                button.GetComponentInChildren<Text>().text = PlayerPrefs.GetInt(button.name + "Seed").ToString();
            }
            else if (PlayerPrefs.GetInt(button.name + "Seed") < 1)
            {
                button.SetActive(false);
                button.GetComponentInChildren<Text>().text = PlayerPrefs.GetInt(button.name + "Seed").ToString();
            }
        }
    }

    void TouchManagement()
    {
        if (Input.touchCount > 0)
        {
            RaycastHit2D RAYHIT;
            RAYHIT = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), transform.forward);
            
            if (RAYHIT.collider != null)
            {
                if (RAYHIT.collider.tag == "Moving")
                {
                    
                    GameObject go = RAYHIT.collider.gameObject;
                    //go.transform.position = Vector3.Lerp(go.transform.position, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) + new Vector3(0, 0, 5), 0.5f);
                    go.transform.position = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) + new Vector3(0, 0, 5);
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        //touchStartTime = Time.time;
                    }

                    if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        //go.transform.position = Vector3.Lerp(go.transform.position, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) + new Vector3(0, 0, 5), 0.5f);
                    }

                    if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        activatequestion();
                        switch (go.name)
                        {
                            case ("Planting Icon"):
                                go.transform.position = Vector3.Lerp(go.transform.position, plantingIconZero.position, 2f * Time.deltaTime);
                                if(PotToAlter.gameObject.GetComponent<potScript>().isPlanted==true)
                                {
                                    Debug.Log("Already planted");
                                }
                                if (PotToAlter != null && 
                                    go.gameObject.GetComponentInChildren<detectionScript>(QueryTriggerInteraction.Collide.Equals(gameObject.tag == "FlowerPot"))==true
                                    && PotToAlter.gameObject.GetComponent<potScript>().isPlanted == false)
                                {
                                    output.clip = planting;
                                    output.Play();
                                    INVENTORY_UI_CANVAS.SetActive(true);
                                    
                                }
                                break;

                            case ("Harvest Icon"):
                                go.transform.position = Vector3.Lerp(go.transform.position, harvestIconZero.position, 2f * Time.deltaTime);
                                if (PlantToAdvance != null && PlantToAdvance.GetComponent<plantItem>().growthStage>1)
                                {
                                    output.clip = harvesting;
                                    output.Play();
                                    //Commented out to test the animations
                                    //PlantToAdvance.GetComponent<plantItem>().AdvanceStage();

                                    PlantToAdvance.GetComponent<plantItem>().anim.SetTrigger("Harvest");

                                   
                                }
                                break;

                            case ("Growing Icon"):
                                go.transform.position = Vector3.Lerp(go.transform.position, growingIconZero.position, 2f * Time.deltaTime);
                                if (PlantToAdvance != null && PlantToAdvance.GetComponent<plantItem>().growthStage < 2 
                                    && PlayerPrefs.GetInt(PlantToAdvance.GetComponent<plantItem>().potParent.name+PlantToAdvance.GetComponent<plantItem>().PLANTNAME+"GrownToday") - TESTINGINT!=DateTime.Today.Day) //<-----------testing int is here
                                {
                                    foreach (GameObject AlarmObject in AlarmObjectsList)
                                    {
                                        output.clip = watering;
                                        output.Play();
                                            Debug.Log("Grow Connected to Player Pref Time");
                                            PlayerPrefs.SetInt(PlantToAdvance.GetComponent<plantItem>().potParent.name + PlantToAdvance.GetComponent<plantItem>().PLANTNAME + "GrownToday", DateTime.Today.Day);

                                        //Commented out to test the animations
                                        //PlantToAdvance.GetComponent<plantItem>().AdvanceStage();

                                            PlantToAdvance.GetComponent<plantItem>().anim.SetTrigger("Grow");
                                        output.clip = firstgrowth;
                                        output.Play();

                                            TryProgessTutorial();
                                        
                                    }
                                    
                                }
                                break;
                        }
                    }
                }
            }
        }
        else if(plantingIcon!=null&&harvestingIcon!=null&&growingIcon!=null)
        {
            plantingIcon.transform.position = Vector3.Lerp(plantingIcon.transform.position, plantingIconZero.position, 2f);
            harvestingIcon.transform.position = Vector3.Lerp(harvestingIcon.transform.position, harvestIconZero.position, 2f );
            growingIcon.transform.position = Vector3.Lerp(growingIcon.transform.position, growingIconZero.position, 2f);
        }
    }

    void activatequestion()
    {
        if(PlayerPrefs.GetString("PillTakenToday"+DateTime.Today.Day)!="True")
        {
            
            if (PlayerPrefs.GetInt("answerHour") > 0 && PlayerPrefs.GetInt("answerMinute") > 0)
            {
                Debug.Log("Got this far");
                TimeSpan TS = new DateTime(DateTime.Now.Year,
                                    DateTime.Now.Month,
                                    DateTime.Now.Day,
                                    PlayerPrefs.GetInt("answerHour"),
                                    PlayerPrefs.GetInt("answerMinute"),
                                    PlayerPrefs.GetInt("answerSecond"))
                                    - DateTime.Now;
                
                if (Mathf.Abs((float)TS.TotalSeconds)> WAITTIMEFLOAT)
                {
                    Debug.Log("Got this far");
                    ASK_UI_CANVAS.SetActive(true);
                }
            }
            else
            {
                ASK_UI_CANVAS.SetActive(true);
            }
        }
    }
    
    public void yes_no(Button buttonName)
    {
        
        if (buttonName.name == "Yes"|| buttonName.name == "No")
        {
            
            PlayerPrefs.SetInt("answerHour", DateTime.Now.Hour);
            PlayerPrefs.SetInt("answerMinute", DateTime.Now.Minute);
            PlayerPrefs.SetInt("answerSecond", DateTime.Now.Second);
            if(buttonName.name == "Yes")
            {
                PlayerPrefs.SetString("PillTakenToday" + DateTime.Today.Day, "True");
            }
            if (buttonName.name == "No")
            {
                PlayerPrefs.SetString("PillTakenToday" + DateTime.Today.Day, "sdfsfs");
            }
            buttonName.gameObject.transform.parent.gameObject.SetActive(false);
        }

    }

    DateTime AddnewDateTime(Text minute, Text hour, Text pmam)
    {
        DateTime dateTimeToAdd = new DateTime();
        if (int.TryParse(minute.text, out int x) && int.TryParse(hour.text, out int xx))
        {
            if (pmam.text == "PM")
            {
                if (xx == 12)
                {
                    dateTimeToAdd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, x, DateTime.Now.Second);
                    return dateTimeToAdd;
                }
                else
                {
                    dateTimeToAdd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, xx+12, x, DateTime.Now.Second);
                    return dateTimeToAdd;
                }
            }
            else
            {
                dateTimeToAdd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, xx, x, DateTime.Now.Second);
                return dateTimeToAdd;
            }
        } 
        else
        {
            return DateTime.Now;
        }
    }

    public void AddDateTimeToList()
    {
        if (minuteInput != null && hourInput != null)
        {

            dateTimes.Add(AddnewDateTime(minuteInput, hourInput, PMAMINPUT));

            // <-----------------------------------------------------------------------------daffodil seed added when the alarm is added, just for testing
            
            // <-----------------------------------------------------------------------------

            for (int i = 0; i< dateTimes.Count; i++)
            {
               //Debug.Log(dateTimes[i]);
            }
        }
    }
    
    //planting a plant
    public void ChoosePlantToPot(Button theButtonClicked)
    {
        var PotObjectScript = PotToAlter.GetComponent<potScript>();
        PotObjectScript.SpawnNewPlant(theButtonClicked.name);
        PotObjectScript.containsFlower = theButtonClicked.name;
        output.clip = firstgrowth;
        output.Play();
        INVENTORY_UI_CANVAS.SetActive(false);
    }
    
    //opening up ui menus
    public void ChoosePanelToEnable(Button theButtonClicked)
    {
        //opening alarm setting menu
        if(theButtonClicked.name == ALARM_UI_CANVAS.name + " Button")
        {
            //ALARM_UI_CANVAS.SetActive(true);
            //MENU_UI_CANVAS.SetActive(false);
            ALARM_UI_CANVAS.GetComponent<MenuPanel>().OpenPanel();
            MENU_UI_CANVAS.GetComponent<MenuPanel>().ClosePanel();
        }
        if (theButtonClicked.name == ALARM_UI_CANVAS.name + " Close Button")
        {
            //ALARM_UI_CANVAS.SetActive(false);
            ALARM_UI_CANVAS.GetComponent<MenuPanel>().ClosePanel();
        }

        if(theButtonClicked.name == "Inventory Close")
        {
            INVENTORY_UI_CANVAS.SetActive(false);
        }

        //opening combination menu
        if (theButtonClicked.name == COMBO_UI_CANVAS.name + " Button")
        {
            //COMBO_UI_CANVAS.SetActive(true);
            COMBO_UI_CANVAS.GetComponent<MenuPanel>().OpenPanel();
        }
        if (theButtonClicked.name == COMBO_UI_CANVAS.name + " Close Button")
        {
            //COMBO_UI_CANVAS.SetActive(false);
            COMBO_UI_CANVAS.GetComponent<MenuPanel>().ClosePanel();
        }

        //opening menu screen
        if (theButtonClicked.name == MENU_UI_CANVAS.name + " Button")
        {
            //MENU_UI_CANVAS.SetActive(true);
            MENU_UI_CANVAS.GetComponent<MenuPanel>().OpenPanel();
        }

        if (theButtonClicked.name == MENU_UI_CANVAS.name + " Close Button")
        {
            //MENU_UI_CANVAS.SetActive(false);
            MENU_UI_CANVAS.GetComponent<MenuPanel>().ClosePanel();
        }


        //opening log panel
        if (theButtonClicked.name == LOG_UI_CANVAS.name + " Button")
        {
            LOG_UI_CANVAS.GetComponent<MenuPanel>().OpenPanel();
        }

        //opening medication panel
        if (theButtonClicked.name == MED_UI_CANVAS.name + " Button")
        {
            //MED_UI_CANVAS.SetActive(true);
            MED_UI_CANVAS.GetComponent<MenuPanel>().OpenPanel();
        }

        if(theButtonClicked.name == "DisplayInfoPanelCloseButton")
        {
            theButtonClicked.gameObject.transform.parent.gameObject.SetActive(false);
        }


        if (theButtonClicked.name=="CloseButton")
        {
            MenuPanel panel = theButtonClicked.gameObject.GetComponentInParent<MenuPanel>();

            if (panel == null) {
                theButtonClicked.gameObject.transform.parent.gameObject.SetActive(false);
            } else {
                panel.ClosePanel();
            }
        }


    }


    // Growing Period Stuff
    void GrowingPeriodTimeChecking()
    {
        //checking current time against the stored time
        DateTime RelockerTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                                       DateTime.Now.Day, PlayerPrefs.GetInt(storedH), PlayerPrefs.GetInt(storedM),
                                       DateTime.Now.Second);
        TimeSpan RelockerTS = RelockerTime - DateTime.Now;

        //Debug.Log(Mathf.Abs(RelockerTS.Minutes));
        if (Mathf.Abs((float)RelockerTS.TotalMinutes) > PeriodLength)
        {
            

            #region Temporary Code (Unused)
            /*
            foreach (GameObject AlarmGO in AlarmObjectsList)
            {
                DateTime UnlockerTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                                           DateTime.Now.Day, PlayerPrefs.GetInt(AlarmGO.name + "Hour"), PlayerPrefs.GetInt(AlarmGO.name + "Minute"),
                                           DateTime.Now.Second);

                TimeSpan UnlockerTS = UnlockerTime - DateTime.Now;
                if (Mathf.Abs(UnlockerTS.Minutes) > PeriodLength)
                {
                    GrowingPeriodButton.gameObject.SetActive(true);
                    GrowingPeriod = false;
                }
                /*
                else
                {
                    buttonEnable = false;
                    GrowingPeriodButton.gameObject.SetActive(false);
                }
                
            }
            
            buttonEnable = true;
            Debug.Log("Relocker time greater than Period Length");
            GrowingPeriodButton.gameObject.SetActive(true);
            */
            #endregion 
        }

        #region Temporary Code (Unused)
        //checking current time against the alarm times
        /*
        foreach (GameObject AlarmGO in AlarmObjectsList)
        {
            DateTime UnlockerTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                                       DateTime.Now.Day, PlayerPrefs.GetInt(AlarmGO.name + "Hour"), PlayerPrefs.GetInt(AlarmGO.name + "Minute"),
                                       DateTime.Now.Second);

            TimeSpan UnlockerTS = UnlockerTime - DateTime.Now;
            if (Mathf.Abs(UnlockerTS.Minutes) <= 60 && buttonEnable == true)
            {
                GrowingPeriodButton.gameObject.SetActive(true);
            }
            if (buttonEnable == false)
            {

                GrowingPeriodButton.gameObject.SetActive(false);
            }
        }
        */
        #endregion
    }

    //Button For Activating Period
    public void ActivateGrowingPeriod(Button GPButton)
    {
        foreach (GameObject AlarmGO in AlarmObjectsList)
        {
            DateTime UnlockerTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                                       DateTime.Now.Day, PlayerPrefs.GetInt(AlarmGO.name + "Hour"), PlayerPrefs.GetInt(AlarmGO.name + "Minute"),
                                       DateTime.Now.Second);
            TimeSpan UnlockerTS = UnlockerTime - DateTime.Now;
            if (Mathf.Abs((float)UnlockerTS.TotalMinutes) <= 60)
            {
                GrowingPeriod = true;
                PlayerPrefs.SetInt(storedH, DateTime.Now.Hour);// <---------------------setting the stored time as player prefs to do comparison on app restart
                PlayerPrefs.SetInt(storedM, DateTime.Now.Minute);
                PlayerPrefs.Save();
                GPButton.gameObject.SetActive(false);
                buttonEnable = false;
                AndroidNotification newNot = new AndroidNotification();
                newNot.Title = "Growing Period has Activated!";
                newNot.Text = "Growing Period has Activated! You have an hour to grow your plants!";
                newNot.FireTime = DateTime.Now.AddSeconds(2);
                AndroidNotificationCenter.SendNotification(newNot, default);
                
                
            }
            
        }

    }
    

    // UI Button Activation for Logging Information
    //first is closing/opening month
    public void LoggingPanelMonthSelect(Button monthButton)
    {
        for(int i =0; i<MonthPanels.Count; i++)
        {
            if(monthButton.gameObject.name == MonthPanels[i].gameObject.name) //<--------------------testing using only November (monthButton.gameObject.name=="January")
            {
                MonthPanelsParent.SetActive(true);
                MonthPanels[i].SetActive(true);
            }
            if (monthButton.gameObject.name == MonthPanels[i].gameObject.name+"CloseButton")
            {
                MonthPanels[i].SetActive(false);
            }
        }
        
    }
    //second is opening/closing day and info
    public void chooseDayInfoToDisplay(Button daySelected)
    {
        string month = daySelected.gameObject.transform.parent.name;
        string day = daySelected.name;
        
        if(PlayerPrefs.GetString(month + day + "TAKEN") != "TRUE") // PlayerPrefs.GetInt(month + day + "Hour").ToString() == "0" && PlayerPrefs.GetString("January" + DateTime.Today.Day + "CHECKEDPMAM") != "PM" ||
        {
            DisplayInfoText.text = "Prescription Was Not Taken";
            DisplayHour.text = "";
            DisplayMinute.text = "";
        }
        else
        {
            int displayHour = PlayerPrefs.GetInt(month + day + "Hour");
            int displayMin = PlayerPrefs.GetInt(month + day + "Minute");
            string displayStringHour=displayHour.ToString();
            string displayStringMinute = displayMin.ToString();
            
            if(displayHour<10)
            {
                displayStringHour = "0" + displayHour.ToString();
            }
            if(displayMin<10)
            {
                displayStringMinute = "0" + displayMin.ToString();
            }
            DisplayInfoText.text = "Prescription taken at: ";
            
                DisplayHour.text = displayStringHour+"  :";
                DisplayMinute.text = displayStringMinute;
            
        }

        DisplayInfoPanel.SetActive(true);
    }

    //The Logging Button(Not Attached to Time)
    public void PillTakenButtonFunction()
    {
        Debug.Log("pressed log button");
        switch (DateTime.Today.Month)
        {
            case 1:
                PlayerPrefs.SetInt("January" + DateTime.Today.Day + "Hour", DateTime.Now.Hour);
                PlayerPrefs.Save();
                PlayerPrefs.SetInt("January" + DateTime.Today.Day + "Minute", DateTime.Now.Minute);
                PlayerPrefs.Save();
                if (DateTime.Now.Hour < 1)
                {
                    PlayerPrefs.SetString("January" + DateTime.Today.Day + "CHECKEDPMAM", "PM");
                }
                PlayerPrefs.SetString("January" + DateTime.Today.Day + "TAKEN", "TRUE");
                PlayerPrefs.Save();
                break;
            case 2:
                PlayerPrefs.SetInt("February" + DateTime.Today.Day + "Hour", DateTime.Now.Hour);
                PlayerPrefs.Save();
                PlayerPrefs.SetInt("February" + DateTime.Today.Day + "Minute", DateTime.Now.Minute);
                PlayerPrefs.Save();
                if (DateTime.Now.Hour < 1)
                {
                    PlayerPrefs.SetString("February" + DateTime.Today.Day + "CHECKEDPMAM", "PM");
                }
                PlayerPrefs.SetString("February" + DateTime.Today.Day + "TAKEN", "TRUE");
                PlayerPrefs.Save();
                break;
            case 3:
                PlayerPrefs.SetInt("March" + DateTime.Today.Day + "Hour", DateTime.Now.Hour);
                PlayerPrefs.Save();
                PlayerPrefs.SetInt("March" + DateTime.Today.Day + "Minute", DateTime.Now.Minute);
                PlayerPrefs.Save();
                if (DateTime.Now.Hour < 1)
                {
                    PlayerPrefs.SetString("March" + DateTime.Today.Day + "CHECKEDPMAM", "PM");
                }
                PlayerPrefs.SetString("March" + DateTime.Today.Day + "TAKEN", "TRUE");
                PlayerPrefs.Save();
                break;
            case 4:
                PlayerPrefs.SetInt("April" + DateTime.Today.Day + "Hour", DateTime.Now.Hour);
                PlayerPrefs.Save();
                PlayerPrefs.SetInt("April" + DateTime.Today.Day + "Minute", DateTime.Now.Minute);
                PlayerPrefs.Save();
                if (DateTime.Now.Hour < 1)
                {
                    PlayerPrefs.SetString("April" + DateTime.Today.Day + "CHECKEDPMAM", "PM");
                }
                PlayerPrefs.SetString("April" + DateTime.Today.Day + "TAKEN", "TRUE");
                PlayerPrefs.Save();
                break;
            case 5:
                PlayerPrefs.SetInt("May" + DateTime.Today.Day + "Hour", DateTime.Now.Hour);
                PlayerPrefs.Save();
                PlayerPrefs.SetInt("May" + DateTime.Today.Day + "Minute", DateTime.Now.Minute);
                PlayerPrefs.Save();
                if (DateTime.Now.Hour < 1)
                {
                    PlayerPrefs.SetString("May" + DateTime.Today.Day + "CHECKEDPMAM", "PM");
                }
                PlayerPrefs.SetString("May" + DateTime.Today.Day + "TAKEN", "TRUE");
                PlayerPrefs.Save();
                break;
            case 6:
                PlayerPrefs.SetInt("June" + DateTime.Today.Day + "Hour", DateTime.Now.Hour);
                PlayerPrefs.Save();
                PlayerPrefs.SetInt("June" + DateTime.Today.Day + "Minute", DateTime.Now.Minute);
                PlayerPrefs.Save();
                if (DateTime.Now.Hour < 1)
                {
                    PlayerPrefs.SetString("June" + DateTime.Today.Day + "CHECKEDPMAM", "PM");
                }
                PlayerPrefs.SetString("June" + DateTime.Today.Day + "TAKEN", "TRUE");
                PlayerPrefs.Save();
                break;
            case 7:
                PlayerPrefs.SetInt("July" + DateTime.Today.Day + "Hour", DateTime.Now.Hour);
                PlayerPrefs.Save();
                PlayerPrefs.SetInt("July" + DateTime.Today.Day + "Minute", DateTime.Now.Minute);
                PlayerPrefs.Save();
                if (DateTime.Now.Hour < 1)
                {
                    PlayerPrefs.SetString("July" + DateTime.Today.Day + "CHECKEDPMAM", "PM");
                }
                PlayerPrefs.SetString("July" + DateTime.Today.Day + "TAKEN", "TRUE");
                PlayerPrefs.Save();
                break;
            case 8:
                PlayerPrefs.SetInt("August" + DateTime.Today.Day + "Hour", DateTime.Now.Hour);
                PlayerPrefs.Save();
                PlayerPrefs.SetInt("August" + DateTime.Today.Day + "Minute", DateTime.Now.Minute);
                PlayerPrefs.Save();
                if (DateTime.Now.Hour < 1)
                {
                    PlayerPrefs.SetString("August" + DateTime.Today.Day + "CHECKEDPMAM", "PM");
                }
                PlayerPrefs.SetString("August" + DateTime.Today.Day + "TAKEN", "TRUE");
                PlayerPrefs.Save();
                break;
            case 9:
                PlayerPrefs.SetInt("September" + DateTime.Today.Day + "Hour", DateTime.Now.Hour);
                PlayerPrefs.Save();
                PlayerPrefs.SetInt("September" + DateTime.Today.Day + "Minute", DateTime.Now.Minute);
                PlayerPrefs.Save();
                if (DateTime.Now.Hour < 1)
                {
                    PlayerPrefs.SetString("September" + DateTime.Today.Day + "CHECKEDPMAM", "PM");
                }
                PlayerPrefs.SetString("September" + DateTime.Today.Day + "TAKEN", "TRUE");
                PlayerPrefs.Save();
                break;
            case 10:
                PlayerPrefs.SetInt("October" + DateTime.Today.Day + "Hour", DateTime.Now.Hour);
                PlayerPrefs.Save();
                PlayerPrefs.SetInt("October" + DateTime.Today.Day + "Minute", DateTime.Now.Minute);
                PlayerPrefs.Save();
                if (DateTime.Now.Hour < 1)
                {
                    PlayerPrefs.SetString("October" + DateTime.Today.Day + "CHECKEDPMAM", "PM");
                }
                PlayerPrefs.SetString("October" + DateTime.Today.Day + "TAKEN", "TRUE");
                PlayerPrefs.Save();
                break;
            case 11:
                PlayerPrefs.SetInt("November" + DateTime.Today.Day + "Hour", DateTime.Now.Hour);
                PlayerPrefs.Save();
                PlayerPrefs.SetInt("November" + DateTime.Today.Day + "Minute", DateTime.Now.Minute);
                PlayerPrefs.Save();
                if (DateTime.Now.Hour < 1)
                {
                    PlayerPrefs.SetString("November" + DateTime.Today.Day + "CHECKEDPMAM", "PM");
                }
                PlayerPrefs.SetString("November" + DateTime.Today.Day + "TAKEN", "TRUE");
                PlayerPrefs.Save();
                break;
            case 12:
                PlayerPrefs.SetInt("December" + DateTime.Today.Day + "Hour", DateTime.Now.Hour);
                PlayerPrefs.Save();
                PlayerPrefs.SetInt("December" + DateTime.Today.Day + "Minute", DateTime.Now.Minute);
                PlayerPrefs.Save();
                if (DateTime.Now.Hour < 1)
                {
                    PlayerPrefs.SetString("December" + DateTime.Today.Day + "CHECKEDPMAM", "PM");
                }
                PlayerPrefs.SetString("December" + DateTime.Today.Day + "TAKEN", "TRUE");
                PlayerPrefs.Save();
                break;

        }
    }

    public void openAskPanel()
    {
        ASK_UI_CANVAS.SetActive(true);
    }
    public void openClearDataPanel()
    {
        CLEAR_UI_CANVAS.SetActive(true);
    }
    //clearing player data button
    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
        Caching.ClearCache();
        PlayerPrefs.DeleteAll();

        Application.Quit();
    }

    public void SaveAndQuitApp()
    {
        foreach (GameObject pot in POTSLIST)
        {
            pot.GetComponent<potScript>().saveData();
        }
    }
    private void OnApplicationPause(bool pause)
    {
        PlayerPrefs.Save();
    }

    public void OptionPanelEnable(GameObject paneltoenable)
    {
        paneltoenable.SetActive(true);
    }

    public void SetTestingInt(int newInt) {
        TESTINGINT = newInt;
    }

}
