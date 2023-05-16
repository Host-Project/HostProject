using HostProject.Network;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CryptedMessageRidle : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> keys;

    [SerializeField]
    private GameObject messageObject;
    private Quaternion rotation;
    [SerializeField]
    private TextMeshProUGUI message;

    private Vector3 inRoomPosition;
    private Vector3 outOfRoomPosition;

    [SerializeField]
    private AudioSource doorSound;

    [SerializeField]
    private GameObject VanocomycineCheck;

    [SerializeField]
    private GameObject CephalosporineCheck;

    [SerializeField]
    private GameObject AmoxicilineCheck;


    [SerializeField]
    private HelpRPC rpc;
    enum Direction { In, Out, None}
    private Direction direction = Direction.None;

    public void SetCryptedMessage(string message, string pairs)
    {
        this.message.GetComponentInChildren<TextMeshProUGUI>().text = message;

        string[] lst = pairs.Split(';');
        for(int i = 0; i < lst.Length; ++i)
        {
            Debug.Log(lst[i]);
            keys[i].text = lst[i];
        }
    }
    private void Start()
    {
        rotation = messageObject.transform.rotation;
        outOfRoomPosition = messageObject.transform.localPosition;
        inRoomPosition = outOfRoomPosition - new Vector3(0, 0, 2);
        //GiveMessage();
        
    }
    private void Update()
    {
        switch (direction)
        {
            case Direction.In:
                if (messageObject.transform.localPosition.z > inRoomPosition.z)
                    messageObject.transform.position += new Vector3(0, 0, 1 * Time.deltaTime);
                else
                    direction = Direction.None;
                break;
            case Direction.Out:
                if (messageObject.transform.localPosition.z < outOfRoomPosition.z)
                    messageObject.transform.position -= new Vector3(0, 0, 1 * Time.deltaTime);
                else
                    direction = Direction.None;
                break;
        }
    }

    public void GiveMessage()
    {
        messageObject.GetComponent<NearInteractionGrabbable>().enabled = true;
        messageObject.transform.localPosition = outOfRoomPosition;
        messageObject.transform.localRotation = rotation;
        direction = Direction.In;
        doorSound.Play();
    }

    public void CheckAnswer()
    {
        if (direction != Direction.None) return;

        messageObject.GetComponent<NearInteractionGrabbable>().enabled = false;
        messageObject.transform.localPosition = inRoomPosition;
        messageObject.transform.localRotation = rotation;
        direction = Direction.Out;
        if(!CephalosporineCheck.activeInHierarchy || VanocomycineCheck.activeInHierarchy || AmoxicilineCheck.activeInHierarchy)
            Invoke("GiveMessage", 5);
        else
        {
            rpc.TriggerCryptedMessageDone();
        }
    }

    public void Done()
    {
        messageObject.SetActive(false);
    }

}
