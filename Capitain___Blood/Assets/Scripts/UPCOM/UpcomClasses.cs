using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace RetroJam.CaptainBlood.Lang
{
    public enum AnswerRequirements { none, Match, MatchPart, MatchSize, MatchWordsScrambled, Binary}

    [System.Serializable]
    public class Sentence
    {
        public Word[] words;
        public int size { get; private set; }

        
        public Sentence()
        {
            words = new Word[8];
            for (int i = 0; i < 8; i++)
            {
                words[i] = Word.none;
            }

            size = 0;
        }

        public void AddWord(Word _word)
        {
            if (size < 8)
            {
                words[size] = _word;
                size++;
            }
            else
            {
                Debug.Log("The Sentence " + this.ToString() + " is full. You can't add a new word.");
            }
        }

        public void RemoveWord()
        {
            if (size > 0)
            {
                words[size - 1] = Word.none;
                size--;
            }
            else
            {
                Debug.Log("The Sentence " + this.ToString() + " is empty. You can't remove any word.");
            }
        }

        public void Clean()
        {
            words = new Word[8];
            for (int i = 0; i < 8; i++)
            {
                words[i] = Word.none;
            }

            size = 0;
        }

        public static bool operator==(Sentence _sentenceA, Sentence _sentenceB)
        {
            for (int i = 0; i < 8; i++)
            {
                if(_sentenceA.words[i]!= _sentenceB.words[i]) return false;
            }

            return true;
        }

        public static bool operator==(Word[] _words, Sentence _sentence)
        {
            for (int i = 0; i < _words.Length; i++)
            {
                if(_words[i] != _sentence.words[i]) return false;
            }

            return true;
        }

        public static bool operator!=(Word[] _words, Sentence _sentence)
        {
            return !(_words == _sentence);
        }

        public static bool operator!=(Sentence _sentenceA, Sentence _sentenceB)
        {
            return !(_sentenceA == _sentenceB);
        }

        public override bool Equals(object obj)
        {
            return true;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public static Sentence nonSense 
        {
            get 
            { 
                Sentence result = new Sentence();
                result.AddWord(Word.You);
                result.AddWord(Word.Say);
                result.AddWord(Word.NonSense);
                return result;
            }
        }

        public static Sentence crazy
        {
            get
            {
                Sentence result = new Sentence();
                result.AddWord(Word.You);
                result.AddWord(Word.Great);
                result.AddWord(Word.Crazy);
                return result;
            }
        }
    }

    public class ContextBehaviors
    {
        public bool teleportable;
    }

    //[System.Serializable]
    public class Alien
    {
        public Word[] name { get; private set; }
        public Races race;
        public float sympathy;
        public Dictionary<Word, GlossaryValues> glossary;
        public Dialogue dialogue;
        public MissionType mission;
        public Vector2Int coordinates;

        public Alien(Vector2Int _coord)
        {
            coordinates = _coord;

            SetName();
            CreateGlossary();
            SetRace();

            mission = MissionType.none;
            sympathy = 0;
        }

        [JsonConstructor]
        public Alien(Word[] _name, float _sympathy, Dictionary<Word, GlossaryValues> _glossary, MissionType _mission, Vector2Int _coord, Races _race)
        {
            name = _name;
            sympathy = _sympathy;
            glossary = _glossary;
            mission = _mission;
            coordinates = _coord;
            race = _race;
        }

        public void SetName()
        {
            name = new Word[2];
            name[0] = (Word)Random.Range(2, 72);
            name[1] = (Word)Random.Range(2, 72);
        }

        public void SetRace()
        {
            race = (Races)Random.Range(0,15);
        }

        public void CreateGlossary()
        {
            glossary = new Dictionary<Word, GlossaryValues>();

            glossary.Add(Words.nouns[0], new GlossaryValues(Random.value < .66f ? Random.value + 1 : -2 + Random.value,0));
            glossary.Add(Words.nouns[1], new GlossaryValues(Random.value + 1, 0));

            for (int i = 2; i < Words.nouns.Count; i++)
            {

                if (Words.nouns[i] == name[0] || Words.nouns[i] == name[1])
                {
                    glossary.Add(Words.nouns[i], new GlossaryValues(Random.value + 1, 0));
                }
                else
                {
                    float value = Random.value < .66f ? Random.value + 1 : -2 + Random.value;
                    glossary.Add(Words.nouns[i], new GlossaryValues(value, 0));
                }
            }
        }

        //[System.Serializable]
        public class GlossaryValues
        {
            public float value;
            public int iterations;

            [JsonConstructor]
            public GlossaryValues(float _value, int _iterations)
            {
                value = _value;
                iterations = _iterations;
            }
        }
    }

    public struct Answer
    {
        public Sentence sentence;
        public SentenceCorrectness correctness;
        public SentenceConstruction construction;
        public Dictionary<WordFunction, List<Word>> structure;
        public float esteem;
        public bool negative;
    }

    [System.Serializable]
    public class AnswerCondition
    {
        public Word[] words;
        public AnswerRequirements requirements;

        [JsonConstructor]
        public AnswerCondition(Word[] _words, AnswerRequirements _requirements)
        {
            words = _words;
            requirements = _requirements;
        }

        public AnswerCondition(Word[] _words, int _requirements)
        {
            words = _words;
            requirements = (AnswerRequirements)_requirements;
        }

        public bool Check(Answer _answer)
        {
            bool result = false;

            Debug.Log(requirements);
            switch (requirements)
            {
                case AnswerRequirements.none:
                    return true;
                case AnswerRequirements.Match:
                    return words == _answer.sentence;
                case AnswerRequirements.MatchPart:
                    return _answer.sentence.Contains(words, true) && !_answer.negative;
                case AnswerRequirements.MatchSize:
                    return words.Length == _answer.sentence.size;
                case AnswerRequirements.MatchWordsScrambled:
                    return _answer.sentence.Contains(words) && !_answer.negative;
                default:
                    break;
            }

            return result;
        }

        public static AnswerCondition yes { get => new AnswerCondition(new Word[] { Word.Yes }, AnswerRequirements.Match); }
        public static AnswerCondition no { get => new AnswerCondition(new Word[] { Word.No }, AnswerRequirements.Match); }
    }
}
