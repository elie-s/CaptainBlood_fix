using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RetroJam.CaptainBlood.CursorLib
{
    [CreateAssetMenu(fileName ="Saving Buttons", menuName ="Buttons")]
    public class SaveButtons : ScriptableObject
    {
        public Button[] main;
        public Button[] galaxy;
        public Button[] planet;
        public Button[] landing;
        public Button[] upcom;
    }
}