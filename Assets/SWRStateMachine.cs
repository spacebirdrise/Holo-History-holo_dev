using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SWRStateMachine : MonoBehaviour{

    //public GameObject DoleBox;
    //public GameObject CofferBox;
    //public GameObject CourtBox;

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

    private String[] riddleTexts1 = {   "Used by a person or for the whole household",
                                        "Used to help those in need",
                                        "Found in homes of wealthy ladies and lords" };
    private String[] riddleTexts2 = {   "It was more than where quilts were left fold",
                                        "Or used in a house with many to feed.",
                                        "An open display seemingly of cups holding boards" };
    private String[] riddleTexts3 = {   "Storage for all you wanted to stash and to keep",
                                        "Anonymous gifts or allowances lay",
                                        "A status symbol reflecting the style of the owner" };
    private String[] riddleTexts4 = {   "For the unlucky, it was also where they had to sleep",
                                        "Found within this cupboard is their pay.",
                                        "This furniture piece mainly stood in the dining parlor" };

    private AudioClip[] riddleAudios;
    private AudioClip[] answerAudios;


    //private bool riddleSolved;
    private bool guessWaiting;
    private int riddleCounter = 0;

    public TextMesh Riddle1;
    public TextMesh Riddle2;
    public TextMesh Riddle3;
    public TextMesh Riddle4;



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
        Riddle3.text = "";
        Riddle4.text = "";

        currentGameState = SWRStates.Idle;  // start in idle mode while waiting for intro
    }

    // Update is called once per frame
    void Update(){
        this.gameObject.GetComponent<Animator>().Play("Stop Walking");

        if (currentGameState == SWRStates.Intro)
        {
            if (!SWRAudioSource.isPlaying)
            {
                riddleCounter = 0;
                currentGameState = SWRStates.PresentRiddle;
            }
        }

        if (currentGameState == SWRStates.PresentRiddle)
        {

            Riddle1.text = riddleTexts1[riddleCounter]; // displays next riddle after incrementing riddle counter
            Riddle2.text = riddleTexts2[riddleCounter]; // displays next riddle after incrementing riddle counter
            Riddle3.text = riddleTexts3[riddleCounter]; // displays next riddle after incrementing riddle counter
            Riddle4.text = riddleTexts4[riddleCounter]; // displays next riddle after incrementing riddle counter

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
            Riddle3.text = "and finished the experience.";
            Riddle4.text = "You may click on Sir Walter's head to play again.";
        }
    }

    // receive message here with game object name. compare to current correct object
    public void ObjectClicked(ClickListener objectThatWasClicked)
    {
        String nameClicked = objectThatWasClicked.gameObject.name;
        if (nameClicked == "Intro")
        {
            SWRAudioSource.clip = intro_audio;
            SWRAudioSource.Play();
            currentGameState = SWRStates.Intro;
            // remove start box
        }

        else if(nameClicked == riddleObjects[riddleCounter])
        {
            Riddle1.text = "Correct! Well done!";
            Riddle2.text = "";
            Riddle3.text = "Now Sir Walter has some information";
            Riddle4.text = "about this object for you.";
            SWRAudioSource.clip = answerAudios[riddleCounter];
            riddleCounter += 1;
            SWRAudioSource.Play();

            currentGameState = SWRStates.CorrectAnswer;
           
            // control SWR if correct object is clicked
        }
       else if(nameClicked != riddleObjects[riddleCounter])
        {
            // code here for playing incorrect answer response
            Riddle1.text = "NOT CORRECT OBJECT DEBUG";
            Riddle2.text = "REMOVE THIS WHEN OBJECTS FLASH CORRECTLY";
            
            objectThatWasClicked.GetComponent<Renderer>().material.color = Color.red;
            objectThatWasClicked.changeToWaitingState();
           
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(3); // change parameter to change how long the code will wait for
    }

    public static class SWREnumHelpers
    {
        public static T GetEnumObjectByValue<T>(int valueId)
        {
            return (T)Enum.ToObject(typeof(T), valueId);
        }
    }
}
