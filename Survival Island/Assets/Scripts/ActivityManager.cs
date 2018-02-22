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

    public PopUpWindow popUpWindow;
    public Image durationBar;
    public Text currentLocationText;

    private void Start ()
    {
        activeActivity = false;
        workingTime = 0;
        eventManager = new EventManager();

        player.setCurrentLocationName("Lager");
        player.setCurrentActivityName("Nichts");

        currentLocationText.text = player.getCurrentLocationName() + ": " + player.getCurrentActivityName();

        durationBar.fillAmount = 1;
    }
	
	private void Update ()
    {
        if (activeActivity)
        {
            workingTime -= Time.deltaTime;

            //Show duration of activity in UI
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
                            currentEvent.run(player);
                            popUpWindow.createNotificationWindow(currentEvent.getTitle(), createEventText(currentEvent));                          
                        }
                    }

                    player.checkToolStability();

                } else
                {
                    //Sleeping and recovering AP
                    player.setAp((int)player.getApMax());
                    popUpWindow.createNotificationWindow("Ausgeruht!", "Du fuhlst dich ausgeruht.\nDeine Aktionspunkte wurde aufgefüllt!");
                }

                //No current activity
                player.setCurrentLocationName("Lager");
                player.setCurrentActivityName("Nichts");

                //Displaying in UI
                currentLocationText.text = player.getCurrentLocationName() + ": " + player.getCurrentActivityName();

                durationBar.fillAmount = 1;
                player.save();
            }
        }

    }

    public void setActivity(Activity activity)
    {
        //If player is not doing any other activity
        if (!activeActivity)
        {
            if (player.getAp() >= activity.getNeededAP())
            {
                //Check if tool, which is neccessarry for activity, is equiped
                if (player.checkEquipptedTool(activity))
                {
                    currentActivity = activity;
                    currentLocation = currentActivity.currentLocation;
                    player.setAp(-currentActivity.getNeededAP());
                    workingTime = currentActivity.getWorkingTime();
                    activeActivity = true;

                    player.setCurrentLocationName(currentLocation.getLocationName());
                    player.setCurrentActivityName(currentActivity.getActivityName());

                    totalTime = workingTime;

                    //Displaying in UI
                    currentLocationText.text = player.getCurrentLocationName() + ": " + player.getCurrentActivityName();
                }
                else
                {
                    string title = "Achtung!";
                    string description = "Nicht das benötigte Werkzeug ausgerüstet. Du brauchst ein/e \n\n" + activity.getNeededTool();
                    popUpWindow.createNotificationWindow(title, description);
                }
            }
            else
            {
                string title = "Achtung!";
                string description = "Leider nicht genug Aktionspunkte zur Verfügung!";
                popUpWindow.createNotificationWindow(title, description);

            }
        }
        else
        {
            string title = "Achtung!";
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

            //Effects on HP
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
            //Effects on AP
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
            //Effects on FP
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
