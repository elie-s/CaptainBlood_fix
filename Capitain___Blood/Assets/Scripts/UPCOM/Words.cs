using System.Collections.Generic;

namespace RetroJam.CaptainBlood.Lang
{
    public static class Words
    {
        public static Dictionary<Word, WordNature> dictionary { get; private set; }

        public static Dictionary<Word, Verb> verbs { get; private set; }

        public static Dictionary<Word, Adjective> adjectives { get; private set; }

        public static List<Word> nouns { get; private set; }

        public class Verb
        {
            public VerbType type;
            public int valency;
            public SentenceConstruction constructions;
            public float factor;

            public Verb(VerbType _type, int _valency, SentenceConstruction _constructions, float _factor)
            {
                type = _type;
                valency = _valency;
                constructions = _constructions;
                factor = _factor;
            }
        }

        public class Adjective
        {
            public float factor;

            public Adjective(float _value)
            {
                factor = _value;
            }
        }

        public static void InitializeWords()
        {
            dictionary = new Dictionary<Word, WordNature>();
            verbs = new Dictionary<Word, Verb>();
            adjectives = new Dictionary<Word, Adjective>();
            nouns = new List<Word>();

            for (int i = 1; i < 121; i++)
            {
                switch ((Word)i)
                {
                    case Word.QuestionMark:
                        dictionary.Add((Word)i, WordNature.Ponctuation);
                        break;
                    case Word.Not:
                        dictionary.Add((Word)i, WordNature.Negation);
                        break;
                    case Word.Yes:
                        dictionary.Add((Word)i, WordNature.Expression);
                        break;
                    case Word.No:
                        dictionary.Add((Word)i, WordNature.Expression);
                        break;
                    case Word.Me:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.You:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Howdy:
                        dictionary.Add((Word)i, WordNature.Expression);
                        break;
                    case Word.Bye:
                        dictionary.Add((Word)i, WordNature.Expression);
                        break;
                    case Word.Go:
                        dictionary.Add((Word)i, WordNature.Verb);
                        verbs.Add((Word)i, new Verb(VerbType.Intransitive, 1, SentenceConstruction.SVA, .25f));
                        break;
                    case Word.Want:
                        dictionary.Add((Word)i, WordNature.Verb);
                        verbs.Add((Word)i, new Verb(VerbType.Transitive, 2, SentenceConstruction.SVO, .25f));
                        break;
                    case Word.Teleport:
                        dictionary.Add((Word)i, WordNature.Verb);
                        verbs.Add((Word)i, new Verb(VerbType.Transitive, 2, SentenceConstruction.SVO, .25f));
                        break;
                    case Word.Give:
                        dictionary.Add((Word)i, WordNature.Verb);
                        verbs.Add((Word)i, new Verb(VerbType.Ditransitive, 3, SentenceConstruction.SVOO, .75f));
                        break;
                    case Word.Like:
                        dictionary.Add((Word)i, WordNature.Verb);
                        verbs.Add((Word)i, new Verb(VerbType.Transitive, 2, SentenceConstruction.SVO, 1f));
                        break;
                    case Word.Say:
                        dictionary.Add((Word)i, WordNature.Verb);
                        verbs.Add((Word)i, new Verb(VerbType.Transitive, 2, SentenceConstruction.SVOO, .25f));
                        break;
                    case Word.Know:
                        dictionary.Add((Word)i, WordNature.Verb);
                        verbs.Add((Word)i, new Verb(VerbType.Transitive, 2, SentenceConstruction.SVO, .5f));
                        break;
                    case Word.Unknown:
                        dictionary.Add((Word)i, WordNature.Adjective);
                        adjectives.Add((Word)i, new Adjective(.8f));
                        break;
                    case Word.Play:
                        dictionary.Add((Word)i, WordNature.Verb);
                        verbs.Add((Word)i, new Verb(VerbType.Intransitive, 1, SentenceConstruction.SVO, .5f));
                        break;
                    case Word.Search:
                        dictionary.Add((Word)i, WordNature.Verb);
                        verbs.Add((Word)i, new Verb(VerbType.Transitive, 2, SentenceConstruction.SVO, .5f));
                        break;
                    case Word.Race:
                        dictionary.Add((Word)i, WordNature.Verb);
                        verbs.Add((Word)i, new Verb(VerbType.Intransitive, 1, SentenceConstruction.SVO, .25f));
                        break;
                    case Word.Vote:
                        dictionary.Add((Word)i, WordNature.Verb);
                        verbs.Add((Word)i, new Verb(VerbType.Transitive, 2, SentenceConstruction.SVO, .25f));
                        break;
                    case Word.Help:
                        dictionary.Add((Word)i, WordNature.Verb);
                        verbs.Add((Word)i, new Verb(VerbType.Transitive, 2, SentenceConstruction.SVO, .75f));
                        break;
                    case Word.Disarm:
                        dictionary.Add((Word)i, WordNature.Verb);
                        verbs.Add((Word)i, new Verb(VerbType.Transitive, 2, SentenceConstruction.SVO, .25f));
                        break;
                    case Word.Laugh:
                        dictionary.Add((Word)i, WordNature.Expression);
                        break;
                    case Word.Sob:
                        dictionary.Add((Word)i, WordNature.Expression);
                        break;
                    case Word.Fear:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Destroy:
                        dictionary.Add((Word)i, WordNature.Verb);
                        verbs.Add((Word)i, new Verb(VerbType.Transitive, 2, SentenceConstruction.SVO, -1f));
                        break;
                    case Word.Free:
                        dictionary.Add((Word)i, WordNature.Adjective);
                        adjectives.Add((Word)i, new Adjective(1.5f));
                        break;
                    case Word.Kill:
                        dictionary.Add((Word)i, WordNature.Verb);
                        verbs.Add((Word)i, new Verb(VerbType.Transitive, 2, SentenceConstruction.SVO, -.75f));
                        break;
                    case Word.Prison:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Prisonner:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Trap:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Danger:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Forbidden:
                        dictionary.Add((Word)i, WordNature.Adjective);
                        adjectives.Add((Word)i, new Adjective(.1f));
                        break;
                    case Word.Radioactivity:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Impossible:
                        dictionary.Add((Word)i, WordNature.Adjective);
                        adjectives.Add((Word)i, new Adjective(.1f));
                        break;
                    case Word.Bounty:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Information:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.NonSense:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.RDV:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Time:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Urgent:
                        dictionary.Add((Word)i, WordNature.Adjective);
                        adjectives.Add((Word)i, new Adjective(.9f));
                        break;
                    case Word.Idea:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Missile:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Code:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Friend:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Ennemy:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Spirit:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Brain:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Warrior:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.President:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Scientist:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Genetic:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Sex:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Reproduction:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Male:
                        dictionary.Add((Word)i, WordNature.Adjective);
                        adjectives.Add((Word)i, new Adjective(1f));
                        break;
                    case Word.Female:
                        dictionary.Add((Word)i, WordNature.Adjective);
                        adjectives.Add((Word)i, new Adjective(1f));
                        break;
                    case Word.Identity:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Pop:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.People:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Different:
                        dictionary.Add((Word)i, WordNature.Adjective);
                        adjectives.Add((Word)i, new Adjective(1f));
                        break;
                    case Word.Small:
                        dictionary.Add((Word)i, WordNature.Adjective);
                        adjectives.Add((Word)i, new Adjective(.5f));
                        break;
                    case Word.Great:
                        dictionary.Add((Word)i, WordNature.Adjective);
                        adjectives.Add((Word)i, new Adjective(1.5f));
                        break;
                    case Word.Strong:
                        dictionary.Add((Word)i, WordNature.Adjective);
                        adjectives.Add((Word)i, new Adjective(1.6f));
                        break;
                    case Word.Bad:
                        dictionary.Add((Word)i, WordNature.Adjective);
                        adjectives.Add((Word)i, new Adjective(.3f));
                        break;
                    case Word.Brave:
                        dictionary.Add((Word)i, WordNature.Adjective);
                        adjectives.Add((Word)i, new Adjective(1.3f));
                        break;
                    case Word.Good:
                        dictionary.Add((Word)i, WordNature.Adjective);
                        adjectives.Add((Word)i, new Adjective(1.4f));
                        break;
                    case Word.Crazy:
                        dictionary.Add((Word)i, WordNature.Adjective);
                        adjectives.Add((Word)i, new Adjective(.4f));
                        break;
                    case Word.Poor:
                        dictionary.Add((Word)i, WordNature.Adjective);
                        adjectives.Add((Word)i, new Adjective(.7f));
                        break;
                    case Word.Insult:
                        dictionary.Add((Word)i, WordNature.Expression);
                        break;
                    case Word.Curse:
                        dictionary.Add((Word)i, WordNature.Expression);
                        break;
                    case Word.Peace:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Dead:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Oorx:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Tromp:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Kingpak:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Robhead:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.CroolisVar:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.CroolisUlv:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Izwal:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Migrax:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Antenna:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Buggol:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Tricephal:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.TubularBrain:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Yukas:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Sinox:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Ondoyante:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Duplicate:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Tuttle:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Morlock:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Yoko:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Maxon:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Blood:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Torka:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Ship:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Contact:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Home:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Planet:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Trauma:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Entrax:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Ondoya:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Kristo:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Rosko:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Corpo:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Ulikan:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.BowBow:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Hour:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Coord:
                        dictionary.Add((Word)i, WordNature.Noun);
                        nouns.Add((Word)i);
                        break;
                    case Word.Equal:
                        dictionary.Add((Word)i, WordNature.Verb);
                        verbs.Add((Word)i, new Verb(VerbType.Transitive, 2, SentenceConstruction.SVO, .25f));
                        break;
                    case Word.OutOf:
                        dictionary.Add((Word)i, WordNature.Ponctuation);
                        break;
                    case Word.Zero:
                        dictionary.Add((Word)i, WordNature.Number);
                        break;
                    case Word.One:
                        dictionary.Add((Word)i, WordNature.Number);
                        break;
                    case Word.Two:
                        dictionary.Add((Word)i, WordNature.Number);
                        break;
                    case Word.Three:
                        dictionary.Add((Word)i, WordNature.Number);
                        break;
                    case Word.For:
                        dictionary.Add((Word)i, WordNature.Number);
                        break;
                    case Word.Five:
                        dictionary.Add((Word)i, WordNature.Number);
                        break;
                    case Word.Six:
                        dictionary.Add((Word)i, WordNature.Number);
                        break;
                    case Word.Seven:
                        dictionary.Add((Word)i, WordNature.Number);
                        break;
                    case Word.Height:
                        dictionary.Add((Word)i, WordNature.Number);
                        break;
                    case Word.Nine:
                        dictionary.Add((Word)i, WordNature.Number);
                        break;
                }
            }
        }

    }
}
