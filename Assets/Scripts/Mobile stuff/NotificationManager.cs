using UnityEngine;
using Unity.Notifications.Android;
using UnityEngine.Android;
using System;

/*Prerequisites Mobile Notifications package 2.3.1
 
Target Android 13 (API level 33) or higher in your Project Settings for runtime notification permission requests to be necessary.

Ensure the POST_NOTIFICATIONS permission is added to your Android Manifest, which Unity typically handles if the Mobile Notifications package is used.

            Check the generated Manifest: After building your project (but before closing the Unity Editor), you can examine the final Android Manifest in the Temp/StagingArea/AndroidManifest.xml file.

            Add manually (if needed): If the permission is missing, you can provide a custom AndroidManifest.xml file in your project at Assets/Plugins/Android/AndroidManifest.xml. The permission should be declared as a child of the <manifest> tag:

                <manifest ...>
                    <uses-permission android:name="android.permission.POST_NOTIFICATIONS" />
                    <application ...>
                        ...
                    </application>
                </manifest>

Install the Mobile Notifications package (Window > Package Manager > Unity Registry > Mobile Notifications). 
*/

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance { get; private set; }

    private AndroidNotificationChannel _notificationChannel;

    private int _comebackID;

    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        if (PlayerPrefs.HasKey("Display_ComeBack"))
        {
            _comebackID = PlayerPrefs.GetInt("Display_ComeBack");
            CancelNotification(_comebackID);
        }
    }

    private void OnEnable()
    {
        RequestNotificationPermission();
    }

    private void RequestNotificationPermission()
    {
        string notificationPermission = "android.permission.POST_NOTIFICATIONS";

        if (!Permission.HasUserAuthorizedPermission(notificationPermission))
        {
            PermissionCallbacks callback = new PermissionCallbacks();

            callback.PermissionGranted += NotifPermissions;

            Permission.RequestUserPermission(notificationPermission, callback);
        }
        else
        {
            NotifPermissions("Allowed");
        }
    }

    void NotifPermissions(string permission)
    {
        _notificationChannel = new AndroidNotificationChannel()
        {
            Id = "reminder_notif_ch",
            Name = "Reminder Notification",
            Description = "Reminder to login",
            Importance = Importance.High,
            EnableLights = true,
            EnableVibration = true
        };

        AndroidNotificationCenter.RegisterNotificationChannel(_notificationChannel);

        PlayerPrefs.SetInt("Display_ComeBack", DisplayNotification("It's been a while!", "It's been a long time since you last played, why don't you come back?", IconSelector.icon_0, IconSelector.icon_1, DateTime.Now.AddHours(24)));

        Debug.Log(AndroidNotificationCenter.CheckScheduledNotificationStatus(PlayerPrefs.GetInt("Display_ComeBack")));
    }

    public int DisplayNotification(string title, string text, IconSelector iconSmall, IconSelector iconLarge, DateTime fireTime)
    {
        var notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = text;
        notification.SmallIcon = iconSmall.ToString();
        notification.LargeIcon = iconLarge.ToString();
        notification.FireTime = fireTime;

        return AndroidNotificationCenter.SendNotification(notification, _notificationChannel.Id);
    }

    public void CancelNotification(int id)
    {
        AndroidNotificationCenter.CancelNotification(id);
    }

    private void OnApplicationQuit()
    {
        _comebackID = DisplayNotification("It's been a while!", "It's been a long time since you last played, why don't you come back?", IconSelector.icon_0, IconSelector.icon_1, DateTime.Now.AddHours(24));
        PlayerPrefs.SetInt("Display_ComeBack", _comebackID);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus) return;
        _comebackID = DisplayNotification("It's been a while!", "It's been a long time since you last played, why don't you come back?", IconSelector.icon_0, IconSelector.icon_1, DateTime.Now.AddHours(24));
        PlayerPrefs.SetInt("Display_ComeBack", _comebackID);
    }

}



public enum IconSelector
{
    icon_0,
    icon_1
}
