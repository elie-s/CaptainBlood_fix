using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RetroJam.CaptainBlood
{
    public class landing_Control : EventsManager
    {
        [SerializeField] GameManager manager;
        [SerializeField] TerrainGenerator[] terGen;
        [SerializeField] RectTransform UiImage;
        [SerializeField] Animator UiAnimator;
        [SerializeField] float cursorSensitivity;
        [Space]
        [SerializeField] Transform CurseurY;
        [SerializeField] TextMeshProUGUI rangeText;
        [SerializeField] GameObject[] speedBarBottom;

        [Space]
        [Header ("Value")]

        [SerializeField]float speed;
        [SerializeField]float verticalMultiplayer;
        [SerializeField]float moveVert;
        [SerializeField]float moveHori;
        [SerializeField]float moveFor;

        #region Propreties
        float moveHoriCursor;
        float y;
        float imageX;
        float pointA;
        float pointB;
        float currentObjective;
        float spawnLocation;
        float limiteLeft, limiteRight;
        [Range(0,4)] int spBrBtSm;
        public int distanceLeft;
        public int result;
        bool IsinZone = true;

        [SerializeField] bool active;
        bool ending;
        
        #endregion


        #region Static Speed

        [Space (20)]
         [SerializeField] float[] variableSpeed;
         [SerializeField, Range(0,5)] int indexSpeed;

         [Space]
         [Header ("Anti-AIR")]
         public bool antiAIR;
         [SerializeField] Image leftArrow;
         [SerializeField] Image rightArrow;
         [SerializeField] float antiAirSpeed;
         bool gotInput;
        #endregion

        private void Start()
        {
            StartLandingSettings();
            //active = false;
        }

        public override void StartLanding()
        {
            StartLandingSettings();
        }
        void StartLandingSettings()
        {
            
            indexSpeed = 1;
            Cursor.blocked = true;
            active = true;
            pointA = Random.Range(20, 900);
            pointB = pointA + 50;
            currentObjective = Random.Range(pointA + 10, pointB - 10);
            spawnLocation = Random.Range(pointA + 10, pointB - 10);
           
            limiteLeft = currentObjective - 0.5f;
            limiteRight = currentObjective + 0.5f;

            spBrBtSm = indexSpeed - 1;

            distanceLeft = (Random.Range(125,250));

            transform.localPosition = new Vector3(-1538,904,3569);
        }

        void Update()
        {
            
            LandingControl();
            if(!active) return;
            CameraBehavior();
            Curseur();
            SpeedFunction();
            UiRange();

            if (antiAIR == true){AntiAIR();}
            else {leftArrow.enabled = false; rightArrow.enabled = false;}

            for (int i = 0; i < terGen.Length; i++)
            {
                terGen[i].GetComponent<Terrain>().materialTemplate.mainTextureOffset = new Vector2(terGen[i].offsetY,terGen[i].offsetX);
            }
        }

        void LandingControl()
        {
            float oldMoveHori = moveHori;
            float oldMoveVert = moveVert;           

            if(active)
            {
                moveVert = Input.GetAxis("Vertical");
                moveHori += Input.GetAxis("Horizontal");
                moveHoriCursor = Input.GetAxis("Horizontal") * 2;
            }
            moveFor += /*(1-Mathf.Abs(Input.GetAxis("Forward")))* */ speed * variableSpeed[indexSpeed];

            ////////////////////////// Camera mouv
            y = transform.localPosition.y + moveVert * verticalMultiplayer;
            y = Mathf.Clamp(y, 300, 800);
            transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
            //

            for (int i = 0; i < terGen.Length; i++)
            {
                terGen[i].offsetY = spawnLocation;
                terGen[i].offsetY += moveHori * 0.05f;
                terGen[i].offsetX = moveFor;

                terGen[i].offsetY = Mathf.Clamp(terGen[i].offsetY, pointA, pointB);

                if (terGen[i].offsetY == pointA || terGen[i].offsetY == pointB)
                {moveHori = oldMoveHori;}

                ///////////////////////// Objectif Zone

                if(limiteLeft > terGen[i].offsetY)
                {
                    UiAnimator.SetBool("IsRight",true);
                    UiAnimator.SetBool("IsLeft",false);
                    IsinZone = false;
                }
                else if(limiteRight < terGen[i].offsetY)
                {
                    UiAnimator.SetBool("IsRight",false);
                    UiAnimator.SetBool("IsLeft",true);
                    IsinZone = false;
                }
                else 
                {
                    UiAnimator.SetBool("IsRight",false);
                    UiAnimator.SetBool("IsLeft",false);
                    IsinZone = true;
                }

                //

            }

        }
        void CameraBehavior()
        {
            Quaternion transRotate = transform.localRotation;
            transRotate.x = Mathf.Lerp(0, 16, (y*2) / 1600 );
            transRotate.y = 0;
            transRotate.z = 0;
            transRotate.w = 0;
            transform.localRotation = Quaternion.Euler(transRotate.x, 0f, 0f); 
        }
        void Curseur()
        {
            float fac = Mathf.Exp(-(Mathf.Pow(UiImage.localPosition.x/75,2)/(2*Mathf.Pow(.35f,2))));
            imageX = UiImage.localPosition.x + moveHoriCursor * fac;
            imageX = Mathf.Clamp(imageX, -100, 100);
            UiImage.localPosition = new Vector3(imageX, UiImage.localPosition.y, UiImage.localPosition.z);

            if (Input.GetAxis("Horizontal") == 0)
            {
                if (UiImage.localPosition.x > 0)
                {UiImage.localPosition = new Vector3(UiImage.localPosition.x - cursorSensitivity, UiImage.localPosition.y, UiImage.localPosition.z);}
            
                if (UiImage.localPosition.x < 0)
                {UiImage.localPosition = new Vector3(UiImage.localPosition.x + cursorSensitivity, UiImage.localPosition.y, UiImage.localPosition.z);}
            
            }

            Vector3 curY = CurseurY.localPosition;
            curY.x = CurseurY.localPosition.x;
            curY.y = (y * 100 /800) - 50;
            curY.z = CurseurY.localPosition.z;
            CurseurY.localPosition = curY;    
        }
        void SpeedFunction()
        {
            if(!gotInput)
            {
                if(Input.GetAxis("Forward") < -.5f)
                {
                    gotInput = true;
                    indexSpeed = Mathf.Clamp(indexSpeed-1, 1,5);
                    spBrBtSm = Mathf.Clamp(spBrBtSm-1, -1,3);
                }
                else if(Input.GetAxis("Forward") > .5f)
                {
                    gotInput = true;
                    indexSpeed = Mathf.Clamp(indexSpeed+1, 1,5);
                    spBrBtSm = Mathf.Clamp(spBrBtSm+1, -1,3);
                }
            }
            else
            {
                if(Input.GetAxis("Forward") > -.3f && Input.GetAxis("Forward") < .3f) gotInput = false;
            }


            for (int x = 0; x < speedBarBottom.Length; x++)
            {
                if(x <= spBrBtSm){speedBarBottom[x].SetActive(true);}
                else{speedBarBottom[x].SetActive(false);}
            }
            
        }
        void UiRange()
        {
            int distanceFlotant = distanceLeft;
            result = distanceFlotant - (int)moveFor;
            string ranger = result.ToString();
            rangeText.text = ranger;

            if (result < 1)
            {
                if(IsinZone)
                {
                    StartCoroutine(Slowing());
                    leftArrow.transform.localPosition = new Vector3(-103.1f,0,0);
                    rightArrow.transform.localPosition = new Vector3(103.1f,0,0);
                }
                else
                {
                    distanceLeft = (int)(1.35f *distanceLeft);
                }
            }

        }
        void AntiAIR()
        {
            float antiAirTime = Time.deltaTime * antiAirSpeed;

            Vector3 left = leftArrow.transform.localPosition;
            left.x = Mathf.Clamp(left.x, -105f, -13.3f);
            left.y = leftArrow.transform.localPosition.y;
            left.z = leftArrow.transform.localPosition.z;
            leftArrow.transform.localPosition = left;

            Vector3 right = rightArrow.transform.localPosition;
            right.x = Mathf.Clamp(right.x, 13.3f, 105f);
            right.y = rightArrow.transform.localPosition.y;
            right.z = rightArrow.transform.localPosition.z;
            rightArrow.transform.localPosition = right;

            if (leftArrow.transform.localPosition.x <= -100f) {leftArrow.enabled = false;}
            else {leftArrow.enabled = true;}
            if (rightArrow.transform.localPosition.x >= 100f) {rightArrow.enabled = false;}
            else {rightArrow.enabled = true;}

            //////////// Trigger Death
            if (leftArrow.transform.localPosition.x >= -20.0f)
            {
                Lose();
            }

            //////// Anti_AIR Speed
            if ( y <= 400 )
            {
                leftArrow.transform.Translate(-antiAirTime, 0, 0);
                rightArrow.transform.Translate(antiAirTime, 0, 0);
            }
            else 
            {
                leftArrow.transform.Translate(antiAirTime, 0, 0);
                rightArrow.transform.Translate(-antiAirTime, 0, 0);
            }
        }
        void OnCollisionEnter (Collision col)
        {
            if (col.gameObject.tag == "Terrain")
            {
                Lose();
            }
        }

        void Lose()
        {
            indexSpeed = 0;
            spBrBtSm = -1;

            leftArrow.transform.localPosition = new Vector3(-103.1f,0,0);
            rightArrow.transform.localPosition = new Vector3(103.1f,0,0);

            active = false;
            Cursor.blocked = false;
            manager.SetPhase(Phase.Planet);
            GameManager.events.CallPlayDestroySound();
        }

        IEnumerator Slowing()
        {
            active = false;
            ending = true;
            int curSpeed = indexSpeed;

            for (int i = curSpeed; i > 0; i--)
            {
                indexSpeed = i;
                yield return new WaitForSeconds(i/2);
            }

            indexSpeed = 0;

            ending = false;
            Cursor.blocked = false;
            manager.SetPhase(Phase.UpCom);
        }
          
    }

}