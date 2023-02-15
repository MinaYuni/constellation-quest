using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;

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

    // public List<string> level1 = new List<string>(){
    //     "Equuleus", "Aries", "Cassiopeia", "LittleDipper", "BigDipper"
    // };
    // public List<string> level2 = new List<string>(){
    //     "Libra", "Cancer", "Leo", "SerpensCaput", "Cygnus", "CanisMajor"
    // };
    // public List<string> level3 = new List<string>(){
    //     "Capricorn", "Perseus", "Taurus", "Scorpio", "Pisces"
    // };
    // public List<string> level4 = new List<string>(){
    //     "Aquarius", "Sagittarius", "Draco", "Andromeda", "Gemini"
    // };
    // public List<string> level5 = new List<string>(){
    //     "Pavo", "Virgo", "Phoenix", "Orion"
    // };

    public int nbLevels = 5;
    
    public List<string> level1 = new List<string>(){
        "Equuleus", "Cassiopeia"
    };
    public List<string> level2 = new List<string>(){
        "Libra", "Cygnus"
    };
    public List<string> level3 = new List<string>(){
        "Scorpio", "Pisces"
    };
    public List<string> level4 = new List<string>(){
        "Draco", "Andromeda"
    };
    public List<string> level5 = new List<string>(){
        "Pavo", "Phoenix"
    };

    private static float tpsSelection = 0.5f; // temps pour qu'une étoile soit sélectionnée si le curseur est dessus 
    private static float cste = 5.0f; 
    public int currLevel;
    // public List<string> lastLevelUnlocked = level1;

    public Dictionary<string, bool> ConstLearnt = new Dictionary<string, bool>();
    public Dictionary<string, int> ConstTimeLearnt = new Dictionary<string, int>();

    // timeLimit = (nbLiens * tpsSelection * 3) + niveau 
    public Dictionary<string, float> ConstTimeLimit = new Dictionary<string, float>() {
        { "Equuleus", (2.0f * tpsSelection * cste) + 1.0f }, 
        { "Cassiopeia", (4.0f * tpsSelection * cste) + 1.0f },
        { "Libra", (6.0f * tpsSelection * cste) + 2.0f }, 
        { "Cygnus", (8.0f * tpsSelection * cste) + 2.0f },
        { "Scorpio", (11.0f * tpsSelection * cste) + 3.0f }, 
        { "Pisces", (17.0f * tpsSelection * cste) + 3.0f },
        { "Draco", (15.0f * tpsSelection * cste) + 4.0f }, 
        { "Andromeda", (15.0f * tpsSelection * cste) + 4.0f },
        { "Pavo", (12.0f * tpsSelection * cste) + 5.0f }, 
        { "Phoenix", (13.0f * tpsSelection * cste) + 5.0f }
    };

    public List<string> ConstForChallenge = new List<string>();

    public void initConstLearnt(List<string> level){
        foreach (var item in level){
            ConstLearnt.Add(item, false);
        }
    }

    public void initConstTimeLearnt(List<string> level){
        foreach (var item in level){
            ConstTimeLearnt.Add(item, 0);
        }
    }

    public void initConstTimeLimit(List<string> level){
        foreach (var item in level){
            ConstTimeLimit.Add(item, 30.0f);
        }
    }

    void Start()
    {
        initConstLearnt(level1);
        initConstLearnt(level2);
        initConstLearnt(level3);
        initConstLearnt(level4);
        initConstLearnt(level5);

        initConstTimeLearnt(level1);
        initConstTimeLearnt(level2);
        initConstTimeLearnt(level3);
        initConstTimeLearnt(level4);
        initConstTimeLearnt(level5);

        //initConstTimeLimit(level1);
        //initConstTimeLimit(level2);
        //initConstTimeLimit(level3);
        //initConstTimeLimit(level4);
        //initConstTimeLimit(level5);

        levels.Add(level1);
        levels.Add(level2);
        levels.Add(level3);
        levels.Add(level4);
        levels.Add(level5);

        currLevel = 0;

        //ConstLearnt["Equuleus"] = true;
        //ConstLearnt["Cassiopeia"] = true;

        foreach (var v in ConstLearnt)
        {
            if (v.Value)
            {
                ConstForChallenge.Add(v.Key);
            }            
        }

        // Debug.Log("in gameData start levels foreach length");
        // foreach(var v in levels){
        //     Debug.Log(v.Count);
        // }
    }

    void Update()
    {
        foreach(var v in ConstLearnt)
        {
            if(!ConstForChallenge.Contains(v.Key))
            {                
                if (ConstTimeLearnt[v.Key] >= 2)
                {
                    ConstForChallenge.Add(v.Key);
                }
            }
        }
    }
}
