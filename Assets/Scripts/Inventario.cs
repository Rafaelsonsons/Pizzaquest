using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario{
    
    private List<Item> itenList;
    public Inventario()
    {
        itenList = new List<Item>();

        Debug.Log("Inventario criado");
    }

}
