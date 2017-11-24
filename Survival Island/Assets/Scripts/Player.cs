using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    private int health;
    private int activityPoints;
    private bool isWorking;
    private Location currentLocation;
    private Activity currentWork;
    private MaterialManager materialManager;

    public Inventory inventory;
    public Text currentLocationText;

    private float workingTime;

	// Use this for initialization
	void Start () {
        activityPoints = 10;
        workingTime = 0;
        isWorking = false;
        materialManager = new MaterialManager();

        //Keine aktuelle aufgabe vom start her -- Platzhalter
        PlayerPrefs.SetString("CurrentLocationName", "Camp");
        PlayerPrefs.SetString("CurrentActivityName", "None");
    }
	
	// Update is called once per frame
	void Update () {

        if (isWorking)
        {
            Debug.Log("Tätigkeitsdauer: " + workingTime + " Sekunden");
            workingTime -= Time.deltaTime;
            if(workingTime <= 0)
            {
                isWorking = false;
                workingTime = 0;

                collectMaterials();
                //ToDo: Set a basic location and basic work
                currentLocation = null;
                currentWork = null;

                Debug.Log("Fertig mit Tätigkeit");
                
                //Am lager chillen
                PlayerPrefs.SetString("CurrentLocationName", "Camp");
                PlayerPrefs.SetString("CurrentActivityName", "None");

            }
        }

        //Zuweisung der Aktuellen Tätigkeit und Ort zum Text in der UI
        currentLocationText.text = PlayerPrefs.GetString("CurrentLocationName") + ": " + PlayerPrefs.GetString("CurrentActivityName");
    }

    private void collectMaterials()
    {
        List<Mat> collectedMaterials = materialManager.collectMaterials(currentWork, currentLocation);
       foreach(Mat collectedMaterial in collectedMaterials)
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
            if (activityPoints >= activity.activityPoints)
            {
                currentWork = activity;
                currentLocation = currentWork.currentLocation;
                activityPoints -= currentWork.activityPoints;
                workingTime = currentWork.workingTime;
                isWorking = true;
                Debug.Log("Der Spieler befindet sich an folgenden Ort: " + currentLocation.locationName + " und führt folgende Tätigkeit aus: " + currentWork.activityName);

                //Zuweisung der aktuellen Location in die Playerprefs
                PlayerPrefs.SetString("CurrentLocationName", currentLocation.locationName);
                PlayerPrefs.SetString("CurrentActivityName", currentWork.activityName);

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

    public void setHealth(int health)
    {

    }

}
