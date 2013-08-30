import Move;
import Type;
Whip = Move.Add("tailwhip");
Whip.DisplayName = "Tail Whip";

def Attack(player):
	player.SetAnimation("tailwhip", False);
	player.Disable(0.25);
	player.Cooldown = 1.0;
	
	Hit = player.AddDamageBox(player.Direction * 0.8, 0, 0.8, 0.8);
	Hit.Duration = 0.5;
	def AttackDown(self, other):
		other.Disable(1);
		other.Defense *= 0.9;
	Hit.OnCollidePlayer = AttackDown;
Whip.OnUse = Attack;
Whip.Type = Type.Normal;
Whip.Category = Type.Status;