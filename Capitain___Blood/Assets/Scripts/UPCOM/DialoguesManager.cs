using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using RetroJam.CaptainBlood.Lang;
using RetroJam.CaptainBlood.CursorLib;

namespace RetroJam.CaptainBlood
{
    public class DialoguesManager : EventsManager
    {
        [SerializeField] public Sentence alien;
        [SerializeField] public Sentence player;
        [SerializeField] GameManager manager;
        [SerializeField] TextAsset jsonFile;
        [SerializeField] Transform cursor;
        [SerializeField] Animator mouth;

        Speech alienSpeech;
        Dialogue dialogue;

        Button button;

        public List<Word> subject = new List<Word>();
        public List<Word> action = new List<Word>();
        public List<Word> @object = new List<Word>();
        public List<Word> complement = new List<Word>();

        public bool isWriting;
        public bool waitingAnswer;
        Queue<Sentence> alienSpeechSentences = new Queue<Sentence>();

        private void Awake()
        {
            //alienSpeech = JsonConvert.DeserializeObject<Speech>(jsonFile.text);
            button = new Button(new Vector2(-.75f, -1.75f), new Vector2(.8f, -.35f));
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R)) ReadPlayerSentence();
            AlienSpeechManager();
            if(alienSpeech != null)MouthManager();
        }

        public override void SetDialogueOfAlien()
        {
            SetDialogue(manager.alien.dialogue);
        }

        public void MouthManager()
        {
            if(alienSpeech.status != SpeechStatus.Said && !isWriting && !mouth.GetBool("used")) mouth.SetBool("used", true);
            else if (mouth.GetBool("used")) mouth.SetBool("used", false);
        }

        public void ReadPlayerSentence()
        {
            Debug.Log(player.Correctness());
            Debug.Log(player.SentenceEsteem(manager.alien));
            if (player.Correctness() != SentenceCorrectness.none)
            {
                Debug.Log(player.Construction());
                DebugStructure();
            }
            player.Clean();
        }

        public void TestGeneratedSpeech()
        {
            if(player.size > 0)
            {
                alienSpeech = Language.SpeakAboutAnswer(manager.alien, player.Answer(manager.alien));
                player.Clean();
                AlienKeyboard(alienSpeech);
            }
            else
            {
                AlienKeyboard(alienSpeech);
            }
        }

        public void SetDialogue(Dialogue _dialogue)
        {
            dialogue = _dialogue;
            SetSpeech();
        }

        public void SetSpeech()
        {
            alienSpeech = dialogue.currentSpeech;
        }

        public void GetAnswer()
        {
            Answer currentAnswer = player.Answer(manager.alien);

            dialogue.Answering(currentAnswer);
            player.Clean();
        }

        public void DebugStructure()
        {
            Dictionary<WordFunction, List<Word>> dico = player.Structure();

            subject = dico[WordFunction.Subject];
            action = dico[WordFunction.Action];
            @object = dico[WordFunction.Object];
            complement = dico[WordFunction.Complement];
        }

        public void AlienSpeechManager()
        {
            if (Input.GetButtonDown("Select1") && button.IsCursorOver(cursor)) 
            {
                if(dialogue.finished)
                {
                    TestGeneratedSpeech();
                }
                else
                {
                    if(alienSpeech.status != SpeechStatus.Said) AlienKeyboard(alienSpeech);
                    else if (player.size > 0) 
                    {
                        GetAnswer();
                        SetSpeech();
                        AlienKeyboard(alienSpeech);
                    }
                }
            }
        }

        public void AlienKeyboard(Speech _speech)
        {
            if (!isWriting) StartCoroutine(InsertWords(alienSpeech.Read()));
        }

        public void AddAlienWord(Word _word)
        {
            alien.AddWord(_word);
        }

        public IEnumerator InsertWords(Sentence _sentence)
        {
            isWriting = true;
            Cursor.blocked = true;
            alien.Clean();

            for (int i = 0; i < 8; i++)
            {
                AddAlienWord(_sentence.words[i]);

                yield return new WaitForSeconds(.35f);
            }

            isWriting = false;
            Cursor.blocked = false;
        }
    }
}