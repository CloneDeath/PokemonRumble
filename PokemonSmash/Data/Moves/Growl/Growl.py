import Move;
import Type;
Growl = Move.Add("growl");
Growl.DisplayName = "Growl";

def Attack(player):
	player.SetAnimation("growl", False);
	player.Disable(0.25);
	player.Cooldown = 1.0;
	
	Hit = player.AddDamageBox(player.Direction * 0.8, 0, 0.8, 0.8);
	Hit.Duration = 0.5;
	Hit.SetSkeleton("Moves/Growl/Growl");
	Hit.SetAnimation("idle", False);
	if (player.Direction < 0):
		Hit.FlipAnimation();
	def AttackDown(self, other):
		other.Disable(1);
		other.Attack *= 0.9;
	Hit.OnCollidePlayer = AttackDown;
Growl.OnUse = Attack;
Growl.Type = Type.Normal;
Growl.Category = Type.Status;