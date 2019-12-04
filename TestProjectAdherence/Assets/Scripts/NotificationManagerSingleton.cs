using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManagerSingleton : MonoBehaviour
{
    public static NotificationManagerSingleton InstancePublicStat
    {
        get
        {
            if(InstancePrivateStat!=null)
            {
                return InstancePrivateStat;
            }
            InstancePrivateStat = FindObjectOfType<NotificationManagerSingleton>();
            if(InstancePrivateStat!=null)
            {
                return InstancePrivateStat;
            }
            CreateNewInstance();
            return InstancePrivateStat;
        }
    }

    public static NotificationManagerSingleton CreateNewInstance()
    {
        NotificationManagerSingleton NMSPrefab = Resources.Load<NotificationManagerSingleton>("NotificationManager");
        InstancePrivateStat = Instantiate(NMSPrefab);
        return InstancePrivateStat;
    }

    private static NotificationManagerSingleton InstancePrivateStat;

    private void Awake()
    {
        if(InstancePublicStat!=this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
        
    }
}
