using HostProject.Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitoringRidle : MonoBehaviour
{


    [SerializeField]
    private Light success;

    [SerializeField]
    private Light error;

    [SerializeField]
    private List<Light> lights;

    [SerializeField]
    private HelpRPC rpc;

    private int lastId = -1;

    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendPushedButton(int id)
    {
        rpc.TriggerMonitoringPressedButton(id);
    }

    public void IsSuccess(bool success)
    {
        this.success.gameObject.SetActive(false);
        this.error.gameObject.SetActive(false);

        if (lastId != -1)
        {
            lights[lastId - 1].gameObject.SetActive(false);
        }

        if (success)
        {
            this.success.gameObject.SetActive(true);
            StartCoroutine(StopLight(this.success, 2));
        }
        else
        {
            this.error.gameObject.SetActive(true);
            StartCoroutine(StopLight(this.error, 2));
        }
    }

    public void ActiveButton(int id)
    {
        if(lastId == -1)
        {
            this.gameObject.GetComponent<AudioSource>().Play();
        }
        if(lastId != -1)
        {
            lights[lastId-1].gameObject.SetActive(false);
            lights[id-1].gameObject.SetActive(true);
        }

        lastId = id;
    }

    IEnumerator StopLight(Light l, int time)
    {
        yield return new WaitForSeconds(time);

        l.gameObject.SetActive(false);
    }


}
