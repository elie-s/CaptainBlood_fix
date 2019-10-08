using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RetroJam.CaptainBlood.MissionsLib;

namespace RetroJam.CaptainBlood
{
    public class MissionsManager : MonoBehaviour
    {
        [SerializeField] RetroJam.CaptainBlood.Lang.SpeechSCO[] files;
        [SerializeField] RetroJam.CaptainBlood.Lang.SpeechConnexionSCO sco;

        public FindCode missionFindCode;

        // Start is called before the first frame update
        void Start()
        {
            missionFindCode = new FindCode(files, sco);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}