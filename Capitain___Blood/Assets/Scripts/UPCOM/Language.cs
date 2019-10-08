using System.Collections.Generic;
using UnityEngine;

namespace RetroJam.CaptainBlood.Lang
{

    public class LanguageException : System.Exception
    {
        public LanguageException(string message) : base(message) { }
    }

    public class Lexicon
    {
        public Dictionary<Word, float> glossary;

        public Lexicon()
        {
            for (int i = 0; i < Words.nouns.Count; i++)
            {
                glossary.Add(Words.nouns[i], UnityEngine.Random.value + 1);
            }
        }
    }

    public static class Language
    {
        public static SentenceCorrectness Correctness(this Sentence _sentence)
        {
            int adjCount = 0;
            int nounsCount = 0;
            int verbsCount = 0;
            int expressionsCount = 0;
            int nounsBeforeVerb = 0;
            int nounsAfterVerb = 0;
            int mainVerbIndex = 0;
            bool verbPassed = false;
            Word mainVerb = Word.none;

            for (int i = 0; i < _sentence.size; i++)
            {
                switch (_sentence.words[i].Nature())
                {
                    case WordNature.Noun:
                        nounsCount++;
                        if (!verbPassed) nounsBeforeVerb++;
                        else nounsAfterVerb++;
                        break;
                    case WordNature.Verb:
                        if (!verbPassed)
                        {
                            mainVerb = _sentence.words[i];
                            mainVerbIndex = i;
                        }
                        verbPassed = true;
                        verbsCount++;
                        break;
                    case WordNature.Expression:
                        expressionsCount++;
                        break;
                    case WordNature.Adjective:
                        adjCount++;
                        break;
                    default:
                        break;
                }
            }

            if (verbsCount == 0)
            {
                if (nounsCount == 0 && expressionsCount == 0) return SentenceCorrectness.none;
                else if (expressionsCount == 0 && adjCount > 0) return SentenceCorrectness.correct;
                else if (expressionsCount == 0) return SentenceCorrectness.needVerb;
                else return SentenceCorrectness.correct;
            }
            else if (verbsCount == 1 || (verbsCount == 2 && (mainVerb == Word.Want || mainVerb == Word.Like || mainVerb == Word.Know)))
            {
                if (nounsCount == 0) return SentenceCorrectness.none;
                else if (nounsBeforeVerb == 0) return SentenceCorrectness.needSubject;
                else if (nounsBeforeVerb < 3)
                    switch (Words.verbs[_sentence.words[mainVerbIndex]].constructions)
                    {
                        case SentenceConstruction.SVO:
                            if (nounsAfterVerb > 0) return SentenceCorrectness.correct;
                            else return SentenceCorrectness.needObject;
                        case SentenceConstruction.SVA:
                            if (nounsAfterVerb > 0) return SentenceCorrectness.correct;
                            else return SentenceCorrectness.needAdverb;
                        case SentenceConstruction.SVOO:
                            if (nounsAfterVerb > 1) return SentenceCorrectness.correct;
                            else return SentenceCorrectness.needObject;
                        default:
                            return SentenceCorrectness.needVerb;
                    }
                else return SentenceCorrectness.none;
            }
            else return SentenceCorrectness.none;
        }

        public static SentenceConstruction Construction(this Sentence _sentence)
        {
            WordNature[] structure = new WordNature[_sentence.size];
            Dictionary<WordNature, int> counts = new Dictionary<WordNature, int>();

            int verbIndex = -1;

            for (int i = 0; i < 7; i++)
            {
                counts.Add((WordNature)i, 0);
            }

            for (int i = 0; i < _sentence.size; i++)
            {
                WordNature nature = _sentence.words[i].Nature();

                structure[i] = nature;
                counts[nature]++;
                if (_sentence.words[i].Nature(WordNature.Verb) && verbIndex == -1) verbIndex = i;
            }

            if(counts[WordNature.Noun] == 0 && counts[WordNature.Adjective] == 0)
            {
                if (counts[WordNature.Expression] > 0) return SentenceConstruction.E;
                else return SentenceConstruction.none;
            }
            else if (counts[WordNature.Verb] > 0)
            {
                return _sentence.words[verbIndex].Type();
            }
            else
            {
                return SentenceConstruction.O;
            }
        }

