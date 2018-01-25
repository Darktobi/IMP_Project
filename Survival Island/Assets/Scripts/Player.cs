using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    //private const int MAX_HEALTH = 100;
    private float health;
    private float activityPoints;
    private bool isWorking;
    private int str, kon, ges, wei;
    private Location currentLocation;
    private Activity currentWork;
    private MaterialManager materialManager;
    private EventManager eventManager;
    private GameEvent currentEvent;

    public Equipment weapon, head, breast, hands, legs, feet;
    public Inventory inventory;
    public Text currentLocationText;
    public Image durationBar;

    private float workingTime;
    private float totalTime;

	// Use this for initialization
	void Start () {
        health = PlayerPrefs.GetFloat("foodMAX");
        activityPoints = PlayerPrefs.GetFloat("apValue");
        workingTime = 0;
        isWorking = false;
        materialManager = new MaterialManager();
        eventManager = new EventManager();

        str = PlayerPrefs.GetInt("STR");
        kon = PlayerPrefs.GetInt("CON");
        ges = PlayerPrefs.GetInt("FIN");
        wei = PlayerPrefs.GetInt("WIS");
       // Debug.Log("Stärke: " + str + ", Konstitution: " + kon + ", Geschicklichkeit: " + ges + ", Wissen: " + wei);

        //Keine aktuelle aufgabe vom start her -- Platzhalter
        PlayerPrefs.SetString("CurrentLocationName", "Camp");
        PlayerPrefs.SetString("CurrentActivityName", "None");

        durationBar.fillAmount = 1;
    }
	
	// Update is called once per frame
	void Update () {
        
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
                        }
                        
                    }

                }
                
                //ToDo: Set a basic location and basic work
                currentLocation = null;
                currentWork = null;
                
                //Am lager chillen
                PlayerPrefs.SetString("CurrentLocationName", "Lager");
                PlayerPrefs.SetString("CurrentActivityName", "Nichts");
                durationBar.fillAmount = 1;
            }
        }

        //Zuweisung der Aktuellen Tätigkeit und Ort zum Text in der UI
        currentLocationText.text = PlayerPrefs.GetString("CurrentLocationName") + ": " + PlayerPrefs.GetString("CurrentActivityName");
    }

    private void collectMaterials()
    {
        List<Item> collectedMaterials = materialManager.collectMaterials(currentWork, currentLocation);
       foreach(Item collectedMaterial in collectedMaterials)
        {
            inventory.addItem(collectedMaterial);
        }
    }

    public float getHealth()
    {
        return PlayerPrefs.GetFloat("foodValue");
    }

    public float getActivityPoints()
    {
        return PlayerPrefs.GetFloat("apValue");
    }

    public void setActivity(Activity activity)
    {
        if (!isWorking)
        {
            if (PlayerPrefs.GetFloat("apValue") >= activity.activityPoints)
            {
                currentWork = activity;
                currentLocation = currentWork.currentLocation;
                activityPoints -= currentWork.activityPoints;
                workingTime = currentWork.workingTime;
                isWorking = true;

                //Zuweisung der aktuellen Location in die Playerprefs
                PlayerPrefs.SetString("CurrentLocationName", currentLocation.locationName);
                PlayerPrefs.SetString("CurrentActivityName", currentWork.activityName);
                totalTime = workingTime;
                if (activityPoints > PlayerPrefs.GetFloat("apMAX"))
                {
                    PlayerPrefs.SetFloat("apValue", PlayerPrefs.GetFloat("apMAX"));
                    Debug.Log("Max AP erreicht.");
                    activityPoints = PlayerPrefs.GetFloat("apMAX");
                }
                else
                {
                    PlayerPrefs.SetFloat("apValue", activityPoints);
                }
            }
            else
            {
                Debug.Log("Leider nicht genug Aktivitätspunkte zur Verfügung");
            }

            
        }
        else
        {
            Debug.Log("Es wird noch eine andere Tätigkeit ausgeführt!");
        }
       
    }

    public void eat(Food food)
    {
        Debug.Log("Aktuelle Gesundheit: " + health);
        if (inventory.subItem(food))
        {
            Debug.Log("Spieler geheilt um " + food.healthPoints);
            setHealth(food.healthPoints);

            Debug.Log("Gesundheit nach dem Essen: " + health);
        }

        else
        {
            Debug.Log(food.name + " nicht im Inventar vorhanden");
        }
    }

    private void setHealth(int health)
    {
        this.health += health;

        if(this.health >= PlayerPrefs.GetFloat("foodMAX", 500))
        {
            this.health = PlayerPrefs.GetFloat("foodMAX", 500);
        }
    }

    public void equip(Equipment equipment)
    {
        if(equipment.type == Equipment.Types.Weapon)
        {
            unequip(weapon);
            weapon = equipment;
        }
        else if(equipment.type == Equipment.Types.Head)
        {
            unequip(head);
            head = equipment;
        }
        else if (equipment.type == Equipment.Types.Breast)
        {
            unequip(breast);
            breast = equipment;
        }
        else if (equipment.type == Equipment.Types.Hands)
        {
            unequip(hands);
            hands = equipment;
        }
        else if (equipment.type == Equipment.Types.Legs)
        {
            unequip(legs);
            legs = equipment;
        }
        else if (equipment.type == Equipment.Types.Feet)
        {
            unequip(feet);
            feet = equipment;
        }

        str += equipment.str;
        kon += equipment.kon;
        ges += equipment.ges;
        wei += equipment.wei;
        Debug.Log("Stärke: " + str + ", Konstitution: " + kon + ", Geschicklichkeit: " + ges + ", Wissen: " + wei);


        /*
        Debug.Log("Waffe: " + weapon.name);
        Debug.Log("Kopf: " + head.name);
        Debug.Log("Brust: " + breast.name);
        Debug.Log("Hände: " + hands.name);
        Debug.Log("Beine: " + legs.name);
        Debug.Log("Füße: " + feet.name);
        */
    }

    private void unequip(Equipment equipment)
    {
        if(equipment != null)
        {
            str -= equipment.str;
            kon -= equipment.kon;
            ges -= equipment.ges;
            wei -= equipment.wei;
        }

    }

}
