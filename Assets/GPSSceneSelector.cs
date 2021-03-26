using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GPSSceneSelector : MonoBehaviour
{
    public Slider locationSlider;
    //public int locationToggle = 0;
    public float minSceneSwapDistance = 1f;
    public Vector3[] KerdroyaSiteCoords;
    public Vector3[] NewquayCoords;
    public Vector3[] PenrynCoords;
    public GameObject popUpPanel;
    public Button sceneSelectBtn;
    public Text sceneText;
    public bool animatingPanel = false;
    public float alphaTranitionSpeed = 0.05f;
    public float alphaDelta = 0f;
    public Text outputText;

    IEnumerator Start()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            outputText.text = "GPS not enabled";
           // yield break;
        }
        

        // Start service before querying location
        Input.location.Start(1, 1);

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
            outputText.text = "Service did not initialize. Timed out";
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            outputText.text = "LocationServiceStatus = failed. Unable to determine device location";
            yield break;
        }


        bool waitingIcon = false;
        while (true)
        {
            outputText.text = "Location: \nLatitude: " + Input.location.lastData.latitude +
               "\nLongitude: " + Input.location.lastData.longitude +
               "\nAltitude: " + Input.location.lastData.altitude +
               "\nHorizontal Accuracy: " + Input.location.lastData.horizontalAccuracy +
               "\ntimeStamp: " + Input.location.lastData.timestamp +
               "\n";

            float closestDistance = 0;
            int bestID = -1;

            //Switch based on the toggle in the debug menu. 0 and default are the coordinates of the Kerdroya labyrinth site
            switch (locationSlider.value)
            {
                case 0:
                    ArrayProximityTest(KerdroyaSiteCoords,
                                   new Vector3(Input.location.lastData.latitude,
                                               Input.location.lastData.longitude,
                                               Input.location.lastData.altitude),
                                   ref bestID,
                                   ref closestDistance);
                    outputText.text += "\n " + "KERDROYA SITE" + "\n";
                    break;
                case 1:
                    ArrayProximityTest(NewquayCoords,
                                   new Vector3(Input.location.lastData.latitude,
                                               Input.location.lastData.longitude,
                                               Input.location.lastData.altitude),
                                   ref bestID,
                                   ref closestDistance);
                    outputText.text += "\n " + "NEWQUAY" + "\n";
                    break;
                    
                case 2:
                    ArrayProximityTest(PenrynCoords,
                                   new Vector3(Input.location.lastData.latitude,
                                               Input.location.lastData.longitude,
                                               Input.location.lastData.altitude),
                                   ref bestID,
                                   ref closestDistance);
                    outputText.text += "\n " + "PENRYN" + "\n";
                    break;
                default:
                    ArrayProximityTest(KerdroyaSiteCoords,
                                   new Vector3(Input.location.lastData.latitude,
                                               Input.location.lastData.longitude,
                                               Input.location.lastData.altitude),
                                   ref bestID,
                                   ref closestDistance);
                    outputText.text += "\n " + "KERDROYA SITE" + "\n";
                    break;
            }
            
            outputText.text += "BestID: " + bestID + "\n" +
                "Closest Distance: " + closestDistance + "\n";
            SceneSelection(bestID, closestDistance);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ArrayProximityTest(Vector3[] targetLocations, Vector3 comparitor, ref int bestID, ref float closestDistance)
    {
        closestDistance = Mathf.Infinity;
        bestID = -1;
        int currentID = 0;
        foreach (Vector3 target in targetLocations)
        {
            //comparison including HEIGHT
            //float distance = Vector3.Distance(target, comparitor);
            //comparison with long/lat ONLY
            float distance = Vector2.Distance(target, comparitor);
            if (closestDistance > distance)
            {
                closestDistance = distance;
                bestID = currentID;
            }
            currentID++;
        }

    }

    public void HideLevelSelectButton() {

    animatingPanel = false;

    }


    public void SceneSelection(int sceneID, float distance)
    {
        //check if main splash has loaded yet
        //check if already loading scene
        //case for player rejects scene change

        if (distance < minSceneSwapDistance && !AppProgression.levelCompleted[sceneID])
        {
            animatingPanel = true;
            switch (sceneID)
            {
                case 0:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(1));
                    sceneText.text = "Predennak";
                    break;
                case 1:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(10));
                    sceneText.text = "Cape Cornwall";
                    break;
                case 2:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(6));
                    sceneText.text = "Morwenstow";
                    break;
                case 3:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(2));
                    sceneText.text = "Lansallos";
                    break;
                case 4:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(4));
                    sceneText.text = "Colliford Lake";
                    break;
                case 5:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(13));
                    sceneText.text = "The Blouth";
                    break;
                case 6:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(7));
                    sceneText.text = "Padstow";
                    break;
                case 7:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(14));
                    sceneText.text = "Park Head";
                    break;
                case 8:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(12));
                    sceneText.text = "St Agnes Head";
                    break;
                case 9:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(5));
                    sceneText.text = "Boscastle";
                    break;
                case 10:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(3));
                    sceneText.text = "Tehidy";
                    break;
                case 11:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(11));
                    sceneText.text = "Clarrick woods";
                    break;
            }
        }
    }

    public void FixedUpdate()
    {
        if (AppProgression.openingwatched)
        {
            if (animatingPanel)
            {
                popUpPanel.SetActive(true);
                alphaDelta += alphaTranitionSpeed;
                alphaDelta = alphaDelta < 0.95f ? alphaDelta : 1f;
                sceneSelectBtn.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, alphaDelta);
                sceneText.color = new Color(sceneText.color.r, sceneText.color.g, sceneText.color.b, sceneText.color.a + alphaDelta);
            }
            else
            {
                popUpPanel.SetActive(false);
            }
        }
    }
}