        public static Dictionary<WordFunction, List<Word>> Structure(this Sentence _sentence)
        {
            Dictionary<WordFunction, List<Word>> result = new Dictionary<WordFunction, List<Word>>();

            SentenceConstruction construction = _sentence.Construction();

            result.Add(WordFunction.Subject, new List<Word>());
            result.Add(WordFunction.Action, new List<Word>());
            result.Add(WordFunction.Object, new List<Word>());
            //result.Add(WordFunction.Complement, new List<Word>());

            bool verbPassed = false;

            if(construction != SentenceConstruction.E  && construction != SentenceConstruction.O)
            {
                for (int i = 0; i < _sentence.size; i++)
                {
                    Word word = _sentence.words[i];

                    if(word.IsWord())
                    {
                        if(word.Nature(WordNature.Verb) && !verbPassed)
                        {
                            verbPassed = true;
                            result[WordFunction.Action].Add(word);
                        }
                        else if (!verbPassed)
                        {
                            result[WordFunction.Subject].Add(word);
                        }
                        else
                        { 
                            result[WordFunction.Object].Add(word);
                        }
                    }
                }
            }
            else if (construction == SentenceConstruction.O)
            {

            }

            return result;
        }

        public static float SentenceEsteem(this Sentence _sentence, Alien _alien)
        {
            float result = 0;
            float verb = 0;
            bool negative = false;

            const float gaussianFactorA = 2.5f;
            const float gaussianFactorC = 1.12f;

            for (int i = 0; i < _sentence.size; i++)
            {
                WordNature nature = _sentence.words[i].Nature();
                
                if (nature == WordNature.Noun)
                {
                    Alien.GlossaryValues word = _alien.glossary[_sentence.words[i]];

                    //float weight = -Mathf.Pow(word.iterations, 2) * .05f * word.value + word.value > 0 ? Mathf.Clamp(-Mathf.Pow(word.iterations, 2) * .05f * word.value + word.value, 0, 2) : Mathf.Clamp(-Mathf.Pow(word.iterations, 2) * .05f * word.value + word.value, -2, 0);
                    float weight = Mathf.Pow(gaussianFactorA, -(Mathf.Pow(word.iterations, 2) / (2 * Mathf.Pow(gaussianFactorC, 2)))) * word.value;
                    if (i == 0)
                    {
                        result += weight / 10;
                    }
                    else if(verb == 0 && _sentence.Construction() != SentenceConstruction.O)
                    {
                        if(_sentence.words[i-1].Nature(WordNature.Adjective))
                        {
                            result += weight / 10 * _sentence.words[i - 1].Value();
                        }
                        else
                        {
                            result += weight / 10;
                        }
                    }
                    else
                    {
                        if (_sentence.words[i - 1].Nature(WordNature.Adjective))
                        {
                            result += weight * _sentence.words[i - 1].Value();
                        }
                        else
                        {
                            result += weight;
                        }
                    }

                    word.iterations++;
                }
                else if(nature == WordNature.Verb && verb == 0)
                {
                    verb = _sentence.words[i].Value();
                }
                else if (nature == WordNature.Verb)
                {
                    result += _sentence.words[i].Value() > 0 ? _sentence.words[i].Value() +1 : -1 + _sentence.words[i].Value();
                }
                else if (_sentence.words[i] == Word.Not)
                {
                    negative = true;
                }
            }

            if (verb == 0) verb = 1;

            result *= verb * (negative ? -1 : 1);

            result = result * 8 / _sentence.size;

            return result;
        }

        public static Answer Answer(this Sentence _sentence, Alien _alien)
        {
            Answer result;
            result.sentence = _sentence;
            result.construction = _sentence.Construction();
            result.correctness = _sentence.Correctness();
            result.structure = _sentence.Structure();
            result.esteem = _sentence.SentenceEsteem(_alien);
            result.negative = _sentence.IsNegative();

            return result;
        }

