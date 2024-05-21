using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class item
{
    public itemData Data;
    public int maxStackSize;
    public int stackSize;
    public  item(itemData _data)    
    {
        Data = _data;
        addStack();
    }

    public void addStack(int _stackSize)
    {
        stackSize+=_stackSize;
    }
    public void addStack()
    {
        stackSize++;
    }
    
    public void removeStack()
    {
        stackSize--;
    }
    public void removeStack(int _stackSize)
    {
        stackSize-=_stackSize;
    }
}
