using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using RetroJam.CaptainBlood.Lang;

namespace RetroJam.CaptainBlood
{
    public class Keyboard : MonoBehaviour
    {
        [SerializeField, Range(1, 40)] private float speed;
        [SerializeField] private Transform pointer;
        [SerializeField] private Tilemap tm;
        [SerializeField] private Tilemap monitorTM;
        [SerializeField] private TextMeshProUGUI textField;
        [SerializeField] DialoguesManager manager;

        private Dictionary<Vector3Int, Word> dictionary = new Dictionary<Vector3Int, Word>();

        private Camera cam;

        public Vector3Int debugPos;

        private Monitor player;
        private Monitor alien;

        private Dictionary<Word, Tile> icons = new Dictionary<Word, Tile>();

        public Word mot;


        public class Monitor
        {
            public Dictionary<Vector3Int, Word> sentence = new Dictionary<Vector3Int, Word>();
            public Vector3Int[] field = new Vector3Int[8];
        }



        // Start is called before the first frame update
        void Start()
        {
            cam = Camera.main;

            InitializeKeyboard();
            InitializeSentences();
            InitializeTiles();
        }

        // Update is called once per frame
        void Update()
        {
            Scroll();

            WriteSentence(player, manager.player);
            WriteSentence(alien, manager.alien);

            DebugMousePos();

            Interact();
        }

        public void Scroll()
        {
            if (Input.GetAxis("ScrollHorizontal") != 0)
            {
                transform.position -= new Vector3(Input.GetAxis("ScrollHorizontal") > 0 ? Mathf.Pow(Input.GetAxis("ScrollHorizontal"), 2) : -Mathf.Pow(Input.GetAxis("ScrollHorizontal"), 2), 0, 0) * speed * Time.deltaTime;
            }

            if (transform.localPosition.x > 0 || transform.localPosition.x < -736)
            {
                transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, -736, 0), transform.localPosition.y, transform.localPosition.z);
            }
        }

        public void DebugMousePos()
        {
            Vector3 cursor = cam.ScreenToWorldPoint(Input.mousePosition);

            debugPos = monitorTM.WorldToCell(cursor);

            if(Input.GetMouseButtonDown(0))
            {
                Debug.Log(dictionary[debugPos]);
            }
        }

        public void InitializeKeyboard()
        {
            for (int i = 1; i < 120; i+=2)
            {
                dictionary[new Vector3Int(-7 + Mathf.FloorToInt(i / 2), -3, 0)] = (Word)i;
                dictionary[new Vector3Int(-7 + Mathf.FloorToInt(i / 2), -4, 0)] = (Word)i+1;
            }

            //dictionary[new Vector3Int(-7 + Mathf.FloorToInt(121 / 2), -4, 0)] = (Word)121;
        }

        public void InitializeSentences()
        {
            alien = new Monitor();
            player = new Monitor();

            for (int i = 0; i < 8; i++)
            {
                alien.field[i] = new Vector3Int(-9 + i, -2, 0);
                player.field[i] = new Vector3Int(1 + i, -2, 0);

                alien.sentence[alien.field[i]] = Word.none;
                player.sentence[player.field[i]] = Word.none;
            }
        }

        public void InitializeTiles()
        {

            for (int i = 0; i < 121; i++)
            {
                icons[(Word)i] = Resources.Load<Tile>("Words/words_" + i);
            }
        }

        public void Selection(Vector3 _pos)
        {
            

            if (_pos.x < -5.6 || _pos.y < -3.7 || _pos.x > 5.6 || _pos.y > -1.9) return;

            Vector3Int cursor = tm.WorldToCell(_pos);

            textField.text = dictionary[cursor].ToText();

            if (Input.GetButtonDown("Select1")) manager.player.AddWord(dictionary[cursor]);

        }

        public void Remove()
        {
            if (pointer.position.x < 5.95 || pointer.position.y < -2.7 || pointer.position.x > 7 || pointer.position.y > -1.9) return;

            if(Input.GetButtonDown("Select1"))
            {
                manager.player.RemoveWord();
            }
        }

        public void Interact()
        {
            textField.text = "";

            Selection(pointer.position);
            ReadSentences();
            Remove();

        }

        public void ReadSentences()
        {
            Vector3 _pos = pointer.position;

            

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