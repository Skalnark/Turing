using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineList {

    public Alphabet alph;
    public List<State> states;
    public string mName;
    public string description;
    

    public MachineList(Alphabet a, List<State> s, string n, string d)
    {
        alph = a;
        states = s;
        mName = n;
        description = d;
    }
}
