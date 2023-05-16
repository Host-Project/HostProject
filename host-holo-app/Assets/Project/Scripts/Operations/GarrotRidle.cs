using HostProject.Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarrotRidle : MonoBehaviour
{
    [SerializeField]
    private HelpRPC rpc;

    [SerializeField]
    private GameObject bed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(bed != null && other.gameObject == bed)
        {
            //transform.parent.gameObject.SetActive(false);
            rpc.TriggerGarrotDone();
        }
    }

    public void Done()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
