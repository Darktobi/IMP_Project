using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivityManager : MonoBehaviour {

    private bool activeActivity;

    private Location currentLocation;
    private Activity currentActivity;

    private EventManager eventManager;
    private GameEvent currentEvent;

    private float workingTime;
    private float totalTime;

    public Player player;
    public PopUpWindowManager popUpWindow;
    public Image durationBar;
    public Text currentLocationText;

    void Start () {

        activeActivity = false;
        workingTime = 0;
        eventManager = new EventManager();

        player.setCurrentLocationName("Lager");
        player .setCurrentActivityName("Nichts");

        durationBar.fillAmount = 1;
    }
	
	void Update () {

        if (activeActivity)
        {
            workingTime -= Time.deltaTime;

            //show duration in UI
            durationBar.fillAmount = workingTime / totalTime;

            if (workingTime <= 0)
            {
                activeActivity = false;
                workingTime = 0;

                player.collectMaterials(currentActivity, currentLocation);

                if (currentLocation.locationName != "Lager")
                {
                    //Check if event occured
                    if (eventManager.checkForEvent())
                    {
                        currentEvent = eventManager.chooseEvent(currentLocation);

                        if (currentEvent != null)
                        {
                            Debug.Log(currentEvent.title);
                            Debug.Log(currentEvent.description);

                            //Eventwindow
                            popUpWindow.createNotificationWindow(currentEvent.title, currentEvent.description);

                            currentEvent.run(player);
                        }
                    }
                }
                player.checkToolStability();

                //Am lager chillen
                player.setCurrentLocationName("Lager");
                player.setCurrentActivityName("Nichts");

                durationBar.fillAmount = 1;
                player.save();
            }
        }
        currentLocationText.text = player.getCurrentLocationName() + ": " + player.getCurrentActivityName();
    }

    public void setActivity(Activity activity)
    {
        if (!activeActivity)
        {
            if (player.getActivityPoints() >= activity.activityPoints)
            {
                if (player.checkEquipptedTool(activity))
                {
                    currentActivity = activity;
                    currentLocation = currentActivity.currentLocation;
                    player.setActivityPoints(-currentActivity.activityPoints);
                    workingTime = currentActivity.workingTime;
                    activeActivity = true;

                    player.setCurrentLocationName(currentLocation.locationName);
                    player.setCurrentActivityName(currentActivity.activityName);

                    totalTime = workingTime;
                }

                else
                {
                    string title = "Achtung!";
                    string description = "Nicht das benötigte Werkzeug ausgerüstet. Du brauchst ein/e \n\n" + activity.neededTool.name;
                    popUpWindow.createNotificationWindow(title, description);
                }
            }
            else
            {
                string title = "Achtung";
                string description = "Leider nicht genug Aktivitätspunkte zur Verfügung";
                popUpWindow.createNotificationWindow(title, description);

            }

        }
        else
        {
            string title = "Achtung";
            string description = "Es wird noch eine andere Tätigkeit ausgeführt!";
            popUpWindow.createNotificationWindow(title, description);

        }
    }
}
