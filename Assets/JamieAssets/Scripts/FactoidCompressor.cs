using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoidCompressor : MonoBehaviour
{

    private bool expanded = false;
    private Vector2 startDelta;
    private RectTransform body;
    private FactoidCompressor[] otherCompressors;
    [SerializeField] private RectTransform arrow;


    private void Start()
    {
        FindBody();
        FindOtherCompressors();
        startDelta = body.sizeDelta;
        Vector2 temp = body.sizeDelta;
        temp.y = 0;
        body.sizeDelta = temp;
    }

    private void FindOtherCompressors()
    {
        List<FactoidCompressor> comps = new List<FactoidCompressor>();
        foreach (FactoidCompressor f in FindObjectsOfType<FactoidCompressor>())
        {
            if (f != this)
                comps.Add(f);
        }
        otherCompressors = comps.ToArray();
    }

    private void FindBody()
    {
        string targetName = transform.parent.name;
        targetName = targetName.Replace("Head", "Body");
        body = GameObject.Find(targetName).GetComponent<RectTransform>();
    }

    public void Close()
    {
        expanded = true;
        Clicked();
    }

    public void Clicked()
    {
        Vector2 targetDelta = startDelta;
        if (expanded)
            targetDelta.y = 0;

        float arrowTarget = expanded? 90 : 0;

        expanded = !expanded;

        StartCoroutine(lerp(targetDelta, arrowTarget));
    }

    public void CollapseOthers()
    {
        foreach (FactoidCompressor f in otherCompressors)
        {
            f.Close();
        }
    }

    IEnumerator lerp(Vector2 to, float arrowTarget)
    {
        Vector2 from = body.sizeDelta;
        Vector3 arrowTargetRot = arrow.localEulerAngles;
        arrowTargetRot.z = arrowTarget;
        Vector3 arrowStart = arrow.localEulerAngles;

        for (float t = 0; t < 1; t += Time.deltaTime * 3)
        {
            body.sizeDelta = Vector2.Lerp(from, to, t);
            arrow.localEulerAngles = Vector3.Lerp(arrowStart, arrowTargetRot, t);
            yield return null;
        }

        body.sizeDelta = to;
        arrow.localEulerAngles = arrowTargetRot;
    }

}
