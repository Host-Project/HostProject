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

    [SerializeField]
    private Transform inPosition;
    [SerializeField]
    private Transform outPosition;

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

    

    private void Start()
    {
        rotation = messageObject.transform.rotation;
        
    }
    private void Update()
    {
        switch (direction)
        {
            case Direction.In:
                messageObject.transform.localPosition = Vector3.MoveTowards(messageObject.transform.localPosition, inPosition.localPosition, 0.1f);
                if (messageObject.transform.localPosition == inPosition.localPosition)
                    direction = Direction.None;
                break;
            case Direction.Out:
                messageObject.transform.localPosition = Vector3.MoveTowards(messageObject.transform.localPosition, outPosition.localPosition, 0.1f);
                if (messageObject.transform.localPosition == outPosition.localPosition)
                    direction = Direction.None;
                break;
        }
    }

    public void SetCryptedMessage(string message, string pairs)
    {
        this.message.GetComponentInChildren<TextMeshProUGUI>().text = message;

        string[] lst = pairs.Split(';');
        for (int i = 0; i < lst.Length; ++i)
        {
            Debug.Log(lst[i]);
            keys[i].text = lst[i];
        }
    }

    public void GiveMessage()
    {
        messageObject.GetComponent<NearInteractionGrabbable>().enabled = true;
        messageObject.transform.localPosition = outPosition.localPosition;
        messageObject.transform.localRotation = rotation;
        direction = Direction.In;
        doorSound.Play();
    }

    public void CheckAnswer()
    {
        if (direction != Direction.None) return;

        messageObject.GetComponent<NearInteractionGrabbable>().enabled = false;
        messageObject.transform.localPosition = inPosition.localPosition;
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
