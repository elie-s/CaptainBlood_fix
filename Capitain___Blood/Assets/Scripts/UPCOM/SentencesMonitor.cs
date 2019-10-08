using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using RetroJam.CaptainBlood.Lang;
using TMPro;

namespace RetroJam.CaptainBlood
{
    public class SentencesMonitor : MonoBehaviour
    {
        [SerializeField] private Transform pointer;
        [SerializeField] private Tilemap monitorTM;
        [SerializeField] private TextMeshProUGUI textField;
        [SerializeField] private DialoguesManager manager;

        private Camera cam;

        public Vector3Int debugPos;

        /*private Dictionary<Vector3Int, Word> alienSentence = new Dictionary<Vector3Int, Word>();
        private Vector3Int[] alienField = new Vector3Int[8];
        private Dictionary<Vector3Int, Word> playerSentence = new Dictionary<Vector3Int, Word>();
        private Vector3Int[] playerField = new Vector3Int[8];*/

        private Monitor player;
        private Monitor alien;

        private Dictionary<Word, Tile> icons = new Dictionary<Word, Tile>();

        public Word mot;


        public class Monitor
        {
            public Dictionary<Vector3Int, Word> sentence = new Dictionary<Vector3Int, Word>();
            public Vector3Int[] field = new Vector3Int[8];
        }


        private void Awake()
        {
            cam = Camera.main;
        }
        // Start is called before the first frame update
        void Start()
        {
            InitializeSentences();
            InitializeTiles();
        }

        // Update is called once per frame
        void Update()
        {
            WriteSentence(player, manager.player);
            WriteSentence(alien, manager.alien);
            ReadSentences();
        }

        public void InitializeSentences()
        {
            alien = new Monitor();
            player = new Monitor();

            for (int i = 0; i < 8; i++)
            {
                alien.field[i] = new Vector3Int(-9 + i, 1, 0);
                player.field[i] = new Vector3Int(1 + i, 1, 0);

                alien.sentence[alien.field[i]] = Word.none;
                player.sentence[player.field[i]] = Word.none;
            }
        }

        public void InitializeTiles()
        {

            for (int i = 0; i < 120; i++)
            {
                icons[(Word)i] = Resources.Load<Tile>("Words/words_"+i);
            }
        }

        public void ReadSentences()
        {
            Vector3 _pos = pointer.position;

            textField.text = "";

            if (_pos.x < -7.15 || _pos.y < -1.5 || _pos.x > 7.2 || _pos.y > -0.6) return;

            Vector3Int cursor = monitorTM.WorldToCell(_pos);

            if (_pos.x < -0.8)
            {
                textField.text = alien.sentence[cursor].ToText();
            }
            else if (_pos.x > 0.85)
            {
                textField.text = player.sentence[cursor].ToText();
            }
        }

        public void WriteSentence(Monitor _monitor, Sentence _sentence)
        {
            for (int i = 0; i < 8; i++)
            {
                _monitor.sentence[_monitor.field[i]] = _sentence.words[i];
                monitorTM.SetTile(_monitor.field[i], icons[_sentence.words[i]]);
            }
        }

    }
}