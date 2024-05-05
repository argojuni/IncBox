﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
#pragma warning disable 649
    //ref to the QuizGameUI script
    [SerializeField] private QuizGameUI quizGameUI;
    //ref to the scriptableobject file
    [SerializeField] private QuizDataScriptable dataScriptable;
    [SerializeField] private float timeInSeconds;
#pragma warning restore 649
    //questions data
    private List<Question> questions;
    //current question data
    private Question selectedQuetion = new Question();
    public int gameScore;
    private int lifesRemaining;
    private float currentTime;

    public GameController gm;

    private GameStatus gameStatus = GameStatus.NEXT;

    private void Start()
    {
        gameScore = 0;
        lifesRemaining = 3;
        currentTime = timeInSeconds;
        //set the questions data
        questions = new List<Question>();
        questions.AddRange(dataScriptable.questions);
        //select the question
        SelectQuestion();
        gameStatus = GameStatus.PLAYING;
    }

    /// <summary>
    /// Method used to randomly select the question form questions data
    /// </summary>
    private void SelectQuestion()
    {
        //get the random number
        int val = UnityEngine.Random.Range(0, questions.Count);
        //set the selectedQuetion
        selectedQuetion = questions[val];
        //send the question to quizGameUI
        quizGameUI.SetQuestion(selectedQuetion);
    }

    private void Update()
    {
        if (gameStatus == GameStatus.PLAYING)
        {
            currentTime -= Time.unscaledDeltaTime;
            SetTime(currentTime);
        }

        quizGameUI.ScoreText.text = "Point:" + gameScore;

    }

    void SetTime(float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(currentTime);                       //set the time value
        quizGameUI.TimerText.text = time.ToString("mm':'ss");   //convert time to Time format

        if (currentTime <= 0)
        {
            //Game Over
            gameStatus = GameStatus.NEXT;
            quizGameUI.GameOverPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Method called to check the answer is correct or not
    /// </summary>
    /// <param name="selectedOption">answer string</param>
    /// <returns></returns>
    public bool Answer(string selectedOption)
    {
        //set default to false
        bool correct = false;
        //if selected answer is similar to the correctAns
        if (selectedQuetion.correctAns == selectedOption)
        {
            //Yes, Ans is correct
            correct = true;
            gameScore += 1;
            quizGameUI.ScoreText.text = "Point:" + gameScore;
        }
        else if(selectedQuetion.correctAns != selectedOption)
        {
            //No, Ans is wrong
            //Reduce Life
            //lifesRemaining--;
            //quizGameUI.ReduceLife(lifesRemaining);

            //if (lifesRemaining == 0)
            //{
            //gameStatus = GameStatus.NEXT;
            gameStatus = GameStatus.PLAYING;
            gm.Die();
            Time.timeScale = 1;
            //    quizGameUI.GameOverPanel.SetActive(true);
            //}
        }

        if (gameStatus == GameStatus.PLAYING)
        {
            //call SelectQuestion method again after 1s
            Invoke("SelectQuestion", 0f);
        }
        //return the value of correct bool
        return correct;
    }
}

//Datastructure for storeing the quetions data
[System.Serializable]
public class Question
{
    public string questionInfo;         //question text
    public QuestionType questionType;   //type
    public Sprite questionImage;        //image for Image Type
    public AudioClip audioClip;         //audio for audio type
    public UnityEngine.Video.VideoClip videoClip;   //video for video type
    public List<string> options;        //options to select
    public string correctAns;           //correct option
}

[System.Serializable]
public enum QuestionType
{
    TEXT,
    IMAGE,
    AUDIO,
    VIDEO
}

[SerializeField]
public enum GameStatus
{
    PLAYING,
    NEXT
}