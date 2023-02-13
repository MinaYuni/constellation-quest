using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {

   
    public List<bool> levelsUnlocked = new List<bool>(){
        true, false, false, false, false
    };

    public List<string> level1 = new List<string>(){
        "Equuleus", "Aries", "Cassiopeia", "Little Dipper", "Big Dipper"
    };

    public List<string> level2 = new List<string>(){
        "Libra", "Cancer", "Leo", "Serpens Caput", "Cygnus", "Canis Major"
    };

    public List<string> level3 = new List<string>(){
        "Capricorn", "Perseus", "Taurus", "Scorpio", "Pisces"
    };

    public List<string> level4 = new List<string>(){
        "Aquarius", "Sagittarius", "Draco", "Andromeda", "Gemini"
    };

    public List<string> level5 = new List<string>(){
        "Pavo", "Virgo", "Phoenix", "Orion"
    };

    public Dictionary<string, bool> ConstLearnt = new Dictionary<string, bool>();

    public void initConstLearnt(List<string> level){
        foreach (var item in level){

            ConstLearnt.Add(item, false);
        }
    }


    // private string[] level1 = {"Equuleus", "Aries", "Cassiopeia", "Little Dipper", "Big Dipper"};
    // private string[] level2 = {"Libra", "Cancer", "Leo", "Serpens Caput", "Cygnus", "Canis Major"};
    // private string[] level3 = {"Capricorn", "Perseus", "Taurus", "Scorpio", "Pisces"};
    // private string[] level4 = {"Aquarius", "Sagittarius", "Draco", "Andromeda", "Gemini"};
    // private string[] level5 = {"Pavo", "Virgo", "Phoenix", "Orion"};


    // public List<List<string>> ConstPerLevel =  new List<List<string>>(){
	// 	new List<string>(){"Equuleus", "Aries", "Cassiopeia", "Little Dipper", "Big Dipper"}
	// };

    // public Dictionary<int, List<string>> ConstPerLevel =  new Dictionary<int, List<string>>() {
    //     {1, {"Equuleus", "Aries", "Cassiopeia", "Little Dipper", "Big Dipper"}},
    //     {2, {"Libra", "Cancer", "Leo", "Serpens Caput", "Cygnus", "Canis Major"}}
    // };

    // public List<List<string>> ConstPerLevel =  new List<List<string>>(4);


    // ConstPerLevel.Add(level1);
        
    //     new List<string> {"Equuleus", "Aries", "Cassiopeia", "Little Dipper", "Big Dipper"},
    //     new List<string> {"Libra", "Cancer", "Leo", "Serpens Caput", "Cygnus", "Canis Major"},
    //     new List<string> {"Capricorn", "Perseus", "Taurus", "Scorpio", "Pisces"},
    //     new List<string> {"Aquarius", "Sagittarius", "Draco", "Andromeda", "Gemini"},
    //     new List<string> {"Pavo", "Virgo", "Phoenix", "Orion"}
    // };

}
