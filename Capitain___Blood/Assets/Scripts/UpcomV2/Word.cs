using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RetroJam.CaptainBlood.Upcom
{
    [System.Serializable]
    public abstract class Word 
    {
        public string name;
        public float weight;
        public int valency;

        private static float[] nounsValuesSet = new float[50];

        public WordNature nature
        {
            get
            {
                if (this is Verb) return WordNature.Verb;
                else if (this is Adjective) return WordNature.Adjective;
                else if (this is Noun) return WordNature.Noun;
                else return WordNature.Special;
            }
        }

        #region Words
        #region Verbs
        public static Verb Go { get { return new Verb("Go", .25f, 2, false); } }
        public static Verb Want { get { return new Verb("Want", .25f, 3, true); } }
        public static Verb Teleport { get { return new Verb("Teleport", .25f, 2, false); } }
        public static Verb Give { get { return new Verb("Give", .75f, 3, false); } }
        public static Verb Like { get { return new Verb("Like", 1.0f, 2, true); } }
        public static Verb Say { get { return new Verb("Say", .25f, 3, true); } }
        public static Verb Know { get { return new Verb("Know", .5f, 2, false); } }
        public static Verb Search { get { return new Verb("Search", .5f, 2, false); } }
        public static Verb Help { get { return new Verb("Help", .75f, 2, true); } }
        public static Verb Destroy { get { return new Verb("Destroy", -1.0f, 2, false); } }
        public static Verb Free { get { return new Verb("Free", .85f, 2, false); } }
        public static Verb Kill { get { return new Verb("Kill", -.85f, 2, false); } }
        public static Verb Be { get { return new Verb("Be", .25f, 2, false); } }
        public static Verb Thank { get { return new Verb("Thank", .85f, 2, false); } }
        public static Verb Pray { get { return new Verb("Pray", .5f, 2, false); } }
        public static Verb Believe { get { return new Verb("Believe", .5f, 2, false); } }
        public static Verb Trade { get { return new Verb("Trade", .25f, 2, false); } }
        public static Verb Find { get { return new Verb("Find", .25f, 2, false); } }
        public static Verb See { get { return new Verb("See", .25f, 2, false); } }
        public static Verb Set { get { return new Verb("Set", .25f, 2, false); } }

        public static Verb[] Verbs { get { return new Verb[] { Go, Want, Teleport, Give, Like, Say, Know, Search, Help, Destroy, Free, Kill, Be, Thank, Pray, Believe, Trade, Find, See, Set }; } }
        #endregion
        #region Adjectives
        public static Adjective Unknown { get { return new Adjective("Unknown", .8f); } }
        public static Adjective Forbidden { get { return new Adjective("Forbidden", .25f); } }
        public static Adjective Impossible { get { return new Adjective("Impossible", .2f); } }
        public static Adjective Urgent { get { return new Adjective("Urgent", .9f); } }
        public static Adjective Different { get { return new Adjective("Different", 1.0f); } }
        public static Adjective Small { get { return new Adjective("Small", .5f); } }
        public static Adjective Great { get { return new Adjective("Great", 1.5f); } }
        public static Adjective Strong { get { return new Adjective("Strong", 1.6f); } }
        public static Adjective Weak { get { return new Adjective("Weak", .4f); } }
        public static Adjective Bad { get { return new Adjective("Bad", .3f); } }
        public static Adjective Brave { get { return new Adjective("Brave", 1.3f); } }
        public static Adjective Good { get { return new Adjective("Good", 1.4f); } }
        public static Adjective Crazy { get { return new Adjective("Crazy", .4f); } }
        public static Adjective Poor { get { return new Adjective("Poor", .7f); } }
        public static Adjective Rich { get { return new Adjective("Rich", 1.3f); } }
        public static Adjective Dead { get { return new Adjective("Dead", .1f); } }
        public static Adjective Alive { get { return new Adjective("Alive", 1.1f); } }
        public static Adjective Sinful { get { return new Adjective("Sinful", .2f); } }
        public static Adjective Sacred { get { return new Adjective("Sacred", 1.5f); } }
        public static Adjective Known { get { return new Adjective("Known", 1.2f); } }
        public static Adjective Beautiful { get { return new Adjective("Beautiful", 1.3f); } }
        public static Adjective Disabled { get { return new Adjective("Disabled", .5f); } }
        public static Adjective Enabled { get { return new Adjective("Enabled", 1.5f); } }
        public static Adjective Stupid { get { return new Adjective("Stupid", .2f); } }

        public static Adjective[] Adjectives { get { return new Adjective[] { Unknown, Forbidden, Impossible, Urgent, Different, Small, Great, Strong, Weak, Bad, Brave, Good, Crazy, Poor, Rich, Dead, Alive, Sinful, Sacred, Known, Beautiful, Disabled, Enabled, Stupid }; } }
        #endregion
        #region Nouns
        public static Noun Me { get { return new Noun("Me", nounsValuesSet[0]); } }
        public static Noun You { get { return new Noun("You", nounsValuesSet[1]); } }
        public static Noun Fear { get { return new Noun("Fear", nounsValuesSet[2]); } }
        public static Noun Prison { get { return new Noun("Prison", nounsValuesSet[3]); } }
        public static Noun Prisoner { get { return new Noun("Prisoner", nounsValuesSet[4]); } }
        public static Noun Trap { get { return new Noun("Trap", nounsValuesSet[5]); } }
        public static Noun Danger { get { return new Noun("Danger", nounsValuesSet[6]); } }
        public static Noun Money { get { return new Noun("Money", nounsValuesSet[7]); } }
        public static Noun Information { get { return new Noun("Information", nounsValuesSet[8]); } }
        public static Noun Nonsense { get { return new Noun("Nonsense", nounsValuesSet[9]); } }
        public static Noun RDV { get { return new Noun("RDV", nounsValuesSet[10]); } }
        public static Noun Time { get { return new Noun("Time", nounsValuesSet[11]); } }
        public static Noun Idea { get { return new Noun("Idea", nounsValuesSet[12]); } }
        public static Noun Code { get { return new Noun("Code", nounsValuesSet[13]); } }
        public static Noun Friend { get { return new Noun("Friend", nounsValuesSet[14]); } }
        public static Noun Enemy { get { return new Noun("Enemy", nounsValuesSet[15]); } }
        public static Noun Warrior { get { return new Noun("Warrior", nounsValuesSet[16]); } }
        public static Noun Leader { get { return new Noun("Leader", nounsValuesSet[17]); } }
        public static Noun Scientist { get { return new Noun("Scientist", nounsValuesSet[18]); } }
        public static Noun Sex { get { return new Noun("Sex", nounsValuesSet[19]); } }
        public static Noun Reproduction { get { return new Noun("Reproduction", nounsValuesSet[20]); } }
        public static Noun Male { get { return new Noun("Male", nounsValuesSet[21]); } }
        public static Noun Female { get { return new Noun("Female", nounsValuesSet[22]); } }
        public static Noun Identity { get { return new Noun("Identity", nounsValuesSet[23]); } }
        public static Noun Parent { get { return new Noun("Parent", nounsValuesSet[24]); } }
        public static Noun People { get { return new Noun("People", nounsValuesSet[25]); } }
        public static Noun Peace { get { return new Noun("Peace", nounsValuesSet[26]); } }
        public static Noun War { get { return new Noun("War", nounsValuesSet[27]); } }
        public static Noun Death { get { return new Noun("Death", nounsValuesSet[28]); } }
        public static Noun Life { get { return new Noun("Life", nounsValuesSet[29]); } }
        public static Noun Home { get { return new Noun("Home", nounsValuesSet[30]); } }
        public static Noun Ship { get { return new Noun("Ship", nounsValuesSet[31]); } }
        public static Noun Contact { get { return new Noun("Contact", nounsValuesSet[32]); } }
        public static Noun Planet { get { return new Noun("Planet", nounsValuesSet[33]); } }
        public static Noun Hour { get { return new Noun("Hour", nounsValuesSet[34]); } }
        public static Noun Coordinates { get { return new Noun("Coordinates", nounsValuesSet[35]); } }
        public static Noun Truth { get { return new Noun("Truth", nounsValuesSet[36]); } }
        public static Noun Priest { get { return new Noun("Priest", nounsValuesSet[37]); } }
        public static Noun Sanctuary { get { return new Noun("Sanctuary", nounsValuesSet[38]); } }
        public static Noun Fight { get { return new Noun("Fight", nounsValuesSet[39]); } }
        public static Noun Species { get { return new Noun("Species", nounsValuesSet[40]); } }
        public static Noun Moon { get { return new Noun("Moon", nounsValuesSet[41]); } }
        public static Noun Weapon { get { return new Noun("Weapon", nounsValuesSet[42]); } }
        public static Noun Infidel { get { return new Noun("Infidel", nounsValuesSet[43]); } }
        public static Noun Neighbour { get { return new Noun("Neighbour", nounsValuesSet[44]); } }
        public static Noun Believer { get { return new Noun("Believer", nounsValuesSet[45]); } }
        public static Noun Migrax { get { return new Noun("Migrax", nounsValuesSet[46]); } }
        public static Noun Yukas { get { return new Noun("Yukas", nounsValuesSet[47]); } }
        public static Noun Duplicates { get { return new Noun("Duplicates", nounsValuesSet[48]); } }
        public static Noun Blood { get { return new Noun("Blood", nounsValuesSet[49]); } }
        public static Noun[] Nouns { get { return new Noun[] { Me, You, Fear, Prison, Prisoner, Trap, Danger, Money, Information, Nonsense, RDV, Time, Idea, Code, Friend, Enemy, Warrior, Leader, Scientist,
                                                                Sex, Reproduction, Male, Female, Identity, Parent, People, Peace, War, Death, Life, Home, Ship, Contact, Planet, Hour, Coordinates, Truth, Priest,
                                                                Sanctuary, Fight, Species, Moon, Weapon, Infidel, Neighbour, Believer, Migrax, Yukas, Duplicates, Blood}; } }
        #endregion
        #region Special
        public static Special none { get { return new Special("", 0); } }
        public static Special QuestionMark { get { return new Special("?", 0); } }
        public static Special Not { get { return new Special("Not", 0); } }
        public static Special Yes { get { return new Special("Yes", 0); } }
        public static Special No { get { return new Special("No", 0); } }
        public static Special Hello { get { return new Special("Hello", 0); } }
        public static Special Bye { get { return new Special("Bye", 0); } }
        public static Special Laugh { get { return new Special("(laugh)", 0); } }
        public static Special Cry { get { return new Special("(cry)", 0); } }
        public static Special Insult { get { return new Special("(insult)", 0); } }
        public static Special What { get { return new Special("What", 0); } }
        public static Special Slash { get { return new Special("/", 0); } }
        public static Special Zero { get { return new Special("0", 0); } }
        public static Special One { get { return new Special("1", 0); } }
        public static Special Two { get { return new Special("2", 0); } }
        public static Special Three { get { return new Special("3", 0); } }
        public static Special For { get { return new Special("4", 0); } }
        public static Special Five { get { return new Special("5", 0); } }
        public static Special Six { get { return new Special("6", 0); } }
        public static Special Seven { get { return new Special("7", 0); } }
        public static Special Eight { get { return new Special("8", 0); } }
        public static Special Nine { get { return new Special("9", 0); } }
        public static Special[] Specials { get { return new Special[] { }; } }
        #endregion
        #endregion

    }

    [System.Serializable]
    public class Verb : Word
    {
        public SentenceConstruction construction;
        public bool allowVerbAfter;

        public Verb(string _name, float _weight, int _valency, bool _allowVerbAfter)
        {
            name = _name;
            weight = _weight;
            valency = _valency;
            allowVerbAfter = _allowVerbAfter;
        }
    }

    [System.Serializable]
    public class Noun : Word
    {
        public Noun(string _name, float _weight)
        {
            name = _name;
            weight = _weight;
        }
    }

    [System.Serializable]
    public class Adjective : Word
    {
        public Adjective(string _name, float _weight)
        {
            name = _name;
            weight = _weight;
        }
    }

    [System.Serializable]
    public class Special : Word
    {
        public Special(string _name, float _weight)
        {
            name = _name;
            weight = _weight;
        }
    }
}