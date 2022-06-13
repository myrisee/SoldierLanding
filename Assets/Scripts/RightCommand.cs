using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCommand : Command
{
    private Player player;

    public RightCommand(Player player)
    {
        this.player = player;
    }
    public override void Execute()
    {
        this.player.Turn(270f);
    }
}
