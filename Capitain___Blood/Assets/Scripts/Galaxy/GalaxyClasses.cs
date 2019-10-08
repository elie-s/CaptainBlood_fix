using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RetroJam.CaptainBlood.Lang;
using System;
using System.Threading.Tasks;

namespace RetroJam.CaptainBlood.GalaxyLib
{
    [System.Serializable]
    public class Planet
    {
        public Word[] name;
        public Vector2Int coordinates;
        public Word race;
        public bool visited;
        public bool destroyed;
        public Vector2 renderingValues;

        public Planet(Word[] _name, Vector2Int _coord, Word _race)
        {
            if (_name.Length > 4) throw new PlanetValueException("Planet's name is too long.");
            if (_name[0] != Word.Planet) throw new PlanetValueException("Planet's name must begin with PLANET");
            if (_coord.x < 0 || _coord.x > 256 || _coord.y < 0 || _coord.y > 125) throw new PlanetValueException("Planet's coordinates must be between [0,0] and [256,125].");
            if((int)_race < 73 && (int)_race > 88) throw new PlanetValueException("Planet's race is not a race !");

            name = _name;
            coordinates = _coord;
            race = _race;


            renderingValues = new Vector2(UnityEngine.Random.Range(0, 20), UnityEngine.Random.Range(0, 1000000));
        }

        public Planet(Vector2Int _coord, Word _race)
        {
            if (_coord.x < 0 || _coord.x > 256 || _coord.y < 0 || _coord.y > 125) throw new PlanetValueException("Planet's coordinates must be between [0,0] and [256,125].");
            if ((int)_race < 73 || (int)_race > 88) throw new PlanetValueException("Planet's race is not a race !");

            name = GenerateName();
            coordinates = _coord;
            race = _race;

            System.Random rand = new System.Random();

            renderingValues = new Vector2(rand.Next(0, 13), rand.Next(0, 999999));
        }

        [JsonConstructor]
        public Planet(Word[] _name, Vector2Int _coord, Word _race, bool _visited, bool _destroyed, Alien _inhabitant, Vector2 _renderingValues)
        {
            name = _name;
            coordinates = _coord;
            race = _race;
            visited = _visited;
            destroyed = _destroyed;
            renderingValues = _renderingValues;
        }


        private Word[] GenerateName()
        {
            System.Random rand = new System.Random();
            int length = rand.Next(3, 4);

            Word[] result = new Word[length];

            //Debug.Log(result.Length);

            result[0] = Word.Planet;
            result[1] = (Word)rand.Next(2, 72);
            result[2] = (Word)rand.Next(2, 72);
            if (result.Length > 3) result[3] = (Word)rand.Next(113, 120);;

            return result;
        }
    }

    [System.Serializable]
    public class Grid
    {

        public Vector2Int[] coord;
        public Planet[] planets;

        public Grid(int _size)
        {
            coord = new Vector2Int[_size];
            planets = new Planet[_size];
        }

        public Planet Planet(Vector2Int _coord)
        {
            for (int i = 256*_coord.x; i < 256 * _coord.x + 126; i++)
            {
                if(_coord.y == coord[i].y) return planets[i];
                
            }

            return null;
        }
    }

    public static class Galaxy
    {
        public static Dictionary<Vector2Int, Planet> planets;
        public static Dictionary<Vector2Int, Alien> inhabitants;
        public static List<Vector2Int> inhabitedCoordinates;

        public static void Initialize()
        {
            
            inhabitants = new Dictionary<Vector2Int, Alien>();
            inhabitedCoordinates = new List<Vector2Int>();

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

            sw.Start();

            

            for (int i = 0; i < 320; i++)
            {
                Vector2Int coord = new Vector2Int(UnityEngine.Random.Range(0,256), UnityEngine.Random.Range(0,126) );

                if (!inhabitants.ContainsKey(coord))
                {
                    inhabitants.Add(coord, new Alien(coord));
                    inhabitedCoordinates.Add(coord);
                }
            }

            

            sw.Stop();

            Debug.Log("Time to Initialize whole Galaxy : "+sw.ElapsedMilliseconds/1000+"s. \n"+"Inhabiants: "+inhabitants.Count+"\n Inhabited: "+inhabitedCoordinates.Count);
        }

        public static void GeneratePlanetsAlongY(int _coordX)
        {
            for (int y = 0; y < 126; y++)
            {
                System.Random rand = new System.Random();
                planets.Add(new Vector2Int(_coordX, y), new Planet(new Vector2Int(_coordX, y), (Word)rand.Next(74,88)));
            }
        }

        public static async Task GeneratePlanets()
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            planets = new Dictionary<Vector2Int, Planet>();
            

            for (int x = 0; x < 256; x++)
            {
                await Task.Run(() => GeneratePlanetsAlongY(x));
            }



            sw.Stop();
            Debug.Log("Whole galaxy generated in "+sw.ElapsedMilliseconds+"ms.");
        }

        public static void Initialize(Dictionary<Vector2Int, Planet> _savePlanet, Dictionary<Vector2Int, Alien> _savePop)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

            sw.Start();

            inhabitedCoordinates = new List<Vector2Int>();

            planets = _savePlanet;
            inhabitants = _savePop;

            foreach (Vector2Int _coord in inhabitants.Keys)
            {
                inhabitedCoordinates.Add(_coord);
            }

