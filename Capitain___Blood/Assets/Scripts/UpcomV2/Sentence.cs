using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RetroJam.CaptainBlood.Upcom
{
    [System.Serializable]
    public class Sentence
    {
        public Word[] words;
        public SentenceElement elementsHierarchy;
        public WordNature[] schema;

        public int[] nouns;
        public int[] adjectives;
        public int[] verbs;
        public int[] specials;

        public Sentence(params Word[] _words)
        {
            words = new Word[8];
            schema = new WordNature[_words.Length];

            List<int>[] naturesLists = CommonMethods.NewListArray<int>(4);


            for (int i = 0; i < 8; i++)
            {
                words[i] = i < _words.Length ? _words[i] : Word.none;

                if (i < schema.Length)
                {
                    schema[i] = _words[i].nature;

                    naturesLists[(int)schema[i]].Add(i);
                }
            }

            nouns = naturesLists[(int)WordNature.Noun].ToArray();
            adjectives = naturesLists[(int)WordNature.Adjective].ToArray();
            verbs = naturesLists[(int)WordNature.Verb].ToArray();
            specials = naturesLists[(int)WordNature.Special].ToArray();
        }

        //private void SetElements()
        //{
        //    List<Word> list = words.ToList();
        //    int hierarchy = 0;

        //    for (int i = 7; i >= 0; i--)
        //    {
        //        if (list[i] == Word.none) list.RemoveAt(i);
        //    }

        //    while (list.Count > 0)
        //    {
        //        elementsHierarchy = new SentenceElement(words[verbs[0]]);
        //    }
        //}

        private void SetElements()
        {
            Word[] copyWords = words;
            int mainVerbIndex = verbs[0];

            List<SentenceElement> elements = new List<SentenceElement>();


            for (int i = 0; i < nouns.Length; i++)
            {

            }
        }

        private SentenceConstruction FirstAnalysisSentenceConstruction()
        {
            State verb = VerbAnalysis();
            State subject = SubjectAnalysis();
            State sentenceObject = ObjectAnalysis();


            return SentenceConstruction.Error;

            SentenceConstruction ConstructionProcess()
            {
                if (verb == State.invalid) return SentenceConstruction.Error;

                int result = (int)subject * 100 + (int)verb * 10 + (int)sentenceObject;

                if (result > 10) return (SentenceConstruction)result;
                else if (nouns.Length > 0) return SentenceConstruction.O;
                else if (adjectives.Length > 0)
                {

                }

                return SentenceConstruction.Error;
            }

            #region Verbs Analysis
            State VerbAnalysis()
            {
                if (!VerbContinuity()) return State.invalid;

                switch (verbs.Length)
                {
                    case 0:
                        return State.none;
                    case 1:
                        return State.valid;
                    case 2:
                        if ((words[verbs[0]] as Verb).allowVerbAfter) return State.valid;
                        else return State.invalid;
                    default:
                        return State.invalid;
                }
            }

            bool VerbContinuity()
            {
                if (verbs.Length < 2) return true;
                else
                {
                    for (int i = 1; i < verbs.Length; i++)
                    {
                        if (verbs[i] - verbs[i - 1] != 1) return false;
                    }

                    return true;
                }
            }
            #endregion
            #region Subject Analysis
            State SubjectAnalysis()
            {
                if (nouns.Length == 0) return State.none;
                else if (nouns[0] < verbs.Last()) return State.valid;
                else return State.none;
            }
            #endregion
            #region Object Analysis
            State ObjectAnalysis()
            {
                if (nouns.Length == 0) return State.none;
                else if (nouns.Last() > verbs.Last()) return State.valid;
                else return State.none;
            }
            #endregion
        }

        enum State { invalid, valid, none };
    }

    [System.Serializable]
    public class SentenceElement
    {
        public Word word;
        public WordFunction function;
        public int index;
        public int hierarchy;
        public SentenceElement[] elements;

        public SentenceElement(Word _word, WordFunction _function)
        {
            word = _word;
            function = _function;
            elements = new SentenceElement[0];
            index = -1;
            hierarchy = -1;
        }

        public SentenceElement(Word _word, WordFunction _function, int _index, int _hierarchy)
        {
            word = _word;
            function = _function;
            elements = new SentenceElement[0];
            index = _index;
            hierarchy = _hierarchy;
        }
    }
}