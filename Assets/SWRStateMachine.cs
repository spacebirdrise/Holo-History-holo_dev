using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SWRStateMachine : MonoBehaviour{

    public AudioSource SWRAudioSource;
    public AudioClip introduction;
    public AudioClip dole_riddle;
    public AudioClip dole_answer;


    private String[] riddleObjects = { "Dole", "Coffer" };
    private String currentCorrect;
    private bool riddleSolved;
    private bool guessWaiting;

    public TextMesh Riddle1;
    public TextMesh Riddle2;

    enum SWRStates
    {
        Walking,
        Speaking,
        PresentRiddle,
        Idle,
        CorrectAnswer,
        WrongAnswer,
        Outro
    };
    SWRStates currentGameState;

    // Start is called before the first frame update
    void Start()
    {
        currentGameState = SWRStates.PresentRiddle;
        currentCorrect = riddleObjects[0];
    }

    // Update is called once per frame
    void Update(){
        this.gameObject.GetComponent<Animator>().Play("Stop Walking");

        if (currentGameState == SWRStates.PresentRiddle)
        {
            SWRAudioSource.clip = dole_riddle;
            SWRAudioSource.Play();
            currentGameState = SWRStates.Speaking;
        }

        if (currentGameState == SWRStates.Speaking)
        {
            if (!SWRAudioSource.isPlaying && riddleSolved == false)
            {
                currentGameState = SWRStates.Idle;
            }
            if (!SWRAudioSource.isPlaying && riddleSolved == true)
            {
                currentGameState = SWRStates.Outro;
            }

        }

        if(currentGameState == SWRStates.Idle)
        {
            // code here in case you want to add anything for him to do while he waits for an answer
        }

        if(currentGameState == SWRStates.Outro)
        {
            Riddle1.text = "Finished all riddles";
            Riddle2.text = "End of experience";
        }
    }

    // receive message here with game object name. compare to current correct object
    public void ObjectClicked(String nameClicked)
    {
        if(nameClicked == currentCorrect)
        {
            Riddle1.text = "Correct!";
            Riddle2.text = "";

            currentGameState = SWRStates.Speaking;
            riddleSolved = true;
            SWRAudioSource.clip = dole_answer;
            SWRAudioSource.Play();


            // control SWR if correct object is clicked
        }
       if(nameClicked != currentCorrect)
        {
            // code here for playing incorrect answer response
            Riddle1.text = "NOT Correct!";
            Riddle2.text = "";
            // should look something like this
            //currentGameState = SWRStates.Speaking;
            //riddleSolved = false;
            //SWRAudioSource.clip = wrong_answer_clip;
        }
    }


    public static class SWREnumHelpers
    {
        public static T GetEnumObjectByValue<T>(int valueId)
        {
            return (T)Enum.ToObject(typeof(T), valueId);
        }
    }
}
