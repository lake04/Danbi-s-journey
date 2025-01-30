using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEditor.VersionControl;
using UnityEngine;

public class Inventory:MonoBehaviour
{
    private void Start()
    {
        swordItem swordItem1 = new swordItem("Excalibur");
        swordItem swordItem2 = new swordItem("sword");
        swordItem swordItem3 = new swordItem("IronSword");
        swordItem swordItem4 = new swordItem("TreeSword");

        Inventory2 composite1 = new Inventory2("Inventory1");
        Inventory2 composite2 = new Inventory2("SwordInventory");

        composite1.Add(swordItem1);
        composite1.Add(swordItem2);
        composite1.Add(composite2);

        composite2.Add(swordItem3);
        composite2.Add(swordItem4);

        swordItem1.Snow();
        swordItem2.Snow();
        composite1.Snow();
        composite2.Snow();

      
    }
}
public abstract class Component
{
    public abstract void Snow();
}
public class Inventory2: Component
{
    public string name;
    private List<Component> inventorys = new List<Component>();

    public Inventory2(string name)
    {
        this.name = name;
    }

    public override void Snow()
    {
        Debug.Log(name);
        foreach (Component child in inventorys)
        {
            Debug.Log(name);
        }
            
    }

    public void Add(Component Component)
    {
        inventorys.Add(Component);
    }

    public void Remove(Component Component)
    {
        inventorys.Remove(Component);
    }
}

public class swordItem: Component
{
    private string name;

    public swordItem(string name)
    {
        this.name = name;
    }
 
    public override void Snow()
    {
        Debug.Log(name);
    }

   
}
