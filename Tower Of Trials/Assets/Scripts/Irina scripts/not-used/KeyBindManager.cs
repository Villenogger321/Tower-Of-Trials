using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class KeyBindManager : MonoBehaviour
{
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    public TMP_Text up, down, left, right, shoot, dash, use;

    private GameObject currentKey;

    public EventSystem eventSystem;

    private void Start()
    {
        keys.Add("up", KeyCode.W);
        keys.Add("down", KeyCode.S);
        keys.Add("left", KeyCode.A);
        keys.Add("right", KeyCode.D);
        keys.Add("shoot", KeyCode.R);
        keys.Add("dash", KeyCode.Q);
        keys.Add("use", KeyCode.E);

        up.text = keys["up"].ToString();
        down.text = keys["down"].ToString();
        left.text = keys["left"].ToString();
        right.text = keys["right"].ToString();
        shoot.text = keys["shoot"].ToString();
        dash.text = keys["dash"].ToString();
        use.text = keys["use"].ToString();
    }

    private void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<TMP_Text>().text = e.keyCode.ToString();
                currentKey = null;
            }
        }
        if (Input.anyKeyDown)
        {
            Invoke("EnableInput", 1f);
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        currentKey = clicked;
    }

    void EnableInput()
    {
        Debug.Log(".");
        eventSystem.enabled = true;
    }
}
