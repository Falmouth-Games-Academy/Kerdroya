using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestLocationService : MonoBehaviour
{
    public Text outputText;
    IEnumerator Start()
    {
        outputText.text = "Booting...\n";

        // First, check if user has location service enabled
        /*if (!Input.location.isEnabledByUser)
        {
            outputText.color = Color.red;
            outputText.text = "GPS not enabled";
           // yield break;
        }
        */
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
                break;
            }
            waitingIcon = !waitingIcon;
            string elipseSwap = waitingIcon ? "" : "...";
            outputText.text = "Location: \nLatitude: " + Input.location.lastData.latitude +
               "\nLongitude: " + Input.location.lastData.longitude +
               "\nAltitude: " + Input.location.lastData.altitude +
               "\nHorizontal Accuracy: " + Input.location.lastData.horizontalAccuracy +
               "\ntimeStamp: " + Input.location.lastData.timestamp +
               "\n" + elipseSwap;
            yield return new WaitForSeconds(0.5f);
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }
}