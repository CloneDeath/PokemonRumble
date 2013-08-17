from Move import *;
Tackle = Move.Add("tackle");

def Attack(player):
	if (player.OnGround):
		player.SetVelocity(player.Direction * 10, 3);
		player.SetAnimation("tackle", False);
		player.Disable(0.5);
Tackle.OnUse = Attack;