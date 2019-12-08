using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using UnityEngine.UI;
public class tabsnotificationtestingstuff : MonoBehaviour
{
    [SerializeField] List<System.DateTime> notify = new List<System.DateTime>();
    bool monday, tuesday, wednesday, thursday, friday, saturday, sunday;
    [SerializeField] Text txtmin;
    [SerializeField] Text txthour;
    [SerializeField] Text twelvecheck;
    public bool alarmpicked = false;
    int[] dayindex = new int[7];
    int[] monthindex = new int[7];
    int[] yearindex = new int[7];
    int weekvalue;
    int min;
    int hour;
    int startup;
    bool bypass;
    int [] dayident = new int[7];
    AndroidNotification a,b,c,d,e,f,g;
    public AudioSource output;
    [SerializeField] AudioClip alarmset;
   

    public AndroidNotificationChannel mon = new AndroidNotificationChannel()
    {
        Id = "monday_id",
        Name = "monday Channel",
        Importance = Importance.High,
        Description = "monday notifications",
        CanBypassDnd = true,
    };
    public void Awake()
    {
        AndroidNotificationCenter.RegisterNotificationChannel(mon);
    }


    public void Start()
    {
       
        PlayerPrefs.GetInt("startup");
        startup = PlayerPrefs.GetInt("startup");
        PlayerPrefs.GetString("switch");
        weekvalue = (int)System.DateTime.Now.DayOfWeek;
        timeflood();
        alarmflood();
        timewatch();
        bypass = true;
        //Debug.Log(PlayerPrefs.GetInt("startup"));
        if (startup == 0)
        {
            
            PlayerPrefs.SetInt("startup", 1);
            
        }
        else
        {
            alarmpicked = bool.Parse(PlayerPrefs.GetString("switch"));
            
        }
        
    }

    public void Update()
    {
        
        //Debug.Log(startup);
        
        
        if(bypass == true)
        {
            alarmredo();
            bypass = false;
        }
      

    }

    void alarmredo()
    {
        if (alarmpicked == true)
        {
            
            min = PlayerPrefs.GetInt("minsave");
            hour = PlayerPrefs.GetInt("hoursave");
            
            AndroidNotificationCenter.CancelAllNotifications();

            for (int i = 0; i < 7; i++)
            {
                notify[i] = new System.DateTime(yearindex[i],monthindex[i], dayindex[i], hour, min,00);
                
            }

            a.FireTime = notify[0];
            b.FireTime = notify[1];
            c.FireTime = notify[2];
            d.FireTime = notify[3];
            e.FireTime = notify[4];
            f.FireTime = notify[5];
            g.FireTime = notify[6];

            dayident[0] = AndroidNotificationCenter.SendNotification(a, mon.Id);


            dayident[1] = AndroidNotificationCenter.SendNotification(b, mon.Id);


            dayident[2] = AndroidNotificationCenter.SendNotification(c, mon.Id);


            dayident[3] = AndroidNotificationCenter.SendNotification(d, mon.Id);


            dayident[4] = AndroidNotificationCenter.SendNotification(e, mon.Id);


            dayident[5] = AndroidNotificationCenter.SendNotification(f, mon.Id);


            dayident[6] = AndroidNotificationCenter.SendNotification(g, mon.Id);
        }
    }

    void alarmsystemflood()
    {
        
        a.FireTime = notify[0];
        b.FireTime = notify[1];
        c.FireTime = notify[2];
        d.FireTime = notify[3];
        e.FireTime = notify[4];
        f.FireTime = notify[5];
        g.FireTime = notify[6];
       dayident[0]= AndroidNotificationCenter.SendNotification(a, mon.Id);
       dayident[1]= AndroidNotificationCenter.SendNotification(b, mon.Id);
       dayident[2]= AndroidNotificationCenter.SendNotification(c, mon.Id);
       dayident[3]= AndroidNotificationCenter.SendNotification(d, mon.Id);
       dayident[4]= AndroidNotificationCenter.SendNotification(e, mon.Id);
       dayident[5]= AndroidNotificationCenter.SendNotification(f, mon.Id);
       dayident[6]= AndroidNotificationCenter.SendNotification(g, mon.Id);
    }

