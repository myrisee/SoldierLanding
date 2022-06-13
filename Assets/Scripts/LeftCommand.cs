using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCommand : Command
{
    private Player player;

    public LeftCommand(Player player)
    {
        this.player = player;
    }

    public override void Execute()
    {
        this.player.Turn(90);
    }
}
