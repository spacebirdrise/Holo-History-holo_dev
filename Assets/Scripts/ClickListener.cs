using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickListener : MonoBehaviour
{
    public SWRStateMachine SWR;

    //public TextMesh debug1;
    //public TextMesh debug2;
    //public TextMesh debug3;

    int timeToWait = 0;
    enum WaitingStates{Waiting,NotWaiting }

    Material originalMaterial;

    int maximumTimeToWaitForClickedObject = 180;//frames

    WaitingStates currentState = WaitingStates.NotWaiting;
    // Start is called before the first frame update
    void Start()
    {
        originalMaterial = this.GetComponent<Renderer>().material;
        //Color test=new Color()
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == WaitingStates.NotWaiting) {
               //do nothing
        }

        if (currentState == WaitingStates.Waiting) {
            timeToWait--;
        //    debug2.text = "state : waiting";
        //    debug3.text = timeToWait.ToString();
            if (timeToWait < 0)
            {
                currentState = WaitingStates.NotWaiting;
                this.GetComponent<Renderer>().material = originalMaterial;
                //set material back to normal
                //set state to not waiting
            }
            else {
                //do nothing
            }
        }

    }

    public void changeToWaitingState() {
        timeToWait = maximumTimeToWaitForClickedObject;
        currentState = WaitingStates.Waiting;
    //    debug1.text = "called change to waiting state";
    }

    void OnSelect()
    {
       
        // send message to SWR with game object name
        //SWR.SendMessage("ObjectClicked", this.name);
        SWR.ObjectClicked(this);
        Debug.Log("called object clicked");
    }
}
