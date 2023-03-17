using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class GyroRotate : MonoBehaviour
{
    // STATE
    private float _initialYAngle = 0f;
    private float _appliedGyroYAngle = 0f;
    private float _calibrationYAngle = 0f;
    private Transform _rawGyroRotation;
    private float _tempSmoothing;
    private Quaternion startRot;
    [SerializeField] private Text text;
    [SerializeField] private Text startTex;
    [SerializeField] private Text curTex;
    private TransitionHandler tHandle;
    private bool setup = false;
    private byte progress;
    [SerializeField] private GyroScene scene;
    [SerializeField] private Transform rotationalTruth;
    [SerializeField] private Transform wishbone;

    // SETTINGS
    [SerializeField] private float _smoothing = 0.1f;

    private IEnumerator Start()
    {
        tHandle = FindObjectOfType<TransitionHandler>();
        Input.gyro.enabled = true;
        Application.targetFrameRate = 60;
        _initialYAngle = transform.eulerAngles.y;

        _rawGyroRotation = new GameObject("GyroRaw").transform;
        _rawGyroRotation.position = transform.position;
        _rawGyroRotation.rotation = transform.rotation;

        // Wait until gyro is active, then calibrate to reset starting rotation.
        yield return new WaitForSeconds(3);

        startRot = Input.gyro.attitude;
        StartCoroutine(CalibrateYAngle());
        if (startTex != null)
            startTex.text = startRot.ToString();
        setup = true;
    }

    private void Update()
    {
        if (!Application.isEditor)
        {
            ApplyGyroRotation();
            ApplyCalibration();

            transform.rotation = Quaternion.Slerp(transform.rotation, _rawGyroRotation.rotation, _smoothing);

        }

        Vector3 curRot = Input.gyro.attitude.eulerAngles;

        NorthCliffScene(curRot);

        if (text != null)
            text.text = Quaternion.Angle(Input.gyro.attitude, startRot).ToString();
        //curTex.text = Input.gyro.attitude.eulerAngles.ToString();
    }

    private void ClarrickWoodsScene(Vector3 curRot)
    {

    }

    private void NorthCliffScene(Vector3 curRot)
    {
        curTex.text = Vector3.Angle(Vector3.up, rotationalTruth.up).ToString();
        //Vector3 boneRot = wishbone.eulerAngles;
        //boneRot.z = Vector3.Angle(Vector3.up, rotationalTruth.up);
        //wishbone.eulerAngles = boneRot;
        switch (progress)
        {
            case 0:
                if (tHandle?.sceneState >= 4)
                    progress |= 1;
                break;
            case 1:
                if (Vector3.Angle(rotationalTruth.up, Vector3.up) < 45)
                    progress |= 1 << 1;
                break;
            case 3:
                if (Vector3.Angle(rotationalTruth.up, Vector3.up) > 135)
                    progress |= 1 << 2;
                break;
            case 7:
                if (Vector3.Angle(rotationalTruth.up, Vector3.up) < 45)
                    FindObjectOfType<MinigameProgressTracker>().points = 99;
                break;
            default:
                progress = 0;
                break;
        }
    }

    private IEnumerator CalibrateYAngle()
    {
        _tempSmoothing = _smoothing;
        _smoothing = 1;
        _calibrationYAngle = _appliedGyroYAngle - _initialYAngle; // Offsets the y angle in case it wasn't 0 at edit time.
        yield return null;
        _smoothing = _tempSmoothing;
    }

    private void ApplyGyroRotation()
    {
        _rawGyroRotation.rotation = Input.gyro.attitude;
        _rawGyroRotation.Rotate(0f, 0f, 180f, Space.Self); // Swap "handedness" of quaternion from gyro.
        _rawGyroRotation.Rotate(90f, 180f, 0f, Space.World); // Rotate to make sense as a camera pointing out the back of your device.
        _appliedGyroYAngle = _rawGyroRotation.eulerAngles.y; // Save the angle around y axis for use in calibration.
    }

    private void ApplyCalibration()
    {
        _rawGyroRotation.Rotate(0f, -_calibrationYAngle, 0f, Space.World); // Rotates y angle back however much it deviated when calibrationYAngle was saved.
    }

    public void SetEnabled(bool value)
    {
        enabled = true;
        StartCoroutine(CalibrateYAngle());
    }
}

public enum GyroScene
{
    clarrickWoods,
    northCliff
}