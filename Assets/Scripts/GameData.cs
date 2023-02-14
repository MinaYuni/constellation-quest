using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {

   
    // public List<bool> levelsUnlocked = new List<bool>(){
    //     true, false, false, false, false
    // };

    public Dictionary<string, bool> levelsUnlocked = new Dictionary<string, bool>(){
        {"Level1", true},
        {"Level2", false},
        {"Level3", false},
        {"Level4", false},
        {"Level5", false}

    };

    public List<List<string>> levels = new List<List<string>>();

    public List<string> level1 = new List<string>(){
        "Equuleus", "Aries", "Cassiopeia", "LittleDipper", "BigDipper"
    };
    public List<string> level2 = new List<string>(){
        "Libra", "Cancer", "Leo", "SerpensCaput", "Cygnus", "CanisMajor"
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

    public int currLevel;
    // public List<string> lastLevelUnlocked = level1;

    public Dictionary<string, bool> ConstLearnt = new Dictionary<string, bool>();

    public void initConstLearnt(List<string> level){
        foreach (var item in level){
            ConstLearnt.Add(item, false);
        }
    }

    void Start()
    {
        initConstLearnt(level1);
        initConstLearnt(level2);
        initConstLearnt(level3);
        initConstLearnt(level4);
        initConstLearnt(level5);

        levels.Add(level1);
        levels.Add(level2);
        levels.Add(level3);
        levels.Add(level4);
        levels.Add(level5);

        currLevel = 0;
        ConstLearnt["Equuleus"] = true;
        ConstLearnt["Cassiopeia"] = true;

        // Debug.Log("in gameData start levels foreach length");
        // foreach(var v in levels){
        //     Debug.Log(v.Count);
        // }
    }

    // initConstLearnt(level1);
    // initConstLearnt(level2);
    // initConstLearnt(level3);
    // initConstLearnt(level4);
    // initConstLearnt(level5);


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
