using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Host;
using Host.Network;


public class RidleManager : MonoBehaviour
{

    private HelpRPC _helpRPC;


    // Start is called before the first frame update
    void Start()
    {
        if (GlobalElements.Instance != null)
            _helpRPC = GlobalElements.Instance.HelpRPC;

        _helpRPC.SetRidleManager(this);
        CryptedMessage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private (string, string) CryptMessage()
    {

        string uncrypted = "il est allergique a la cephalosporine";

        string remainingLetters = "abefghjklnopqrstwxyz";
        string lettersToCode = "lgquchons";

        Dictionary<char, char> pairs = new Dictionary<char, char>();
        pairs.Add('e', 'c');
        pairs.Add('i', 'u');
        pairs.Add('t', 'd');
        pairs.Add('r', 'i');
        pairs.Add('a', 'm');
        pairs.Add('p', 'v');

        string pairsString = string.Empty;

        foreach (char c in lettersToCode)
        {

            char l = remainingLetters[UnityEngine.Random.Range(0, remainingLetters.Length - 1)];
            pairs.Add(c, l);
            remainingLetters = remainingLetters.Replace(l.ToString(), string.Empty);

            pairsString += c + " = " + l + ";";

        }

        pairsString = pairsString.Substring(0, pairsString.Length - 1);

        string crypted = string.Empty;

        foreach (char c in uncrypted)
        {
            try
            {
                crypted += pairs[c];
            }
            catch (System.Exception e)
            {
                crypted += " ";
            }
        }


        return (crypted, pairsString);
    }

    public void CryptedMessage()
    {
        (string, string) crypt = CryptMessage();
        _helpRPC.TriggerCryptedMessage(crypt.Item1, crypt.Item2);
    }

    int currentButton = -1;
    bool hasBeenPressed = false;

    public void SendMonitoring()
    {
        if (!hasBeenPressed) _helpRPC.TriggerMonitoringFeedback(false);

        hasBeenPressed = false;
        currentButton = Random.Range(1, 6);
        _helpRPC.TriggerMonitoringActiveButton(currentButton);
        Invoke("SendMonitoring", 10);
    }

    public void ButtonClicked(int id)
    {
        hasBeenPressed = true;
        _helpRPC.TriggerMonitoringFeedback(currentButton == id);
        currentButton = -1;
    }




    
}
