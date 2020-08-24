using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeGenerator : MonoBehaviour
{

    public GameObject bee;
    public float timer = 3;
    public bool left;
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
            BeeBehaviour beeBahaviour = b.GetComponent<BeeBehaviour>();

            float xpos = 2;
            if (left)
            {
                xpos = -2;
                beeBahaviour.leftFacing = false;
                b.transform.localRotation = Quaternion.Euler(0, 180,0);
            }
            b.transform.localPosition = new Vector3(xpos, Random.Range(-0.5f, 0.5f), b.transform.localPosition.z);



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
