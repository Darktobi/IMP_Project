using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour {

    //private const int MAX_HEALTH = 100;
    private float health;
    private float activityPoints;
    private bool isWorking;
    private int str, con, agi, wis, counter;
    private Location currentLocation;
    private Activity currentWork;
    private MaterialManager materialManager;
    private EventManager eventManager;
    private GameEvent currentEvent;

    public Equipment weapon, head, breast, hands, legs, feet;
    public Tool tool;

    public Inventory inventory;
    public InventoryHandler inventoryHandler;
    public PlayerDatas playerData;
    public Text currentLocationText;
    public Image durationBar;
    public PopUpWindowManager popUpWindow;

    //NotificationWindow
    //public GameObject notiWindow;
    //public Text eventTitle;
    //public Text eventText;

    //ItemDiscriptionWindow
    public GameObject descrWindow;
    public Text descrTitle;
    public Text descrText;
    public Button confirmBtn;

    //EQ-Slots in UI in Character Display
    public Text[] eqSlots;

    private float workingTime;
    private float totalTime;

    public int foodDownSpeed;

	// Use this for initialization
	void Start () {
        health = playerData.health;
        activityPoints = playerData.ap;
        PlayerPrefs.SetFloat("foodValue", health);

        //eqSlots = new Text[5];

        workingTime = 0;
        counter = 0;
        isWorking = false;
        materialManager = new MaterialManager();
        eventManager = new EventManager();

        playerData.currentLocationName = "Lager";
        playerData.currentActivityName = "Nichts";


        durationBar.fillAmount = 1;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp(KeyCode.S))
        {
            save();
        }


        if (isWorking)
        {
            workingTime -= Time.deltaTime;

            //in der Leiste darstellen
            durationBar.fillAmount = workingTime/totalTime;

            if (workingTime <= 0)
            {
                isWorking = false;
                workingTime = 0;

                collectMaterials();

                if(currentLocation.locationName != "Lager")
                {
                    //Check if event occured
                    if (eventManager.checkForEvent())
                    {
                        Debug.Log("Event ist eingetreten");
                        currentEvent = eventManager.chooseEvent(currentLocation);

                        if(currentEvent != null)
                        {
                            Debug.Log(currentEvent.title);
                            Debug.Log(currentEvent.description);

                            //Eventwindow
                            popUpWindow.createNotificationWindow(currentEvent.title, currentEvent.description);

                            currentEvent.run(this);
                        } 
                    }
                }

                //Check for tool stability if a tool is equiped
                if(tool != null)
                {
                    if (tool.getCurrentStability() <= 0)
                    {
                        tool.resetStability();
                        inventory.subItem(tool);
                        tool = null;
                    }
                }
               
                
                //ToDo: Set a basic location and basic work
                currentLocation = null;
                currentWork = null;

                //Am lager chillen
                playerData.currentLocationName = "Lager";
                playerData.currentActivityName = "Nichts";

                durationBar.fillAmount = 1;

                save();
                
            }

        }

        //Zuwissung der Aktuellen Tätigkeit und Ort zum Text in der UI
        currentLocationText.text = playerData.currentLocationName  + ": " + playerData.currentActivityName;
    }

    public void OnGUI()
    {
        counter++;
        if (counter == foodDownSpeed)
        {
            if (playerData.food != 0)
            {
                playerData.food--;
            } else
            {
                playerData.health--;                
            }
            counter = 0;
        }

    }


    public float getHealth()
    {
        return playerData.health;
    }


    public float getActivityPoints()
    {
        return playerData.ap;
    }

    public int getStr()
    {
        return playerData.str;
    }

    public void changeStatus(int health, int activityPoints)
    {
        setHealth(health);
        setActivityPoints(activityPoints);
    }

    public void save()
    {
        playerData.Save();
        Debug.Log("Saved!");
    }

    public void setActivity(Activity activity)
    {
        if (!isWorking)
        {
                if (playerData.ap >= activity.activityPoints)
                {
                    if (checkEquipptedTool(activity))
                    {
                        currentWork = activity;
                        currentLocation = currentWork.currentLocation;
                        activityPoints -= currentWork.activityPoints;
                        workingTime = currentWork.workingTime;
                        isWorking = true;

                        //Zuwissung der aktuellen Location
                        playerData.currentLocationName = currentLocation.locationName;
                        playerData.currentActivityName = currentWork.activityName;

                        totalTime = workingTime;

                        Debug.Log("AP: " + activityPoints);
                        if (activityPoints > playerData.apMAX)

                        {
                            playerData.ap = playerData.apMAX;
                            Debug.Log("Max AP erreicht.");
                            activityPoints = playerData.apMAX;
                        }
                        else
                        {
                            playerData.ap = activityPoints;
                        }


                }

                else
                {
                    Debug.Log("Nicht das benötigte Werkzeug ausgerüstet. Du brauchst " + activity.neededTool.name);

                    string title = "Achtung!";
                    string description = "Nicht das benötigte Werkzeug ausgerüstet. Du brauchst ein/e \n\n" + activity.neededTool.name;

                    popUpWindow.createNotificationWindow(title, description);
                }
            }
            else
            {
                Debug.Log("Leider nicht genug Aktivitätspunkte zur Verfügung");
                string title = "Achtung";
                string description = "Leider nicht genug Aktivitätspunkte zur Verfügung";
                popUpWindow.createNotificationWindow(title, description);

            }

        }
        else
        {
            Debug.Log("Es wird noch eine andere Tätigkeit ausgeführt!");
            string title = "Achtung";
            string description = "Es wird noch eine andere Tätigkeit ausgeführt!";
            popUpWindow.createNotificationWindow(title, description);

        }

    }

    public void eat(Food food)
    {
        Debug.Log("Aktuelle Gesundheit: " + health);
        if (inventory.subItem(food))
        {
            Debug.Log("Spieler geheilt um " + food.healthPoints);
            string title = "Heilung";
            string description = "Du wurdest um \n" + food.healthPoints + " \ngeheilt.\nDu hast jetzt \n" + playerData.health + " Gesundheit.";
            setHealth(food.healthPoints);
            playerData.food = playerData.foodMAX;
            popUpWindow.createNotificationWindow(title, description);

            Debug.Log("Gesundheit nach dem Essen: " + health);
        }

        else
        {
            Debug.Log(food.name + " nicht im Inventar vorhanden");
            string title = "Nicht möglich";
            string description = food.name + " nicht im Inventar vorhanden";

            popUpWindow.createNotificationWindow(title, description);

        }

        inventoryHandler.OpenFood();
    }

    public void equip(Tool tool)
    {
        this.tool = tool;
        eqSlots[5].text = "Waffe: "+tool.name;
    }

    public void equip(Equipment equipment)
    {
        if (equipment.type == Equipment.Types.Weapon)
        {
            unequip(weapon);
            weapon = equipment;
            eqSlots[5].text = "Waffe: " + weapon.name;
        }
        else if (equipment.type == Equipment.Types.Head)
        {
            unequip(head);
            head = equipment;
            eqSlots[0].text = "Kopf: " + equipment.name;
        }
        else if (equipment.type == Equipment.Types.Breast)
        {
            unequip(breast);
            breast = equipment;
            eqSlots[1].text = "Brust: "+ equipment.name;
        }
        else if (equipment.type == Equipment.Types.Hands)
        {
            unequip(hands);
            hands = equipment;
            eqSlots[2].text = "Hände: " + equipment.name;
        }
        else if (equipment.type == Equipment.Types.Legs)
        {
            unequip(legs);
            legs = equipment;
            eqSlots[3].text = "Beine: " + equipment.name;
        }
        else if (equipment.type == Equipment.Types.Feet)
        {
            unequip(feet);
            feet = equipment;
            eqSlots[4].text = "Füße: "+ equipment.name;
        }

        playerData.str += equipment.str;
        playerData.con += equipment.con;
        playerData.agi += equipment.agi;
        playerData.wis += equipment.wis;
        Debug.Log("Stärke: " + str + ", Konstitution: " + con + ", Geschicklichkeit: " + agi + ", Wissen: " + wis);

    }

    private void collectMaterials()
    {
        List<Item> collectedMaterials = materialManager.collectMaterials(currentWork, currentLocation);
        foreach (Item collectedMaterial in collectedMaterials)
        {
            inventory.addItem(collectedMaterial);
            string title = "Du hast etwas gefunden!";
            string description = "Du hast eine/n \n" + collectedMaterial.name + "\ngefunden";

            popUpWindow.createNotificationWindow(title, description);

        }
    }

    private void setHealth(int health)
    {
        this.health += health;

        if (this.health <= 0)
        {
            // Vorrübergehend, bis bessere Lösung
            Debug.Log("Spieler ist gestorben! ");
            SceneManager.LoadScene("MainMenu");
        }

        if (this.health >= playerData.healthMAX)
        {
            this.health = playerData.healthMAX;
        }

        playerData.health = this.health;
    }

    private void setActivityPoints(int activityPoints)
    {
        this.activityPoints += activityPoints;

        if (this.activityPoints < 0)
        {
            this.activityPoints = 0;
        }

        if (this.activityPoints >= playerData.apMAX)
        {
            this.activityPoints = playerData.apMAX;
        }

        playerData.ap = this.activityPoints;
    }

    private void unequip(Equipment equipment)
    {
        if(equipment != null)
        {
            str -= equipment.str;
            con -= equipment.con;
            agi -= equipment.agi;
            wis -= equipment.wis;
        }

    }

    private bool checkEquipptedTool(Activity activity)
    {
        if(activity.neededTool == null)
        {
            return true;
        }

        if(tool == activity.neededTool)
        {
            tool.subStability();
            return true;
        }

        return false;
    }
 
}