        public static Speech SpeakAboutAnswer(this Alien _alien, Answer _answer)
        {
            List<Sentence> result = new List<Sentence>();

            if(!_answer.Contains(WordFunction.Object, Word.You) && !_answer.Contains(WordFunction.Object,Word.Me))
            {
                
                { // First Sentence Added
                    Sentence sentence = new Sentence();

                    sentence.AddWord(Word.Me);
                    if(_answer.esteem < 0) sentence.AddWord(Word.Not);
                    sentence.AddWord(Word.Like);
                    sentence.AddWord(_answer.structure[WordFunction.Action][0]);
                    for (int i = 0; i < _answer.structure[WordFunction.Object].Count; i++)
                    {
                        sentence.AddWord(_answer.structure[WordFunction.Object][i]);
                    }

                    result.Add(sentence);
                }

                { // Second Sentence Added
                    Sentence sentence = new Sentence();

                    for (int i = 0; i < Mathf.Clamp(_answer.structure[WordFunction.Object].Count,0,5); i++)
                    {
                        sentence.AddWord(_answer.structure[WordFunction.Object][i]);
                    }
                    sentence.AddWord(WordNature.Verb.Random());
                    if(Random.value > .6f) sentence.AddWord(WordNature.Adjective.Random());
                    sentence.AddWord(WordNature.Noun.Random());
                    
                    result.Add(sentence);
                }

                { // Third Sentence Added
                    Sentence sentence = new Sentence();

                    sentence.AddWord(result[1].Structure()[WordFunction.Action][0]);
                    for (int i = 0; i < result[1].Structure()[WordFunction.Object].Count; i++)
                    {
                        sentence.AddWord(result[1].Structure()[WordFunction.Object][i]);
                    }
                    if(result[1].SentenceEsteem(_alien) > 0) 
                    {
                        sentence.AddWord(Word.Good);
                        sentence.AddWord(Word.Good);
                        sentence.AddWord(Word.Laugh);
                    }
                    else
                    {
                        sentence.AddWord(Word.Bad);
                        sentence.AddWord(Word.Bad);
                        sentence.AddWord(Word.Sob);
                    }
                    
                    result.Add(sentence);
                }

                { // Forth Sentence Added
                    Sentence sentence = new Sentence();

                    sentence.AddWord(Word.You);
                    sentence.AddWord(Word.Know);
                    for (int i = 0; i < result[1].Structure()[WordFunction.Object].Count; i++)
                    {
                        sentence.AddWord(result[1].Structure()[WordFunction.Object][i]);
                    }
                    sentence.AddWord(Word.QuestionMark);
                    
                    result.Add(sentence);
                }

            }
            else if((_answer.Contains(WordFunction.Object, Word.You) && _answer.construction != SentenceConstruction.O) || (_answer.Contains(WordFunction.Subject, Word.You) && _answer.construction == SentenceConstruction.O))
            {
                if(_answer.esteem > 0)
                {
                    {// First Sentence Added
                    Sentence sentence = new Sentence();

                    sentence.AddWord(Word.You);
                    sentence.AddWord(Word.Great);
                    sentence.AddWord(Word.Pop);

                    result.Add(sentence);
                    }

                    {// Second Sentence Added
                    Sentence sentence = new Sentence();

                    sentence.AddWord(Word.Me);
                    sentence.AddWord(Word.Like);
                    sentence.AddWord(Word.Great);
                    sentence.AddWord(Word.Pop);

                    result.Add(sentence);
                    }
                }
                else
                {
                    {// First Sentence Added
                    Sentence sentence = new Sentence();

                    sentence.AddWord(Word.You);
                    sentence.AddWord(Word.Great);
                    sentence.AddWord(Word.Curse);

                    result.Add(sentence);
                    }
                    
                    {// Second Sentence Added
                    Sentence sentence = new Sentence();

                    sentence.AddWord(Word.Insult);
                    sentence.AddWord(Word.Insult);
                    sentence.AddWord(Word.Curse);
                    sentence.AddWord(Word.Pop);

                    result.Add(sentence);
                    }
                }

                {// Third Sentence Added
                    Sentence sentence = new Sentence();

                    sentence.AddWord(Word.Me);
                    if(_answer.esteem < 0) sentence.AddWord(Word.Not);
                    sentence.AddWord(Word.Like);
                    sentence.AddWord(Word.You);
                    sentence.AddWord(_answer.esteem < 0 ? Word.Sob : Word.Laugh);

                    result.Add(sentence);
                }

                {// Forth Sentence Added
                    Sentence sentence = new Sentence();

                    sentence.AddWord(Word.You);
                    sentence.AddWord(Random.value > .66f ? Word.Like : (Random.value > .5f ? Word.Know : Word.Want));
                    if(Random.value > .6f) sentence.AddWord(WordNature.Verb.Random());
                    if(Random.value > .6f) sentence.AddWord(WordNature.Adjective.Random());
                    sentence.AddWord(WordNature.Noun.Random());

                    result.Add(sentence);
                }
            }
            else
            {
                bool truth = Random.value < .75f;
                if(truth)
                {
                    {// First Sentence Added
                    Sentence sentence = new Sentence();

                    sentence.AddWord(_answer.esteem < 0 ? Word.Sob : Word.Laugh);
                    sentence.AddWord(_answer.esteem < 0 ? Word.Sob : Word.Laugh);

                    result.Add(sentence);
                    }

                    {// Second Sentence Added
                    Sentence sentence = new Sentence();

                    sentence.AddWord(Word.You);
                    sentence.AddWord(_answer.esteem < 0 ? Word.Poor : Word.Brave);
                    sentence.AddWord(Word.Pop);

                    result.Add(sentence);
                    } 
                }
                else
                {
                    {// First Sentence Added
                    Sentence sentence = new Sentence();

                    sentence.AddWord(Word.You);
                    sentence.AddWord(Word.Say);
                    sentence.AddWord(Word.Impossible);

                    result.Add(sentence);
                    }

                    {// First Sentence Added
                    Sentence sentence = new Sentence();

                    sentence.AddWord(Word.Me);
                    sentence.AddWord(Word.Not);
                    sentence.AddWord(Word.Like);
                    sentence.AddWord(Word.Impossible);

                    result.Add(sentence);
                    }
                }

                {// Third Sentence Added
                Sentence sentence = new Sentence();

                sentence.AddWord(Word.Me);
                if(_answer.esteem < 0) sentence.AddWord(Word.Not);
                sentence.AddWord(Word.Like);
                sentence.AddWord(Word.You);
                sentence.AddWord(_answer.esteem < 0 ? Word.Sob : Word.Laugh);

                result.Add(sentence);
                }

                {// Forth Sentence Added
                    Sentence sentence = new Sentence();

                    sentence.AddWord(Word.You);
                    sentence.AddWord(Random.value > .66f ? Word.Like : (Random.value > .5f ? Word.Know : Word.Want));
                    if(Random.value > .6f) sentence.AddWord(WordNature.Verb.Random());
                    if(Random.value > .6f) sentence.AddWord(WordNature.Adjective.Random());
                    sentence.AddWord(WordNature.Noun.Random());

                    result.Add(sentence);
                }
            }

            List<AnswerCondition> conditions = new List<AnswerCondition>();

            conditions.Add(AnswerCondition.yes);
            conditions.Add(new AnswerCondition(new Word[]{Word.Me, result[result.Count-1].Structure()[WordFunction.Action][0]}, AnswerRequirements.MatchPart));
            return new Speech(result.ToArray(), conditions.ToArray());
        }

