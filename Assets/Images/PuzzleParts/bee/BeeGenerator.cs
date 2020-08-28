using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeGenerator : MonoBehaviour
{

    public GameObject bee;
    public float timer = 3;
    public bool right;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void GenerateBee()
    {
        if (bee != null)
        {
            GameObject b = Instantiate(bee, bee.transform.localPosition, bee.transform.localRotation);
            b.transform.parent = transform;
            b.transform.localPosition = Vector3.zero;
            BeeBehaviour beeBehaviour = b.GetComponent<BeeBehaviour>();
            beeBehaviour.leftFacing = true;

            float xpos = -2;
            if (!right)
            {
                xpos = 2;
                beeBehaviour.leftFacing = false;
                b.transform.localRotation = Quaternion.Euler(0, 180,0);
            }
            beeBehaviour.startYpos = Random.Range(-0.5f, 0.5f);
            b.transform.localPosition = new Vector3(xpos,beeBehaviour.startYpos, b.transform.localPosition.z);
            


            //b.GetComponent<BeeBehaviour>().Initialise();
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            GenerateBee();
            timer = 3;
        }
    }
}
