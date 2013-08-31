import Move;
import Type;
Tackle = Move.Add("takedown");
Tackle.DisplayName = "Take Down";

def Attack(player):
	if (player.OnGround):
		player.SetVelocity(player.Direction * 10, 3);
		player.SetAnimation("tackle", False);
		player.Disable(1);
		player.Cooldown = 2;
		Hit = player.AddDamageBox(player.Direction * 0.4, 0, 0.2, 0.6);
		Hit.Duration = 0.5;
		def KnockBack(self, other):
			other.SetVelocity(player.Direction * 5, 3);
			player.SetVelocity(player.Direction * -3, 3);
			player.SetVelocity(0, 0);
			other.Disable(0.25);
			other.TakeDamage(20, player);
			player.TakeDamage(20/4, player);
			self.Unload();
		Hit.OnCollidePlayer = KnockBack;
Tackle.OnUse = Attack;
Tackle.Type = Type.Normal;
Tackle.Category = Type.Physical;