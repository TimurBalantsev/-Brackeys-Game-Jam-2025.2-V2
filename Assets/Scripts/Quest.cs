using System;
using UnityEngine;

[Serializable]
public class Quest
{
    [SerializeField] private int amount;
    [SerializeField] private Item item;
    [SerializeField] private int popReward;
    [SerializeField] private int popConsequence;

    public int Amount => amount;
    public Item Item => item;
    public int PopReward => popReward;
    public int PopConsequence => popConsequence;
    public Quest(int amount, Item item, int popReward, int popConsequence)
    {
        this.amount = amount;
        this.item = item;
        this.popReward = popReward;
        this.popConsequence = popConsequence;
    }   

    public override string ToString()
    {
        return $"Quest : {item.name} x {amount}|Reward : {popReward}|Punishment : {popConsequence} {base.ToString()}";
    }
}
