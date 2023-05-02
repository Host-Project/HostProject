using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CryptedMessageRidle : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> keys;

    public void SetKeys(string pairs)
    {
        string[] lst = pairs.Split(';');
        for(int i = 0; i < lst.Length; ++i)
        {
            Debug.Log(lst[i]);
            keys[i].text = lst[i];
        }
    }

}
