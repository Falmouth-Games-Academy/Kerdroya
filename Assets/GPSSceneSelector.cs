using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class GPSSceneSelector : MonoBehaviour
{
    public Slider locationSlider;
    //public int locationToggle = 0;
    public float minSceneSwapDistance = 0.00005f;
    public InputField debugInputForAdjustingDistanceSensitivity;
    public Text debugDetectionDistanceText;
    public Vector3[] KerdroyaSiteCoords = 
    {
        new Vector3(50.52843f	,-4.592323f, 0f),
        new Vector3(50.52837f	,-4.592353f, 0f),
        new Vector3(50.528219f	,-4.592422f, 0f),
        new Vector3(50.52833f	,-4.592485f, 0f),
        new Vector3(50.52822f	,-4.592645f, 0f),
        new Vector3(50.52818f	,-4.592618f, 0f),
        new Vector3(50.52812f	,-4.592772f, 0f),
        new Vector3(50.52807f	,-4.592645f, 0f),
        new Vector3(50.52817f	,-4.592514f, 0f),
        new Vector3(50.52824f	,-4.592287f, 0f),
        new Vector3(50.52823f	,-4.592142f, 0f),
        new Vector3(50.52824f	,-4.592150f, 0f)
    };
    public Vector3[] NewquayCoords;
    public Vector3[] PenrynCoords;
    public GameObject popUpPanel;
    public Button sceneSelectBtn;
    public Text sceneText;
    public bool animatingPanel = false;
    public float alphaTranitionSpeed = 0.05f;
    public float alphaDelta = 0f;
    public Text outputText;

    private int[] AONBtoSavefileTranslation = {2,9,6,7,8,10,1,0,5,3,11,4};

    IEnumerator Start()
    {
        popUpPanel.SetActive(false);

        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }


        // First, check if user has loc`§§§ation service enabled
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
            int bestAONBID = -1;

            //Switch based on the toggle in the debug menu. 0 and default are the coordinates of the Kerdroya labyrinth site
            switch (locationSlider.value)
            {
                case 0:
                    ArrayProximityTest(KerdroyaSiteCoords,
                                   new Vector3(Input.location.lastData.latitude,
                                               Input.location.lastData.longitude,
                                               Input.location.lastData.altitude),
                                   ref bestAONBID,
                                   ref closestDistance);
                    outputText.text += "\n " + "KERDROYA SITE" + "\n";
                    break;
                case 1:
                    ArrayProximityTest(NewquayCoords,
                                   new Vector3(Input.location.lastData.latitude,
                                               Input.location.lastData.longitude,
                                               Input.location.lastData.altitude),
                                   ref bestAONBID,
                                   ref closestDistance);
                    outputText.text += "\n " + "NEWQUAY" + "\n";
                    break;
                    
                case 2:
                    ArrayProximityTest(PenrynCoords,
                                   new Vector3(Input.location.lastData.latitude,
                                               Input.location.lastData.longitude,
                                               Input.location.lastData.altitude),
                                   ref bestAONBID,
                                   ref closestDistance);
                    outputText.text += "\n " + "PENRYN" + "\n";
                    break;
                default:
                    ArrayProximityTest(KerdroyaSiteCoords,
                                   new Vector3(Input.location.lastData.latitude,
                                               Input.location.lastData.longitude,
                                               Input.location.lastData.altitude),
                                   ref bestAONBID,
                                   ref closestDistance);
                    outputText.text += "\n " + "KERDROYA SITE" + "\n";
                    break;
            }
            
            outputText.text += "BestID: " + bestAONBID + "\n" +
                "Closest Distance: " + closestDistance + "\n";

            int saveID = AONBtoSavefileTranslation[bestAONBID];
            SceneSelection(bestAONBID, saveID, closestDistance);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ArrayProximityTest(Vector3[] targetLocations, Vector3 comparitor, ref int bestAONBID, ref float closestDistance)
    {
        closestDistance = Mathf.Infinity;
        bestAONBID = -1;
        int currentID = 0;
        
        foreach (Vector3 target in targetLocations)
        {
            Debug.Log("CurrentID = " + currentID);
            int saveID = AONBtoSavefileTranslation[currentID];
            //check if complete
            if (AppProgression.levelCompleted[saveID])
            {
                //continue
                currentID++;
                continue;
            }
            
            //comparison including HEIGHT
            //float distance = Vector3.Distance(target, comparitor);
            //comparison with long/lat ONLY
            float distance = Vector2.Distance(target, comparitor);
            if (closestDistance > distance)
            {
                closestDistance = distance;
                bestAONBID = currentID;
            }
            currentID++;
        }

        

    }

    public void HideLevelSelectButton() {

    animatingPanel = false;

    }


    public void SceneSelection(int AONBID, int saveID, float distance)
    {
        //check if main splash has loaded yet
        //check if already loading scene
        //case for player rejects scene change

        Debug.Log(
            AppProgression.levelCompleted[0] + "\n" +
            AppProgression.levelCompleted[1] + "\n" +
            AppProgression.levelCompleted[2] + "\n" +
            AppProgression.levelCompleted[3] + "\n" +
            AppProgression.levelCompleted[4] + "\n" +
            AppProgression.levelCompleted[5] + "\n" +
            AppProgression.levelCompleted[6] + "\n" +
            AppProgression.levelCompleted[7] + "\n" +
            AppProgression.levelCompleted[8] + "\n" +
            AppProgression.levelCompleted[9] + "\n" +
            AppProgression.levelCompleted[10] + "\n" +
            AppProgression.levelCompleted[11] + "\n" 
            );


        if (distance < minSceneSwapDistance && !AppProgression.levelCompleted[saveID])
        {
            animatingPanel = true;
            switch (AONBID)
            {

                case 0:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(6));
                    sceneText.text = "Lucky Hole"; //Morwenstow
                    break;
                case 1:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(5));
                    sceneText.text = "Pentargon"; //Boscastle
                    break;
                case 2:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(7));
                    sceneText.text = "Dennis Cove"; //Padstow
                    break;
                case 3:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(14));
                    sceneText.text = "Park Head"; 
                    break;
                case 4:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(12));
                    sceneText.text = "Bawden"; //St Agnes
                    break;
                case 5:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(3));
                    sceneText.text = "North Cliff Plantation"; //Tehidy
                    break;
                case 6:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(10));
                    sceneText.text = "Priest's Cove"; //Cape Cornwall
                    break;
                case 7:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(1));
                    sceneText.text = "Predannack Wollas"; //The Lizard
                    break;
                case 8:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(13));
                    sceneText.text = "The Blouth";
                    break;
                case 9:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(2));
                    sceneText.text = "West Coombe"; //Lansellos
                    break;
                case 10:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(11));
                    sceneText.text = "Clarrick Woods";
                    break;
                case 11:
                    sceneSelectBtn.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(4));
                    sceneText.text = "Colliford Lake";
                    break;
            }
        }
        else
        {
            animatingPanel = false;
        }
    }

    public void ChangeDetectionDistance()
    {
        minSceneSwapDistance = float.Parse(debugInputForAdjustingDistanceSensitivity.text);

        if (debugInputForAdjustingDistanceSensitivity.text == null)
        {
            minSceneSwapDistance = 1;
            return;
        }

        
    }

    void UpdateDetectionDistanceText()
    {
        debugDetectionDistanceText.text = "" + minSceneSwapDistance;
    }

    public void FixedUpdate()
    {
        UpdateDetectionDistanceText();

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