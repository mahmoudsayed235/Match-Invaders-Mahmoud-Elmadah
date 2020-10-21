using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionController : MonoBehaviour
{
    public GameObject protectionContainer;
    public Vector3 position = new Vector3(0f, 0f, 0f);
    // Start is called before the first frame update
    void OnEnable()
    {
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        GameObject protections = Instantiate(protectionContainer, position, Quaternion.identity);
        protections.transform.parent = this.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
