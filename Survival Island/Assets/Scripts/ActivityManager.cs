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

        player.playerData.currentLocationName = "Lager";
        player.playerData.currentActivityName = "Nichts";

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

                //Check for tool stability if a tool is equiped
                if (player.playerData.tool != null)
                {
                    if (player.playerData.tool.getCurrentStability() <= 0)
                    {
                        player.playerData.tool.resetStability();
                        player.inventory.subItem(player.playerData.tool);
                        player.playerData.tool = null;
                    }
                }

                //Am lager chillen
                player.playerData.currentLocationName = "Lager";
                player.playerData.currentActivityName = "Nichts";

                durationBar.fillAmount = 1;

                player.save();
            }
        }
        currentLocationText.text = player.playerData.currentLocationName + ": " + player.playerData.currentActivityName;
    }

    public void setActivity(Activity activity)
    {
        if (!activeActivity)
        {
            if (player.playerData.ap >= activity.activityPoints)
            {
                if (player.checkEquipptedTool(activity))
                {
                    currentActivity = activity;
                    currentLocation = currentActivity.currentLocation;
                    player.playerData.ap -= currentActivity.activityPoints;
                    workingTime = currentActivity.workingTime;
                    activeActivity = true;

                    player.playerData.currentLocationName = currentLocation.locationName;
                    player.playerData.currentActivityName = currentActivity.activityName;

                    totalTime = workingTime;

                    if (player.playerData.ap > player.playerData.apMAX)
                    {
                        player.playerData.ap = player.playerData.apMAX;
                    }

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