            Debug.Log("Amount of aliens loaded : " + inhabitedCoordinates.Count + ".");

            sw.Stop();

            Debug.Log("Time to Initialize whole Galaxy : " + sw.ElapsedMilliseconds + "ms.");
        }

        public static void AddAlien(Vector2Int _coord)
        {
            if (!inhabitants.ContainsKey(_coord))
            {
                inhabitants.Add(_coord, new Alien(_coord));
            }
        }

        public static Alien UnemployedAlien()
        {
            Alien result;

            do
            {
                result = Galaxy.inhabitants[Galaxy.RandomInhabitedPlanet().coordinates];
            } while (result.mission != MissionType.none);

            return result;
        }

        public static Vector2Int SetDuplicate()
        {
            Alien duplicate = UnemployedAlien();
            duplicate.mission = MissionType.Duplicate;
            duplicate.race = Races.Duplicate;

            return duplicate.coordinates;
        }

        public static Planet RandomInhabitedPlanet()
        {
            return planets[inhabitedCoordinates[UnityEngine.Random.Range(0, inhabitedCoordinates.Count)]];
        }
    }

    public class PlanetValueException : System.Exception
    {
        public PlanetValueException(string message) : base (message) { }
    }

    public class PlanetLoading : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Dictionary<Vector2Int, Planet>).IsAssignableFrom(objectType);
        }

        //Deserialize json to an Object
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            //Debug.Log("De-serializing!");
            if (reader.TokenType == JsonToken.StartArray)
            {
                // Load JArray from stream
                JArray jArray = JArray.Load(reader);

                //Where to re-create the json data into 
                Dictionary<Vector2Int, Planet> dict = new Dictionary<Vector2Int, Planet>();

                if (jArray == null || jArray.Count < 2)
                {
                    return dict;
                }

                //Do the loop faster with +=2
                for (int i = 0; i < jArray.Count; i += 2)
                {
                    //first item = key
                    string firstData = jArray[i + 0].ToString();
                    //second item = value
                    string secondData = jArray[i + 1].ToString();

                    //Create Vector2Int key data 
                    Vector2Int vect = JsonConvert.DeserializeObject<Vector2Int>(firstData);

                    //Create Collection value data
                    Planet values = JsonConvert.DeserializeObject<Planet>(secondData);

                    //Add both Key and Value to the Dictionary if key doesnt exit yet
                    if (!dict.ContainsKey(vect))
                        dict.Add(vect, values);
                }
                //Return the Dictionary result
                return dict;
            }
            return new Dictionary<Vector2Int, Planet>();
        }

        //SerializeObject to Json
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //Debug.Log("Serializing!");
            if (value is Dictionary<Vector2Int, Planet>)
            {
                //Get the Data to serialize
                Dictionary<Vector2Int, Planet> dict = (Dictionary<Vector2Int, Planet>)value;

                //Loop over the Dictionary array and write each one
                writer.WriteStartArray();
                foreach (KeyValuePair<Vector2Int, Planet> entry in dict)
                {
                    //Write Key (Vector) 
                    serializer.Serialize(writer, entry.Key);
                    //Write Value (Collection)
                    serializer.Serialize(writer, entry.Value);
                }
                writer.WriteEndArray();
                return;
            }
            writer.WriteStartObject();
            writer.WriteEndObject();
        }
    }

    public class AlienLoading : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Dictionary<Vector2Int, Alien>).IsAssignableFrom(objectType);
        }

        //Deserialize json to an Object
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            //Debug.Log("De-serializing!");
            if (reader.TokenType == JsonToken.StartArray)
            {
                // Load JArray from stream
                JArray jArray = JArray.Load(reader);

                //Where to re-create the json data into 
                Dictionary<Vector2Int, Alien> dict = new Dictionary<Vector2Int, Alien>();

                if (jArray == null || jArray.Count < 2)
                {
                    return dict;
                }

                //Do the loop faster with +=2
                for (int i = 0; i < jArray.Count; i += 2)
                {
                    //first item = key
                    string firstData = jArray[i + 0].ToString();
                    //second item = value
                    string secondData = jArray[i + 1].ToString();

                    //Create Vector2Int key data 
                    Vector2Int vect = JsonConvert.DeserializeObject<Vector2Int>(firstData);

                    //Create Collection value data
                    Alien values = JsonConvert.DeserializeObject<Alien>(secondData);

                    //Add both Key and Value to the Dictionary if key doesnt exit yet
                    if (!dict.ContainsKey(vect))
                        dict.Add(vect, values);
                }
                //Return the Dictionary result
                return dict;
            }
            return new Dictionary<Vector2Int, Alien>();
        }

        //SerializeObject to Json
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //Debug.Log("Serializing!");
            if (value is Dictionary<Vector2Int, Alien>)
            {
                //Get the Data to serialize
                Dictionary<Vector2Int, Alien> dict = (Dictionary<Vector2Int, Alien>)value;

                //Loop over the Dictionary array and write each one
                writer.WriteStartArray();
                foreach (KeyValuePair<Vector2Int, Alien> entry in dict)
                {
                    //Write Key (Vector) 
                    serializer.Serialize(writer, entry.Key);
                    //Write Value (Collection)
                    serializer.Serialize(writer, entry.Value);
                }
                writer.WriteEndArray();
                return;
            }
            writer.WriteStartObject();
            writer.WriteEndObject();
        }
    }

}
