from Move import *;
Burst = Move.Add("flameburst");

def Attack(player):
	if (player.OnGround):
		player.SetAnimation("idle", False);
		player.Disable(2.0);
		player.Cooldown = 5.0;
		Hit = player.AddDamageBox(0, 0.2, 2.0, 2.0);
		Hit.Duration = 0.5;
		def KnockBack(self, other):
			other.SetVelocity(player.Direction * 2, 3);
			other.Disable(1.0);
			other.TakeSpecialDamage(20, player);
		Hit.OnCollidePlayer = KnockBack;
Burst.OnUse = Attack;