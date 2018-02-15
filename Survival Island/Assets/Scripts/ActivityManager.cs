using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivityManager : MonoBehaviour {

    [SerializeField]
    private Player player;

    private bool activeActivity;

    private Location currentLocation;
    private Activity currentActivity;

    private EventManager eventManager;
    private GameEvent currentEvent;

    private float workingTime;
    private float totalTime;

    public PopUpWindowManager popUpWindow;
    public Image durationBar;
    public Text currentLocationText;

    private void Start () {

        activeActivity = false;
        workingTime = 0;
        eventManager = new EventManager();

        player.setCurrentLocationName("Lager");
        player.setCurrentActivityName("Nichts");

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

                if (currentLocation.getLocationName() != "Lager")
                {
                    player.collectMaterials(currentActivity, currentLocation);

                    //Check if event occured
                    if (eventManager.checkForEvent())
                    {
                        currentEvent = eventManager.chooseEvent(currentLocation);

                        if (currentEvent != null)
                        {
                            //Eventwindow
                            Debug.Log(currentEvent);
                            //currentEvent.g
                            currentEvent.run(player);
                            popUpWindow.createNotificationWindow(currentEvent.getTitle(), createEventText(currentEvent));

                            
                        }
                    }
                } else
                {
                    //Anzeigen, dass man sich ausgeruht hat.
                    player.setAp(+10);
                    popUpWindow.createNotificationWindow("Ausgeruht!", "Du fuhlst dich ausgeruht.\nDeine Aktionspunkte wurde aufgefüllt!");
                }
                player.checkToolStability();

                //Am Lager verweilen
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
            if (player.getAp() >= activity.getAp())
            {
                if (player.checkEquipptedTool(activity))
                {
                    currentActivity = activity;
                    currentLocation = currentActivity.currentLocation;
                    player.setAp(-currentActivity.getAp());
                    workingTime = currentActivity.getWorkingTime();
                    activeActivity = true;

                    player.setCurrentLocationName(currentLocation.getLocationName());
                    player.setCurrentActivityName(currentActivity.getActivityName());

                    totalTime = workingTime;
                }

                else
                {
                    string title = "Achtung!";
                    string description = "Nicht das benötigte Werkzeug ausgerüstet. Du brauchst ein/e \n\n" + activity.getNeededTool().getItemName();
                    popUpWindow.createNotificationWindow(title, description);
                }
            }
            else
            {
                string title = "Achtung";
                string description = "Leider nicht genug Aktionspunkte zur Verfügung!";
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

    private string createEventText(GameEvent theEvent)
    {
        string eventDescription = currentEvent.getDescription() ;
        string textEnd = "";

        if (currentEvent.GetType() == typeof(PlayerEvent))
        {
            eventDescription += "\nDu hast\n";
            if (currentEvent.GetComponent<PlayerEvent>().getHealtPoints() != 0)
            {
                eventDescription += "\n" + currentEvent.GetComponent<PlayerEvent>().getHealtPoints() + " HP";
                if (currentEvent.GetComponent<PlayerEvent>().getHealtPoints() < 0)
                    textEnd = "\n\nverloren";
                else
                {
                    textEnd = "\n\ngewonnen";
                }
            }
            if (currentEvent.GetComponent<PlayerEvent>().getActivityPoints() != 0)
            {
                eventDescription += "\n" + currentEvent.GetComponent<PlayerEvent>().getActivityPoints() + " AP";
                if (currentEvent.GetComponent<PlayerEvent>().getActivityPoints() < 0)
                    textEnd = "\n\nverloren";
                else
                {
                    textEnd = "\n\ngewonnen";
                }
            }

            if (currentEvent.GetComponent<PlayerEvent>().getFoodPoints() != 0)
            {
                eventDescription += "\n" + currentEvent.GetComponent<PlayerEvent>().getFoodPoints() + " FP";
                if (currentEvent.GetComponent<PlayerEvent>().getFoodPoints() < 0)
                    textEnd = "\n\nverloren";
                else
                {
                    textEnd = "\n\ngewonnen";
                }
            }
            eventDescription += textEnd;

        }
        else if (currentEvent.GetType() == typeof(BattleEvent))
        {
            eventDescription += "\nDu hast\n\n-" + currentEvent.GetComponent<BattleEvent>().getTotalDamage() + " HP";
            textEnd += "\n\nan Schaden erlitten";

            eventDescription += textEnd;
        }
        else if (currentEvent.GetType() == typeof(ItemEvent))
        {

            eventDescription += "\n\n"+currentEvent.GetComponent<ItemEvent>().getGivenItem().getItemName() +"\n";
            textEnd = "\n wurde deinem Inventar hinzugefügt!";

            eventDescription += textEnd;
        }

        return eventDescription;
    }
}
