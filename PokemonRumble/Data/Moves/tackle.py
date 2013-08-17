from Move import *;
Tackle = Move.Add("tackle");

def Attack(player):
	if (player.OnGround):
		player.SetVelocity(player.Direction * 10, 3);
		player.SetAnimation("tackle", False);
		player.Disable(0.5);
		Hit = player.AddDamageBox(player.Direction * 0.4, 0, 0.2, 0.6);
		Hit.Damage = 10;
		Hit.Duration = 0.5;
		def KnockBack(other):
			other.SetVelocity(player.Direction * 5, 3);
			player.SetVelocity(0, 0);
			other.Disable(0.5);
		Hit.OnDamage = KnockBack;
Tackle.OnUse = Attack;