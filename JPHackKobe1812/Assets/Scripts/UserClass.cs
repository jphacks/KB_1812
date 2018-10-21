using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserClass {

    public string name;
    public bool isGoal;
    public int score;

    public UserClass(string name, bool isGoal, int score)
    {
        this.name = name;
        this.isGoal = isGoal;
        this.score = score;
    }
}