    void timeflood()
    {
        if (alarmpicked == false)
        {
            System.DateTime z = new System.DateTime();
            notify.Add(z);
            System.DateTime x = new System.DateTime();
            notify.Add(x);
            System.DateTime y = new System.DateTime();
            notify.Add(y);
            System.DateTime w = new System.DateTime();
            notify.Add(w);
            System.DateTime v = new System.DateTime();
            notify.Add(v);
            System.DateTime u = new System.DateTime();
            notify.Add(u);
            System.DateTime t = new System.DateTime();
            notify.Add(t);
        }
    }
    
    void alarmflood() 
    {
        a = new AndroidNotification();
        a.Title = "Have you watered your plants today?";
        a.Text = "You should check up on your terrarium!";
      
        

        b = new AndroidNotification();
        b.Title = "Have you watered your plants today?";
        b.Text = "You should check up on your terrarium!";
        

        c = new AndroidNotification();
        c.Title = "Have you watered your plants today?";
        c.Text = "You should check up on your terrarium!";



        d = new AndroidNotification();
        d.Title = "Have you watered your plants today?";
        d.Text = "You should check up on your terrarium!";



        e = new AndroidNotification();
        e.Title = "Have you watered your plants today?";
        e.Text = "You should check up on your terrarium!";



        f = new AndroidNotification();
        f.Title = "Have you watered your plants today?";
        f.Text = "You should check up on your terrarium!";




        g = new AndroidNotification();
        g.Title = "Have you watered your plants today?";
        g.Text = "You should check up on your terrarium!";


    }

    void timewatch()
    {
        for(int i = 0; i < 7; i++)
        {
          monthindex[i] = System.DateTime.Now.Month;
            yearindex[i] = System.DateTime.Now.Year;
          dayindex[i]=i-weekvalue;
            if (i < weekvalue)
            {
                dayindex[i] +=7;
            }


            dayindex[i] = dayindex[i] + System.DateTime.Now.Day;
            if (dayindex[i]> System.DateTime.DaysInMonth(System.DateTime.Now.Year,System.DateTime.Now.Month))
            {
                dayindex[i] =  dayindex[i] - System.DateTime.DaysInMonth(System.DateTime.Now.Year, System.DateTime.Now.Month);
                monthindex[i] += 1;
            }

            if (monthindex[i]> 12)
            {
                monthindex[i] = monthindex[i] - 12;
                yearindex[i] += 1;
            }
            dayindex[i] = dayindex[i];
        }
        
    }

   public void onclick()
    {
        int pmcheck=0;
        Debug.Log("yup");
        if (alarmpicked == true)
        {
            
            AndroidNotificationCenter.CancelAllNotifications();
        }

        alarmpicked = true;
        PlayerPrefs.SetString("switch", alarmpicked.ToString());
        
      if(twelvecheck.text == "PM" && txthour.text != "12")
        {
            pmcheck = 12;
        }

        if(twelvecheck.text == "AM" && txthour.text == "12")
        {
            pmcheck = 12;
        }

       min = int.Parse(txtmin.text);
       hour = int.Parse(txthour.text)+pmcheck;
       // min = System.DateTime.Now.Minute;
       // hour = System.DateTime.Now.Hour;
       
        for (int i =0; i < 7; i++)
        {
            notify[i] = new System.DateTime(yearindex[i], monthindex[i],dayindex[i], hour, min,00);
            Debug.Log(notify[i]);
        }
        

        a.FireTime = notify[0];
        b.FireTime = notify[1];
        c.FireTime = notify[2];
        d.FireTime = notify[3];
        e.FireTime = notify[4];
        f.FireTime = notify[5];
        g.FireTime = notify[6];

        
            dayident[0] = AndroidNotificationCenter.SendNotification(a, mon.Id);
        
        
            dayident[1] = AndroidNotificationCenter.SendNotification(b, mon.Id);
        
        
            dayident[2] = AndroidNotificationCenter.SendNotification(c, mon.Id);
        
       
            dayident[3] = AndroidNotificationCenter.SendNotification(d, mon.Id);
        
      
            dayident[4] = AndroidNotificationCenter.SendNotification(e, mon.Id);
        
       
            dayident[5] = AndroidNotificationCenter.SendNotification(f, mon.Id);
        
        
            dayident[6] = AndroidNotificationCenter.SendNotification(g, mon.Id);
        PlayerPrefs.SetInt("minsave", min);
        PlayerPrefs.SetInt("hoursave", hour);
        output.clip = alarmset;
        output.Play();

    }

