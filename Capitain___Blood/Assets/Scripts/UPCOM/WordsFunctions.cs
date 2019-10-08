namespace RetroJam.CaptainBlood.Lang
{
    public static class WordsFunctions
    {
        public static string ToText(this Word _word)
        {
            switch (_word)
            {
                case Word.none:
                    return "";
                case Word.QuestionMark:
                    return "?";
                case Word.Not:
                    return "NOT";
                case Word.Yes:
                    return "YES";
                case Word.No:
                    return "NO";
                case Word.Me:
                    return "ME";
                case Word.You:
                    return "YOU";
                case Word.Howdy:
                    return "HOWDY";
                case Word.Bye:
                    return "BYE";
                case Word.Go:
                    return "GO";
                case Word.Want:
                    return "WANT";
                case Word.Teleport:
                    return "TELEPORT";
                case Word.Give:
                    return "GIVE";
                case Word.Like:
                    return "LIKE";
                case Word.Say:
                    return "SAY";
                case Word.Know:
                    return "KNOW";
                case Word.Unknown:
                    return "UNKNOWN";
                case Word.Play:
                    return "PLAY";
                case Word.Search:
                    return "SEARCH";
                case Word.Race:
                    return "RACE";
                case Word.Vote:
                    return "VOTE";
                case Word.Help:
                    return "HELP";
                case Word.Disarm:
                    return "DISARM";
                case Word.Laugh:
                    return "( LAUGH )";
                case Word.Sob:
                    return "SOB";
                case Word.Fear:
                    return "FEAR";
                case Word.Destroy:
                    return "DESTROY";
                case Word.Free:
                    return "FREE";
                case Word.Kill:
                    return "KILL";
                case Word.Prison:
                    return "PRISON";
                case Word.Prisonner:
                    return "PRISONNER";
                case Word.Trap:
                    return "TRAP";
                case Word.Danger:
                    return "DANGER";
                case Word.Forbidden:
                    return "FORBIDDEN";
                case Word.Radioactivity:
                    return "RADIOACTIVITY";
                case Word.Impossible:
                    return "IMPOSSIBLE";
                case Word.Bounty:
                    return "BOUNTY";
                case Word.Information:
                    return "INFORMATION";
                case Word.NonSense:
                    return "NONSENSE";
                case Word.RDV:
                    return "RENDEZ-VOUS";
                case Word.Time:
                    return "TIME";
                case Word.Urgent:
                    return "URGENT";
                case Word.Idea:
                    return "IDEA";
                case Word.Missile:
                    return "MISSILE";
                case Word.Code:
                    return "CODE";
                case Word.Friend:
                    return "FRIEND";
                case Word.Ennemy:
                    return "ENNEMY";
                case Word.Spirit:
                    return "SPIRIT";
                case Word.Brain:
                    return "BRAIN";
                case Word.Warrior:
                    return "WARRIOR";
                case Word.President:
                    return "PRESIDENT";
                case Word.Scientist:
                    return "SCIENTIST";
                case Word.Genetic:
                    return "GENETIC";
                case Word.Sex:
                    return "SEX";
                case Word.Reproduction:
                    return "REPRODUCTION";
                case Word.Male:
                    return "MALE";
                case Word.Female:
                    return "FEMALE";
                case Word.Identity:
                    return "IDENTITY";
                case Word.Pop:
                    return "POP";
                case Word.People:
                    return "PEOPLE";
                case Word.Different:
                    return "DIFFERENT";
                case Word.Small:
                    return "SMALL";
                case Word.Great:
                    return "GREAT";
                case Word.Strong:
                    return "STRONG";
                case Word.Bad:
                    return "BAD";
                case Word.Brave:
                    return "BRAVE";
                case Word.Good:
                    return "GOOD";
                case Word.Crazy:
                    return "CRAZY";
                case Word.Poor:
                    return "POOR";
                case Word.Insult:
                    return "( INSULT )";
                case Word.Curse:
                    return "( CURSE )";
                case Word.Peace:
                    return "PEACE";
                case Word.Dead:
                    return "DEAD";
                case Word.Oorx:
                    return "OORX";
                case Word.Tromp:
                    return "TROMP";
                case Word.Kingpak:
                    return "KINGPAK";
                case Word.Robhead:
                    return "ROBHEAD";
                case Word.CroolisVar:
                    return "CROOLIS-VAR";
                case Word.CroolisUlv:
                    return "CROOLIS-ULV";
                case Word.Izwal:
                    return "IZWAL";
                case Word.Migrax:
                    return "MIGRAX";
                case Word.Antenna:
                    return "ANTENNA";
                case Word.Buggol:
                    return "BUGGOL";
                case Word.Tricephal:
                    return "TRICEPHAL";
                case Word.TubularBrain:
                    return "TUBULAR-BRAIN";
                case Word.Yukas:
                    return "YUKAS";
                case Word.Sinox:
                    return "SINOX";
                case Word.Ondoyante:
                    return "ONDOYANTE";
                case Word.Duplicate:
                    return "DUPLICATE";
                case Word.Tuttle:
                    return "TUTTLE";
                case Word.Morlock:
                    return "MORLOCK";
                case Word.Yoko:
                    return "YOKO";
                case Word.Maxon:
                    return "MAXON";
                case Word.Blood:
                    return "BLOOD";
                case Word.Torka:
                    return "TORKA";
                case Word.Ship:
                    return "SHIP";
                case Word.Contact:
                    return "CONTACT";
                case Word.Home:
                    return "HOME";
                case Word.Planet:
                    return "PLANET";
                case Word.Trauma:
                    return "TRAUMA";
                case Word.Entrax:
                    return "ENTRAX";
                case Word.Ondoya:
                    return "ONDOYA";
                case Word.Kristo:
                    return "KRISTO";
                case Word.Rosko:
                    return "ROSKO";
                case Word.Corpo:
                    return "CORPO";
                case Word.Ulikan:
                    return "ULIKAN";
                case Word.BowBow:
                    return "BOW-BOW";
                case Word.Hour:
                    return "HOUR";
                case Word.Coord:
                    return "COORDINATE";
                case Word.Equal:
                    return "=";
                case Word.OutOf:
                    return "/";
                case Word.Zero:
                    return "0";
                case Word.One:
                    return "1";
                case Word.Two:
                    return "2";
                case Word.Three:
                    return "3";
                case Word.For:
                    return "4";
                case Word.Five:
                    return "5";
                case Word.Six:
                    return "6";
                case Word.Seven:
                    return "7";
                case Word.Height:
                    return "8";
                case Word.Nine:
                    return "9";
                default:
                    return "";
            }
        }

        public static float Value(this Word _word)
        {
            switch (Words.dictionary[_word])
            {
                case WordNature.Adjective:
                    return Words.adjectives[_word].factor;
                case WordNature.Verb:
                    return Words.verbs[_word].factor;
                case WordNature.Negation:
                    return -1;
                case WordNature.Number:
                    return (float)_word - 111;
                default:
                    return 0;
            }
        }

        public static Word Random(this WordNature _nature)
        {
            Word result = Word.none;

            do
            {
                result = (Word)UnityEngine.Random.Range(1,121);
            } while (result.Nature() != _nature);

            return result;
        }

        public static WordNature Nature(this Word _word)
        {
            return Words.dictionary[_word];
        }

        public static bool Nature(this Word _word,WordNature _nature)
        {
            return _word.Nature() == _nature;
        }

        public static SentenceConstruction Type(this Word _word)
        {
            if (!_word.Nature(WordNature.Verb)) throw new LanguageException("You must use a verb.");

            return Words.verbs[_word].constructions;
        }

        public static bool IsWord(this Word _word)
        {
            WordNature nature = _word.Nature();
            if (nature == WordNature.Ponctuation || nature == WordNature.Expression || nature == WordNature.Negation || nature == WordNature.Number) return false;

            return true;
        }

        public static bool Contains(this Sentence _sentence, Word _word)
        {
            for (int i = 0; i < _sentence.size; i++)
            {
                if (_sentence.words[i] == _word) return true;
            }

            return false;
        }

        public static bool Contains(this Sentence _sentence, Word[] _words)
        {
            int count = 0;

            for (int k = 0; k < _words.Length; k++)
            {
                for (int i = 0; i < _sentence.size; i++)
                {
                    if (_sentence.words[i] == _words[k]) count++;
                }
            }

            return count == _words.Length;
        }

        public static bool Contains(this Sentence _sentence, Word[] _words, bool _validOrder)
        {
            int count = 0;

            if (!_validOrder) return _sentence.Contains(_words);
            else
            {
                for (int k = 0; k < _sentence.size; k++)
                {
                    if (_sentence.words[k] != _words[0]) continue;

                    for (int i = 0; i < _words.Length; i++)
                    {
                        if (_sentence.words[k + i] != _words[i]) break;
                        else
                        {
                            count++;
                        }
                    }

                    if (count == _words.Length) return true;
                    else count = 0;
                }

                return false;
            }
        }
    }
}
