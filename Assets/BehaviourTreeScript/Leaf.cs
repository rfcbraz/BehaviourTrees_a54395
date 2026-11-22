using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Leaf : Node
{  
    public delegate Status Tick();
    public Tick ProcessMethod;

    public Leaf(){}

    public Leaf(String n, Tick pm)
    {
        name = n;
        ProcessMethod = pm;
    }
    
    public override Status Process()
    {
        if (ProcessMethod != null)
            return ProcessMethod();
        return Status.FAILURE;
    }
}
