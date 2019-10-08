using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RetroJam.CaptainBlood
{
    public class Cursor : MonoBehaviour
    {
        public static bool blocked;

        [SerializeField, Range(1,40)] private float speed;
        public bool clicking;

        [SerializeField] private Animator animator;
        private Camera cam;

        [SerializeField] private int height = 87;

        public Vector2 Debug1;

        private void Awake()
        {
            cam = Camera.main;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(blocked) return;

            Move();

            AnimationManager();

            Debug1 = cam.WorldToScreenPoint(transform.position);
        }

        public void Move()
        {
            Vector3 pos = cam.WorldToScreenPoint(transform.position);

            if (Input.GetAxis("Horizontal") != 0)
            {
                if ((pos.x >= 5 && pos.x <= 320)
                    || (pos.x < 5 && Input.GetAxis("Horizontal") > 0)
                    || (pos.x > 310 && Input.GetAxis("Horizontal") < 0))
                {
                    transform.position += new Vector3(Input.GetAxis("Horizontal") > 0 ? Mathf.Pow(Input.GetAxis("Horizontal"), 2) : -Mathf.Pow(Input.GetAxis("Horizontal"),2),0, 0) * speed * Time.deltaTime;
                }
            }

            if (Input.GetAxis("Vertical") != 0)
            {
                if ((pos.y >= 2 && pos.y <= height)
                    || (pos.y < 2 && Input.GetAxis("Vertical") > 0)
                    || (pos.y > height && Input.GetAxis("Vertical") < 0))
                {
                    transform.position += new Vector3(0,Input.GetAxis("Vertical") > 0 ? Mathf.Pow(Input.GetAxis("Vertical"), 2) : -Mathf.Pow(Input.GetAxis("Vertical"), 2), 0) * speed * Time.deltaTime;
                }
            }

            pos = cam.WorldToScreenPoint(transform.position);

            transform.position = cam.ScreenToWorldPoint(new Vector3(Mathf.Clamp(pos.x, 5, 310), Mathf.Clamp(pos.y, 2, height), pos.z));


        }

        public void AnimationManager()
        {
            if(Input.GetButton("Select1") && !clicking)
            {
                clicking = true;
                animator.SetBool("used", true);
            }
            else if(clicking && !Input.GetButton("Select1"))
            {
                clicking = false;
                animator.SetBool("used", clicking);
            }
        }

        public void SetHeight(int _value)
        {
            height = Mathf.Clamp(_value, 2, 87);
        }
    } 
}