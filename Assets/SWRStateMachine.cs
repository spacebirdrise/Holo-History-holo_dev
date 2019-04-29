using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SWRStateMachine : MonoBehaviour{

    public AudioSource SWRAudioSource;
    public AudioClip intro_audio;
    //public AudioClip outro_audio;

    public AudioClip coffer_riddle;
    public AudioClip coffer_answer;

    public AudioClip dole_riddle;
    public AudioClip dole_answer;

    public AudioClip court_riddle;
    public AudioClip court_answer;

    private String[] riddleObjects = { "Coffer", "Dole", "Court" };
    private String[] riddleTexts = { "coffer riddle text here", "dole riddle text here", "court riddle text here" };
    private AudioClip[] riddleAudios;
    private AudioClip[] answerAudios;


    //private bool riddleSolved;
    private bool guessWaiting;
    private int riddleCounter = 0;

    public TextMesh Riddle1;
    public TextMesh Riddle2;

    enum SWRStates
    {
        Intro,
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
        riddleAudios = new AudioClip[]{ coffer_riddle, dole_riddle, court_riddle };
        answerAudios = new AudioClip[]{ coffer_answer, dole_answer, court_answer };

        Riddle1.text = "Welcome to Sir Walter's Scavenger Hunt!";
        Riddle2.text = "Please click on Sir Walter's head to begin";

        currentGameState = SWRStates.Idle;  // start in idle mode while waiting for intro
    }

    // Update is called once per frame
    void Update(){
        this.gameObject.GetComponent<Animator>().Play("Stop Walking");

        if (currentGameState == SWRStates.Intro)
        {
            if (!SWRAudioSource.isPlaying)
            {
                currentGameState = SWRStates.PresentRiddle;
            }
        }

        if (currentGameState == SWRStates.PresentRiddle)
        {

            Riddle1.text = riddleTexts[riddleCounter]; // displays next riddle after incrementing riddle counter
            Riddle2.text = "";

            SWRAudioSource.clip = riddleAudios[riddleCounter];
            SWRAudioSource.Play();
            currentGameState = SWRStates.Speaking;
        }

        if( currentGameState == SWRStates.CorrectAnswer)
        {

            if (!SWRAudioSource.isPlaying && riddleCounter < riddleAudios.Length)
            {
                currentGameState = SWRStates.PresentRiddle;
            }
            else if (!SWRAudioSource.isPlaying)
            {
                //SWRAudioSource.clip = outro_audio;
                //SWRAudioSource.Play();
                currentGameState = SWRStates.Outro;
            }
        }

        if (currentGameState == SWRStates.Speaking)
        {
            if (!SWRAudioSource.isPlaying)
            {
                currentGameState = SWRStates.Idle;
            }
        }

        if(currentGameState == SWRStates.Idle)
        {
            // code here in case you want to add anything for him to do while he waits for an answer
        }

        if(currentGameState == SWRStates.Outro)
        {
            Riddle1.text = "Congratulations!";
            Riddle2.text = "You've solved all of Sir Walter's Riddles";
        }
    }

    // receive message here with game object name. compare to current correct object
    public void ObjectClicked(String nameClicked)
    {
        if(nameClicked == "Intro")
        {
            SWRAudioSource.clip = intro_audio;
            SWRAudioSource.Play();
            currentGameState = SWRStates.Intro;
            // remove start box
        }

        if(nameClicked == riddleObjects[riddleCounter])
        {

            SWRAudioSource.clip = answerAudios[riddleCounter];
            riddleCounter += 1;
            SWRAudioSource.Play();

            currentGameState = SWRStates.CorrectAnswer;
           
            // control SWR if correct object is clicked
        }
       if(nameClicked != riddleObjects[riddleCounter])
        {
            // code here for playing incorrect answer response
            Riddle1.text = "NOT Correct!";
            Riddle2.text = "";
            GameObject.Find(nameClicked).GetComponent<Renderer>().material.color = Color.red;
            new WaitForSecondsRealtime(3);
            GameObject.Find(nameClicked).GetComponent<Renderer>().material.color = Color.white;

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
