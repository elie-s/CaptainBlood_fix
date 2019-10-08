using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using RetroJam.CaptainBlood.Lang;
using RetroJam.CaptainBlood.GalaxyLib;

namespace RetroJam.CaptainBlood.MissionsLib
{
        public class Mission
    {
        public Alien giver;
        public int currentPhase;
        public Vector2Int duplicateCoord;
        public MissionStatus status;


    }

    public class FindCode : Mission
    {
        public Part[] parts;
        public Word[] mainCode;

        public struct Part
        {
            public Vector2Int coord;
            public Alien alien;
            public Word[] code;
            public bool given;

            public void Initialize(Word[] _code)
            {
                alien = Galaxy.UnemployedAlien();
                coord = alien.coordinates;
                code = _code;
                given = false;

                List<Speech> speeches = new List<Speech>();
                Sentence sentence1 = new Sentence();
                Sentence sentence2 = new Sentence();
                Sentence sentence3 = new Sentence();
                Sentence sentence4 = new Sentence();

                sentence1.AddWord(Word.Howdy);
                sentence1.AddWord(Word.Brave);
                sentence1.AddWord(Word.Blood);
                sentence2.AddWord(Word.You);
                sentence2.AddWord(Word.Want);
                sentence2.AddWord(Word.Small);
                sentence2.AddWord(Word.Code);
                sentence3.AddWord(Word.Small);
                sentence3.AddWord(Word.Code);
                sentence3.AddWord(Word.Equal);
                sentence3.AddWord(_code[0]);
                sentence3.AddWord(_code[1]);
                if(_code.Length == 3) sentence3.AddWord(_code[2]);
                sentence4.AddWord(Word.Bye);
                sentence4.AddWord(Word.Laugh);
                sentence4.AddWord(Word.Laugh);

                Sentence[] sentences = new Sentence[4];
                sentences[0] = sentence1;
                sentences[1] = sentence2;
                sentences[2] = sentence3;
                sentences[3] = sentence4;


                SpeechConnexion connexion;
                connexion.trueStatement = 100;
                connexion.falseStatement = 100;


                speeches.Add(new Speech(sentences, new AnswerCondition[]{AnswerCondition.yes}));

                alien.dialogue = new Dialogue(speeches.ToArray(),new SpeechConnexion[]{connexion});

                Galaxy.inhabitants[coord] = alien;
            }
        }

        public FindCode()
        {
            giver = Galaxy.UnemployedAlien();
            giver.mission = MissionType.Code;

            Debug.Log("Mission FindCode given to the alien in: "+giver.coordinates.x+"/"+giver.coordinates.y+".");

            currentPhase = 0;
            duplicateCoord = Galaxy.SetDuplicate();
            status = MissionStatus.none;
            int tmpCode = Random.Range(0,99999999);
            mainCode = Language.ReturnCode(tmpCode);
            Debug.Log("Mission FindCode use the following code: "+tmpCode);

            parts = new Part[3];
            parts[0].Initialize(new Word[]{mainCode[0], mainCode[1], mainCode[2]});
            parts[1].Initialize(new Word[]{mainCode[3], mainCode[4]});
            parts[2].Initialize(new Word[]{mainCode[5], mainCode[6], mainCode[7]});

            giver.dialogue = SetUpDialogue();
        }

        public FindCode(SpeechSCO[] _speechesFiles, SpeechConnexionSCO _sco)
        {
            giver = Galaxy.UnemployedAlien();
            giver.mission = MissionType.Code;

            Debug.Log("Mission FindCode given to the alien in: "+giver.coordinates.x+"/"+giver.coordinates.y+".");

            currentPhase = 0;
            duplicateCoord = Galaxy.SetDuplicate();
            status = MissionStatus.none;
            int tmpCode = Random.Range(0,99999999);
            mainCode = Language.ReturnCode(tmpCode, true);
            Debug.Log("Mission FindCode use the following code: "+tmpCode);

            parts = new Part[3];
            parts[0].Initialize(new Word[]{mainCode[0], mainCode[1], mainCode[2]});
            parts[1].Initialize(new Word[]{mainCode[3], mainCode[4]});
            parts[2].Initialize(new Word[]{mainCode[5], mainCode[6], mainCode[7]});

            giver.dialogue = SetUpDialogue(_speechesFiles, _sco);
        }

        public Dialogue SetUpDialogue()
        {
            Object[] files = Resources.LoadAll("Speeches/FindCode");

            TextAsset[] speechesFiles = new TextAsset[files.Length-1];
            for (int i = 0; i < speechesFiles.Length; i++)
            {
                speechesFiles[i] = files[i] as TextAsset;
            }
            
            SpeechConnexion[] connexions = (files[files.Length-1] as SpeechConnexionSCO).connexions;

            Speech[] speeches = new Speech[speechesFiles.Length];

            for (int i = 0; i < speeches.Length; i++)
            {
                speeches[i] = JsonConvert.DeserializeObject<Speech>(speechesFiles[i].text);
            }
            
            speeches[2].condition[0].words = mainCode;
            speeches[3].sentences[4] = Language.ReturnCoordinates(duplicateCoord);
            speeches[4].sentences[5] = Language.ReturnCoordinates(parts[0].coord);
            speeches[4].sentences[7] = Language.ReturnCoordinates(parts[1].coord);
            speeches[4].sentences[9] = Language.ReturnCoordinates(parts[2].coord);
            speeches[5].condition[0].words = mainCode;

            return new Dialogue(speeches, connexions);
        }

        public Dialogue SetUpDialogue(TextAsset[] _speechesFiles, SpeechConnexionSCO _sco)
        {

            TextAsset[] speechesFiles = _speechesFiles;
            
            SpeechConnexion[] connexions = _sco.connexions;

            Speech[] speeches = new Speech[speechesFiles.Length];

            for (int i = 0; i < speeches.Length; i++)
            {
                speeches[i] = JsonConvert.DeserializeObject<Speech>(speechesFiles[i].text);
            }
            
            speeches[2].condition[0].words = mainCode;
            speeches[3].sentences[4].words = Language.ReturnCoordinates(duplicateCoord).words;
            speeches[4].sentences[5] = Language.ReturnCoordinates(parts[0].coord);
            speeches[4].sentences[7] = Language.ReturnCoordinates(parts[1].coord);
            speeches[4].sentences[9] = Language.ReturnCoordinates(parts[2].coord);
            speeches[5].condition[0].words = mainCode;

            return new Dialogue(speeches, connexions);
        }

        public Dialogue SetUpDialogue(SpeechSCO[] _speechesFiles, SpeechConnexionSCO _sco)
        {

            //SpeechSCO[] speechesFiles = _speechesFiles;
            
            SpeechConnexion[] connexions = _sco.connexions;

            Speech[] speeches = new Speech[_speechesFiles.Length];

            for (int i = 0; i < speeches.Length; i++)
            {
                speeches[i] = _speechesFiles[i].speech;
            }
            
            speeches[2].condition[0].words = mainCode;
            speeches[3].sentences[4].words = Language.ReturnCoordinates(duplicateCoord).words;
            speeches[4].sentences[5] = Language.ReturnCoordinates(parts[0].coord);
            speeches[4].sentences[7] = Language.ReturnCoordinates(parts[1].coord);
            speeches[4].sentences[9] = Language.ReturnCoordinates(parts[2].coord);
            speeches[5].condition[0].words = mainCode;

            return new Dialogue(speeches, connexions);
        }
    }
}