using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardCommand : Command
{
    private Player player;

    public ForwardCommand(Player player)
    {
        this.player = player;
    }

    public override void Execute()
    {
        this.player.Move(Vector2.up);
    }
}
