using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raycast : MonoBehaviour
{
    Camera camera;
    public Text text;
    string current_text;
    public bool isPlayerTrapped = false;
    public bool isEnemyTrapped = false;
    public bool isPlayerNear = false;
    bool doOnce = true;
    Ray ray;
    RaycastHit hit;
    
    void Start()
    {
        camera = FindObjectOfType<Camera>();
        text = GetComponentInChildren<Text>();
    }

    void Update()
    {
        ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            //if the mouse is on a cube then
            if (hit.transform.GetComponent<Cube>())
                current_text = "currently hovering on:" + hit.transform.name;
            else
                current_text = "";
        }
        else if (isPlayerNear)
        {
            //if the player moved to neighbouring cube of enemy
            current_text = " Player Moved to Neighbouring cube of enemy";

            //Show the UI confirmation for desired seconds
            //used doOnce so that Coroutine initated once.
            if (doOnce)
            {
                doOnce = false;
                StartCoroutine(Timer(1f)) ;
            }
        }
        //if the player has been trapped by the enemy, Send UI confirmation
        else if (isPlayerTrapped)
            current_text = " Player has been trapped by the Enemy";
        //if the Enemy has been trapped, then Send the UI confirmation
        else if (isEnemyTrapped)
            current_text = " Enemy has been trapped";
        else
            current_text = " ";

        text.text = current_text;
    }

    //Returns raycast hit
    public RaycastHit SendHit()
    {
        return hit;
    }

    //A small timer function
    IEnumerator Timer(float Seconds)
    {
        yield return new WaitForSeconds(Seconds);
        isPlayerNear = false;
        doOnce = true;
        
    }
}
