using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour {

    //private const int MAX_HEALTH = 100;
    private float health;
    private float activityPoints;
    private bool isWorking;
    private int str, con, agi, wis;
    private Location currentLocation;
    private Activity currentWork;
    private MaterialManager materialManager;

    public Equipment weapon, head, breast, hands, legs, feet;
    public Inventory inventory;
    public PlayerDatas playerData;
    public Text currentLocationText;
    public Image durationBar;

    private float workingTime;
    private float totalTime;

	// Use this for initialization
	void Start () {

        health = playerData.health;
        activityPoints = playerData.ap;
        workingTime = 0;
        isWorking = false;
        materialManager = new MaterialManager();

        //str = playerData.str;
        //con = playerData.con;
        //agi = playerData.agi;
        //wis = playerData.wis;

        Debug.Log("Stärke: " + playerData.str + ", Konstitution: " + playerData.con + ", Geschicklichkeit: " + playerData.agi + ", Wissen: " + playerData.wis);

        //Keine aktuelle aufgabe vom start her -- Platzhalter
        PlayerPrefs.SetString("CurrentLocationName", "Camp");
        PlayerPrefs.SetString("CurrentActivityName", "None");

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
            //Debug.Log("Tätigkeitsdauer: " + workingTime + " Sekunden");
            
            workingTime -= Time.deltaTime;

            //in der Leiste darstellen
            durationBar.fillAmount = workingTime/totalTime;

            if (workingTime <= 0)
            {
                isWorking = false;
                workingTime = 0;

                collectMaterials();
                //ToDo: Set a basic location and basic work
                currentLocation = null;
                currentWork = null;

                Debug.Log("Fertig mit Tätigkeit");
                
                //Am lager chillen
                PlayerPrefs.SetString("CurrentLocationName", "Lager");
                PlayerPrefs.SetString("CurrentActivityName", "Nichts");
                durationBar.fillAmount = 1;

                save();
                
            }
        }

        //Zuwissung der Aktuellen Tätigkeit und Ort zum Text in der UI
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
        return playerData.health;
    }

    public float getActivityPoints()
    {
        return playerData.ap;
    }

    public void setActivity(Activity activity)
    {
        if (!isWorking)
        {
            if (playerData.ap >= activity.activityPoints)
            {
                currentWork = activity;
                currentLocation = currentWork.currentLocation;
                activityPoints -= currentWork.activityPoints;
                workingTime = currentWork.workingTime;
                isWorking = true;
                Debug.Log("Der Spieler befindet sich an folgenden Ort: " + currentLocation.locationName + " und führt folgende Tätigkeit aus: " + currentWork.activityName);

                //Zuwissung der aktuellen Location in die Playerprefs
                PlayerPrefs.SetString("CurrentLocationName", currentLocation.locationName);
                PlayerPrefs.SetString("CurrentActivityName", currentWork.activityName);
                totalTime = workingTime;
                Debug.Log("AP: "+ activityPoints);
                if (activityPoints > playerData.apMAX)
                {
                    //PlayerPrefs.SetFloat("apValue", PlayerPrefs.GetFloat("apMAX"));
                    playerData.ap = playerData.apMAX;
                    Debug.Log("Max AP erreicht.");
                    activityPoints = playerData.apMAX;
                }
                else
                {
                    //PlayerPrefs.SetFloat("apValue", activityPoints);
                    playerData.ap = activityPoints;
                }
                Debug.Log("AP Werte: "+playerData.ap + " : " + activityPoints);
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

        if(this.health >= playerData.healthMAX)
        {
            this.health = playerData.healthMAX;
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
        con += equipment.con;
        agi += equipment.agi;
        wis += equipment.wis;
        Debug.Log("Stärke: " + str + ", Konstitution: " + con + ", Geschicklichkeit: " + agi + ", Wissen: " + wis);


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
            con -= equipment.con;
            agi -= equipment.agi;
            wis -= equipment.wis;
        }

    }

    public void save()
    {
        playerData.Save();
        Debug.Log("Saved!");
    }

    //private void OnGUI()
    //{
    //    if (Input.GetKeyUp(KeyCode.S))
    //    {
    //        playerData.Save();
    //        Debug.Log("Saved!");

    //    }
    //}
  
    
}


