using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class InventoryStack
{
    public InventoryStack(LifeformInfo.Lifeform type, int quantity)
    {
        this.lifetype = type;
        this.quantity = quantity;
    }
    public LifeformInfo.Lifeform lifetype;
    public int quantity;

    public override string ToString()
    {
        return this.lifetype.ToString() +" : " + this.quantity.ToString()+"\n";
    }
}
public class FishInventory : MonoBehaviour
{
    // Start is called before the first frame update
   

    public List<InventoryStack> stacks;

    public int maxFishs = 5;
    public int count = 0;
    public Collider collider;
    public bool onPull = false;

    public TextMeshProUGUI text;

    private void Start()
    {
        collider = GetComponent<Collider>();
    }

    private void Update()
    {
        collider.enabled = (!IsFull() && onPull) ;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (count == maxFishs) return;
        other.TryGetComponent(out CommonFish fish);
        if(fish != null)
        {
            for(int i = 0; i < stacks.Count; i ++)
            {
                if (stacks[i].lifetype == fish.LifeformType)
                {
                    stacks[i].quantity++;
                    print("Achei um " + stacks[i].lifetype+", agora tenho "+ stacks[i].quantity);
                    count++;
                    Destroy(other.gameObject);
                    text.text = this.ToString();
                    return;
                }
            }
            stacks.Add(new InventoryStack(fish.LifeformType, 1));
            count++;
            Destroy(other.gameObject);
        }
        text.text = this.ToString();
    }

    public bool IsFull()
    {
        return (count == maxFishs);
    }

    public void SetOnPull(bool b)
    {
        onPull = b;
    }

    public override string ToString()
    {
        if (count == 0) return "Nothing on the inventory";

        string s = "";
        foreach(InventoryStack stack in stacks)
        {
            s += stack.ToString();
        }
        return s;
    }
}
