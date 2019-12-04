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
    int weekvalue;
    int min;
    int hour;
    int [] dayident = new int[7];
    AndroidNotification a,b,c,d,e,f,g;
    
   

    public AndroidNotificationChannel mon = new AndroidNotificationChannel()
    {
        Id = "monday_id",
        Name = "monday Channel",
        Importance = Importance.High,
        Description = "monday notifications",
        CanBypassDnd = true,
    };

   

    public void Start()
    {
        weekvalue = (int)System.DateTime.Now.DayOfWeek;
        Debug.Log(weekvalue);
        timeflood();
        alarmflood();
        timewatch();

    }

    

    public void Awake()
    {
        AndroidNotificationCenter.RegisterNotificationChannel(mon);
       
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
    
    void alarmflood() 
    {
        a = new AndroidNotification();
        a.Title = "Take the pill yo";
        a.Text = "this Irwin yo ready to help";
      
        

        b = new AndroidNotification();
        b.Title = "Take the pill we bang ok";
        b.Text = "im commander shepard and i approve this message";
        

        c = new AndroidNotification();
        c.Title = "Take the pill";
        c.Text = "Experience the true mans world";
        


        d = new AndroidNotification();
        d.Title = "GRIFFFFFFFITHHHHH!!!!!!";
        d.Text = "GRIIIIIIIIIIIFFFFFFFFFIIIIIITH";
        


        e = new AndroidNotification();
        e.Title = "DUN DUN DUN DUN";
        e.Text = "another one takes the pill";
        


        f = new AndroidNotification();
        f.Title = "Depression is pretty sus bro";
        f.Text = "-Eoin McSharry";
        


  
        g = new AndroidNotification();
        g.Title = "Bruh";
        g.Text = "-Aaron Kennedy";
        

    }

    void timewatch()
    {
        for(int i = 0; i < 7; i++)
        {
          dayindex[i]=i-weekvalue;
            if (i < weekvalue)
            {
                dayindex[i] +=7;
            }

            dayindex[i] = min + dayindex[i];

            if (dayindex[i]> 60)
            {
                dayindex[i] = min + dayindex[i] - 60;
                
            }
            dayindex[i] = dayindex[i];
        }
        
    }

   public void onclick()
    {
        int pmcheck=0;
        if (alarmpicked == true)
        {
            AndroidNotificationCenter.CancelAllScheduledNotifications();
        }

        alarmpicked = true;
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
        for(int i =0; i < 7; i++)
        {
            notify[i] = new System.DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day, hour, dayindex[i], 00);
        }
        

        a.FireTime = notify[0];
        b.FireTime = notify[1];
        c.FireTime = notify[2];
        d.FireTime = notify[3];
        e.FireTime = notify[4];
        f.FireTime = notify[5];
        g.FireTime = notify[6];

        if (sunday == true)
        {
            dayident[0] = AndroidNotificationCenter.SendNotification(a, mon.Id);
        }
        if (monday == true)
        {
            dayident[1] = AndroidNotificationCenter.SendNotification(b, mon.Id);
        }
        if (tuesday == true)
        {
            dayident[2] = AndroidNotificationCenter.SendNotification(c, mon.Id);
        }
        if (wednesday == true)
        {
            dayident[3] = AndroidNotificationCenter.SendNotification(d, mon.Id);
        }
        if (thursday == true)
        {
            dayident[4] = AndroidNotificationCenter.SendNotification(e, mon.Id);
        }
        if (friday == true)
        {
            dayident[5] = AndroidNotificationCenter.SendNotification(f, mon.Id);
        }
        if (saturday == true)
        {
            dayident[6] = AndroidNotificationCenter.SendNotification(g, mon.Id);
        }
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

   public void mondayclick()
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
    }

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
