using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickListener : MonoBehaviour
{
    public SWRStateMachine SWR;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSelect()
    {
       
        // send message to SWR with game object name
        //SWR.SendMessage("ObjectClicked", this.name);
        SWR.ObjectClicked(this.gameObject.name);
        Debug.Log("called object clicked");
    }
}
