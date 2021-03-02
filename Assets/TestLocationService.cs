using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.UIElements;

[System.Serializable]

public class TestLocationService : MonoBehaviour
{
    public Text outputText;
    public float minSceneSwapDistance = 99999;//1f;
    public Vector3[] locationData;
    public GameObject popUpPanel;
    public UnityEngine.UI.Button sceneSelectBtn;
    public Text sceneText;
    private RectTransform RT;
    public bool animatingPanel = false;
    public float alphaTranitionSpeed = 0.05f;
    public float alphaDelta = 0f;

    public float TESTlat;
    public float TESTlon;
    public float TESTalt;
    public float TESTclosestDistance = 0;
    public int TESTbestID = -1;
    public Text TESTtext;

    
    IEnumerator Start()
    {
        /*
        RT = popUpPanel.GetComponent<RectTransform>();
        outputText.text = "Booting...\n";

        // First, check if user has location service enabled
        /*if (!Input.location.isEnabledByUser)
        {
            outputText.color = Color.red;
            outputText.text = "GPS not enabled";
           // yield break;
        }
        */
    /*
        // Start service before querying location
        Input.location.Start(1,1);

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            outputText.text += ".";
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            outputText.text = "Timed out";
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            outputText.text = "Unable to determine device location";
            yield break;
        }

        
        bool waitingIcon = false;
        while (true)
        {
            if (Input.location.status == LocationServiceStatus.Stopped || 
                Input.location.status == LocationServiceStatus.Failed)
            {
                outputText.color = Color.red;
                outputText.text = "Connection Status: " + Input.location.status.ToString();
               // break;
            }
            waitingIcon = !waitingIcon;
            string elipseSwap = waitingIcon ? "" : "...";
            outputText.text = "Location: \nLatitude: " + Input.location.lastData.latitude +
               "\nLongitude: " + Input.location.lastData.longitude +
               "\nAltitude: " + Input.location.lastData.altitude +
               "\nHorizontal Accuracy: " + Input.location.lastData.horizontalAccuracy +
               "\ntimeStamp: " + Input.location.lastData.timestamp +
               "\n" + elipseSwap;
    */

            float closestDistance = 0;
            int bestID = -1;
            //took out Input.location.lastData.latitude, long, alt
            ArrayProximityTest(locationData,
                                   new Vector3(TESTlat,
                                               TESTlon,
                                               TESTlat),
                                   ref bestID,
                                   ref closestDistance);
            TESTSceneSelection(bestID, closestDistance); //<-----------------------Remove test from this line
            yield return new WaitForSeconds(0.5f);
        /*
        }
        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();

        */
    }

    
    
    private void Update()
    {
       ArrayProximityTest(locationData,new Vector3(TESTlat, TESTlon, TESTalt),ref TESTbestID, ref TESTclosestDistance);
        TESTSceneSelection(TESTbestID, TESTclosestDistance);
    }
    

    public void ArrayProximityTest(Vector3[] targetLocations, Vector3 comparitor, ref int bestID, ref float closestDistance)
    {
        closestDistance = Mathf.Infinity;
        bestID = -1;
        int currentID = 0;
        foreach(Vector3 target in targetLocations)
        {
            float distance = Vector3.Distance(target, comparitor);
            if (closestDistance > distance)
            {
                closestDistance = distance;
                bestID = currentID;
            }
            currentID++;
        }
        
    }

    public void SceneSelection(int sceneID, float distance)
    {
        //check if main splash has loaded yet
        //check if already loading scene
        //case for player rejects scene change

        if(distance < minSceneSwapDistance)
        {
            animatingPanel = true;
            switch (sceneID)
            {
                case 0 :
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(1));
                    sceneText.text = "Predennak";
                    break;
                case 1 :
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(2));
                    sceneText.text = "Lansallos";
                    break;
                case 2 :
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(3));
                    sceneText.text = "Tehidy";
                    break;
                case 3 :
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(4));
                    sceneText.text = "Colliford Lake";
                    break;
                case 4:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(5));
                    sceneText.text = "Boscastle";
                    break;
                case 5:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(6));
                    sceneText.text = "Morwenstow";
                    break;
                case 6:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(7));
                    sceneText.text = "Padstow";
                    break;
                case 7:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(10));
                    sceneText.text = "Cape Cornwall";
                    break;
                case 8:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(11));
                    sceneText.text = "Clarrick woods";
                    break;
                case 9:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(12));
                    sceneText.text = "St Agnes Head";
                    break;
                case 10:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(13));
                    sceneText.text = "The Blouth";
                    break;
                case 11:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(14));
                    sceneText.text = "Park Head";
                    break;
            }

        }
    }

    public void TESTSceneSelection(int sceneID, float distance)
    {
        //check if main splash has loaded yet
        //check if already loading scene
        //case for player rejects scene change

        if (distance < minSceneSwapDistance)
        {
            animatingPanel = true;
            switch (sceneID)
            {
                case 0:
                    TESTtext.text = "Site: " + sceneID + '\n' + "Distance: " + distance;
                    break;
                case 1:
                    TESTtext.text = "Site: " + sceneID + '\n' + "Distance: " + distance;
                    break;
                case 2:
                    TESTtext.text = "Site: " + sceneID + '\n' + "Distance: " + distance;
                    break;
                case 3:
                    TESTtext.text = "Site: " + sceneID + '\n' + "Distance: " + distance;
                    break;
                case 4:
                    TESTtext.text = "Site: " + sceneID + '\n' + "Distance: " + distance;
                    break;
                case 5:
                    TESTtext.text = "Site: " + sceneID + '\n' + "Distance: " + distance;
                    break;
                case 6:
                    TESTtext.text = "Site: " + sceneID + '\n' + "Distance: " + distance;
                    break;
                case 7:
                    TESTtext.text = "Site: " + sceneID + '\n' + "Distance: " + distance;
                    break;
                case 8:
                    TESTtext.text = "Site: " + sceneID + '\n' + "Distance: " + distance;
                    break;
                case 9:
                    TESTtext.text = "Site: " + sceneID + '\n' + "Distance: " + distance;
                    break;
                case 10:
                    TESTtext.text = "Site: " + sceneID + '\n' + "Distance: " + distance;
                    break;
                case 11:
                    TESTtext.text = "Site: " + sceneID + '\n' + "Distance: " + distance;
                    break;
            }
        }
        else
        {
            TESTtext.text = "No point in range";
        }
        
    }

    public void FixedUpdate()
    {

        if (animatingPanel)
        {
            alphaDelta += alphaTranitionSpeed;
            alphaDelta = alphaDelta < 0.95f ? alphaDelta : 1f;
            sceneSelectBtn.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, alphaDelta);
        }
    }
}