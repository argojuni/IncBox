﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizGameUI : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private QuizManager quizManager;               //ref to the QuizManager script
    [SerializeField] private Text scoreText, timerText;
    [SerializeField] private List<Image> lifeImageList;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Color correctCol, wrongCol, normalCol; //color of buttons
    [SerializeField] private Image questionImg;                     //image component to show image
    [SerializeField] private UnityEngine.Video.VideoPlayer questionVideo;   //to show video
    [SerializeField] private AudioSource questionAudio;             //audio source for audio clip
    [SerializeField] private Text questionInfoText;                 //text to show question
    [SerializeField] private List<Button> options;                  //options button reference

    [Header("EnemyControllerDie")]
    public EnemyManager enemyManager;
    public string enemyNameInput; // InputField untuk memasukkan nama musuh. ubah jadi type string jika tidak mau input

    public GameObject EnemyParticleDie;

    public GameController gameController;
    // Method untuk mengatur nama musuh.
#pragma warning restore 649

    private float audioLength;          //store audio length
    private Question question;          //store current question data
    private bool answered = false;      //bool to keep track if answered or not

    public Text TimerText { get => timerText; }                     //getter
    public Text ScoreText { get => scoreText; }                     //getter
    public GameObject GameOverPanel { get => gameOverPanel; }                     //getter

    private void Start()
    {   //add the listner to all the buttons
        for (int i = 0; i < options.Count; i++)
        {
            Button localBtn = options[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }
    }
    /// <summary>
    /// Method which populate the question on the screen
    /// </summary>
    /// <param name="question"></param>
    public void SetQuestion(Question question)
    {
        //set the question
        this.question = question;
        //check for questionType
        switch (question.questionType)
        {
            case QuestionType.TEXT:
                questionImg.transform.parent.gameObject.SetActive(false);   //deactivate image holder
                break;
            case QuestionType.IMAGE:
                questionImg.transform.parent.gameObject.SetActive(true);    //activate image holder
                questionVideo.transform.gameObject.SetActive(false);        //deactivate questionVideo
                questionImg.transform.gameObject.SetActive(true);           //activate questionImg
                questionAudio.transform.gameObject.SetActive(false);        //deactivate questionAudio

                questionImg.sprite = question.questionImage;                //set the image sprite
                break;
            case QuestionType.AUDIO:
                questionVideo.transform.parent.gameObject.SetActive(true);  //activate image holder
                questionVideo.transform.gameObject.SetActive(false);        //deactivate questionVideo
                questionImg.transform.gameObject.SetActive(false);          //deactivate questionImg
                questionAudio.transform.gameObject.SetActive(true);         //activate questionAudio
                
                audioLength = question.audioClip.length;                    //set audio clip
                StartCoroutine(PlayAudio());                                //start Coroutine
                break;
            case QuestionType.VIDEO:
                questionVideo.transform.parent.gameObject.SetActive(true);  //activate image holder
                questionVideo.transform.gameObject.SetActive(true);         //activate questionVideo
                questionImg.transform.gameObject.SetActive(false);          //deactivate questionImg
                questionAudio.transform.gameObject.SetActive(false);        //deactivate questionAudio

                questionVideo.clip = question.videoClip;                    //set video clip
                questionVideo.Play();                                       //play video
                break;
        }

        questionInfoText.text = question.questionInfo;                      //set the question text

        //suffle the list of options
        List<string> ansOptions = ShuffleList.ShuffleListItems<string>(question.options);

        //assign options to respective option buttons
        for (int i = 0; i < options.Count; i++)
        {
            //set the child text
            options[i].GetComponentInChildren<Text>().text = ansOptions[i];
            options[i].name = ansOptions[i];    //set the name of button
            options[i].image.color = normalCol; //set color of button to normal
        }

        answered = false;                       

    }

    public void ReduceLife(int remainingLife)
    {
        lifeImageList[remainingLife].color = Color.red;
    }

    /// <summary>
    /// IEnumerator to repeate the audio after some time
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayAudio()
    {
        //if questionType is audio
        if (question.questionType == QuestionType.AUDIO)
        {
            //PlayOneShot
            questionAudio.PlayOneShot(question.audioClip);
            //wait for few seconds
            yield return new WaitForSeconds(audioLength + 0.5f);
            //play again
            StartCoroutine(PlayAudio());
        }
        else //if questionType is not audio
        {
            //stop the Coroutine
            StopCoroutine(PlayAudio());
            //return null
            yield return null;
        }
    }

    /// <summary>
    /// Method assigned to the buttons
    /// </summary>
    /// <param name="btn">ref to the button object</param>
    void OnClick(Button btn)
    {
        //if answered is false
        if (!answered)
        {
            //set answered true
            answered = true;
            //get the bool value
            bool val = quizManager.Answer(btn.name);

            //if its true
            if (val)
            {
                //set color to correct
                btn.image.color = correctCol;

                DestroyEnemy();

            }
            else
            {
                //else set it to wrong color
                btn.image.color = wrongCol;
                HideFirstChild(gameObject);
                Time.timeScale = 1;

            }
        }
    }

    public void SetEnemyName(string name)
    {
        // Mengatur nama musuh pada input field.
        enemyNameInput = name;
    }

    // Method yang akan dipanggil saat tombol ditekan.
    public void DestroyEnemy()
    {
        // Mengambil nama musuh dari input field.
        string enemyName = enemyNameInput;
        Time.timeScale = 1f;

        if (enemyName == enemyNameInput)
        {
            // Dapatkan transform dari musuh berdasarkan nama
            Transform enemyTransform = GameObject.Find(enemyName).transform;

            // Instansiasi prefab EnemyParticleDie
            GameObject enemyParticleInstance = Instantiate(EnemyParticleDie, enemyTransform.position, enemyTransform.rotation);

            AudioManager.Instance.PlaySFX("die");

            Destroy(enemyParticleInstance, 0.5f);

            enemyManager.DestroyEnemyByName(enemyName);

            HideFirstChild(gameObject);
        }

    }

    public void HideFirstChild(GameObject parent)
    {
        // Mengambil komponen Transform dari parent.
        Transform parentTransform = parent.transform;

        // Jika parent memiliki setidaknya satu child.
        if (parentTransform.childCount > 0)
        {
            // Mengambil child pertama.
            GameObject firstChild = parentTransform.GetChild(0).gameObject;

            // Menyembunyikan child pertama.
            firstChild.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Parent tidak memiliki child.");
        }
    }

    public void RestryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