    public void OnApplicationPause(bool pause)
    {   if (alarmpicked == true)
        {
            foreach(int i in dayident)
            {
                if (AndroidNotificationCenter.CheckScheduledNotificationStatus(dayident[i]) == NotificationStatus.Delivered)
                {
                    AndroidNotificationCenter.CancelNotification(i);
                }
                

            }
            
        }
    }

   /*public void mondayclick()
    {
        if (!monday)
        {
            monday = true;
        }
        else
        {
            monday = false;
        }
    }

   public void tuesdayclick()
    {
        if (!tuesday)
        {
            tuesday = true;
        }
        else
        {
            tuesday = false;
        }
    }

   public void wednesdayclick()
    {
        if (!wednesday)
        {
            wednesday = true;
        }
        else
        {
            wednesday = false;
        }
    }

   public void thursdayclick()
    {
        if (!thursday)
        {
            thursday = true;
        }
        else
        {
            thursday = false;
        }
    }

   public void fridayclick()
    {
        if (!friday)
        {
            friday = true;
        }
        else
        {
            friday = false;
        }
    }

   public void saturdayclick()
    {
        if (!saturday)
        {
            saturday = true;
        }
        else
        {
            saturday = false;
        }
    }

   public void sundayclick()
    {
        if (!sunday)
        {
            sunday = true;
        }
        else
        {
            sunday = false;
        }
    }*/

    /* void calculation()
     {
         int year = System.DateTime.Now.Year;
         int backyears = int.Parse(System.DateTime.Now.ToString("yy"));
         int centuary = year - backyears;
         int ankcal = centuary / 100;
         var ankind = ankcal % 4;
         int ank=0;
         switch (ankind)
         {
             case 0:
                 ank = 2;
                 break;
             case 1:
                 ank = 0;
                 break;
             case 2:
                 ank = 5;
                 break;
             case 3:
                 ank = 3;
                 break;
         }
         int divisionvalue = Mathf.Abs(backyears / 12);
         var remainder = backyears % 12;
         var absremainder = Mathf.Abs(remainder / 4);
         int addition = ank + divisionvalue + remainder + absremainder;
          doomsdaynum = addition %7;

     }

     void weekcalculations()
     {
        int month =int.Parse(System.DateTime.Now.ToString("MM"));

         for(int i = 0; i < doomsdays.Length; i++)
         {
             if(i == month - 1)
             {

                 doomnum = doomsdays[i];

                 if (i < 2)
                 {
                     doomnum = doomsdays[i] + leapcheck;
                 }
             }
         }

     }

     void daycalculations()
     {
         int date = int.Parse(System.DateTime.Now.ToString("dd"));
         int value = date- doomnum;
         //int divvalue = Mathf.Abs(value / 7);
         int remvalue = value % 7;
         int dayvalue =Mathf.Abs( 7-remvalue);
         weekvalue = doomsdaynum - dayvalue;
         if (weekvalue < 0)
         {
             weekvalue = 7 + weekvalue;
         }
         Debug.Log(weekvalue);
     }*/
}
