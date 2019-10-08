using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RetroJam.CaptainBlood
{
    public class CoordManager : EventsManager
    {
        [SerializeField] public Vector2Int coord;
        [SerializeField] private Transform x;
        [SerializeField] private TextMeshProUGUI xScreen;
        [SerializeField] private Transform y;
        [SerializeField] private TextMeshProUGUI yScreen;
        [SerializeField] private float speed;

        private float xIndex;
        private float yIndex;

        // Start is called before the first frame update
        void Start()
        {
            coord = new Vector2Int(123, 62);
        }

        // Update is called once per frame
        void Update()
        {
            PrecisionHandler();
            SelectCoordinates();
            LineManager();
            ScreenManager();
        }

        public void ScreenManager()
        {
            xScreen.text = coord.x.ToString();
            yScreen.text = coord.y.ToString();
        }

        public void LineManager()
        {
            x.localPosition = new Vector3(coord.x-128, -64);
            y.localPosition = new Vector3(-160, coord.y-63);
        }

        public void SelectCoordinates()
        {
            if (Input.GetAxis("ScrollHorizontal") == 0 && Input.GetAxis("ScrollVertical") == 0) return;

            if (Input.GetAxis("ScrollHorizontal") != 0)
            {
                xIndex += Input.GetAxis("ScrollHorizontal");
            }
            if (Input.GetAxis("ScrollVertical") != 0)
            {
                yIndex += Input.GetAxis("ScrollVertical");
            }

            if(Mathf.Abs(xIndex) > speed)
            {
                coord += new Vector2Int(Mathf.RoundToInt(xIndex / speed),0);
                xIndex = 0;
            }
            if(Mathf.Abs(yIndex) > speed)
            {
                coord += new Vector2Int(0,Mathf.RoundToInt(yIndex / speed));
                yIndex = 0;
            }

            coord = new Vector2Int(Mathf.Clamp(coord.x, 0, 255), Mathf.Clamp(coord.y, 0, 125));
        }

        public void PrecisionHandler()
        {
            if(Input.GetButtonDown("Precision"))
            {
                if (speed == .5f) speed = 4;
                else speed = .5f;
            }
        }
    }
}