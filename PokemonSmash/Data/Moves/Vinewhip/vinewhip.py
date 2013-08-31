import Move;
import Timer;
import Type;
Vinewhip = Move.Add("vinewhip");
Vinewhip.DisplayName = "Vinewhip";

def Attack(player):
	player.SetAnimation("vinewhip", False);
	player.Disable(0.5);
	player.Cooldown = 1.0;
	
	Animation = player.AddDamageBox(0, 0, 0.1, 0.1);
	Animation.SetSkeleton("Moves/Vinewhip/vinewhip");
	Animation.SetAnimation("idle", False);
	Animation.Duration = 0.5;
	if (player.Direction == -1):
		Animation.FlipAnimation(True);
	
	def SpawnDamage():
		Hit = player.AddDamageBox(player.Direction * .75, 0, 1, 0.6);
		Hit.Duration = 0.2;
		def Damage(self, other):
			other.SetVelocity(player.Direction * 2, 3);
			other.Disable(0.25);
			other.TakeDamage(10, player);
		Hit.OnCollidePlayer = Damage;
	Timer.Schedule(SpawnDamage, 0.20);
	
Vinewhip.OnUse = Attack;

Vinewhip.Type = Type.Grass;
Vinewhip.Category = Type.Physical;