        public static bool Contains(this Answer _answer, WordFunction _function, Word _word)
        {
            return _answer.structure[_function].Contains(_word);
        }
        public static bool IsNegative(this Sentence _sentence)
        {
            return _sentence.Contains(Word.No) || _sentence.Contains(Word.Not);
        }

        public static Sentence ReturnCoordinates(Vector2Int _coord)
        {
            return ReturnCoordinates(_coord.x, _coord.y);
        }

        public static Sentence ReturnCoordinates(int _x, int _y)
        {
            Sentence result = new Sentence();

            result.AddWord(Word.Coord);
            result.AddWord((Word)(111 + Mathf.FloorToInt(_x / 100)));
            result.AddWord((Word)(111 + Mathf.FloorToInt(_x / 10)- Mathf.FloorToInt(_x / 100)*10));
            result.AddWord((Word)(111 + _x - Mathf.FloorToInt(_x / 10)*10));
            result.AddWord(Word.OutOf);
            result.AddWord((Word)(111 + Mathf.FloorToInt(_y / 100)));
            result.AddWord((Word)(111 + Mathf.FloorToInt(_y / 10) - Mathf.FloorToInt(_y / 100)*10));
            result.AddWord((Word)(111 + _y - Mathf.FloorToInt(_y / 10)*10));

            return result;
        }

        public static Word[] ReturnCode(int _code)
        {
            if (_code.ToString().Length > 8 ) throw new LanguageException("The max length is 8 digits.");
            if (_code.ToString().Length == 0) throw new LanguageException("The min length is 1 digit.");

            Word[] result = new Word[_code.ToString().Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i]=(Word)(111+int.Parse(_code.ToString()[i].ToString()));
            }

            return result;
        }

        public static Word[] ReturnCode(string _code)
        {
            if (_code.Length > 8 ) throw new LanguageException("The max length is 8 digits.");
            if (_code.Length == 0) throw new LanguageException("The min length is 1 digit.");

            Word[] result = new Word[_code.Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i]=(Word)(111+int.Parse(_code[i].ToString()));
            }

            return result;
        }

        public static Word[] ReturnCode(int _code, bool _use8Digits)
        {
            if(!_use8Digits) return Language.ReturnCode(_code);

            List<Word> result= new List<Word>();
            for (int i = 0; i < 8-_code.ToString().Length; i++)
            {
                result.Add(Word.Zero);
            }

            Word[] tmp = ReturnCode(_code);
            for (int i = 0; i < tmp.Length; i++)
            {
                result.Add(tmp[i]);
            }

            return result.ToArray();
        }

        public static Sentence RandomSentenceSVO()
        {
            Sentence result = new Sentence();

            if (Random.value < .8f) result.AddWord(GetWordOfNature(WordNature.Adjective));
            result.AddWord(Words.nouns[Random.Range(0, Words.nouns.Count)]);
            if (Random.value < .5f) result.AddWord(Word.Not);
            result.AddWord(GetWordOfNature(WordNature.Verb));
            if (Random.value < .8f) result.AddWord(GetWordOfNature(WordNature.Adjective));
            result.AddWord(Words.nouns[Random.Range(0, Words.nouns.Count)]);

            return result;

            Word GetWordOfNature(WordNature _nature)
            {
                Word tmp;

                do
                {
                    tmp = (Word)Random.Range(0, Words.dictionary.Count);
                } while (Words.dictionary[tmp] != _nature);

                return tmp;

            }
        }

        
    }
}
