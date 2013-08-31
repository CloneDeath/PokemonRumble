import Move;
import Type;
import StatusEffect;
Hypnosis = Move.Add("hypnosis");
Hypnosis.DisplayName = "Hypnosis";

def Attack(player):
	player.SetAnimation("hypnosis", False);
	player.Disable(2);
	player.Cooldown = 10;
	
	Hit = player.AddProjectile(player.Direction * 0.8, 0, 0.8, 0.8);
	Hit.SetVelocity(player.Direction * 3, 0);
	Hit.Duration = 0.5;
	
	Hit.SetSkeleton("Moves/Hypnosis/Hypnosis");
	Hit.SetAnimation("idle", True);
	def Sleep(self, other):
		other.AddEffect(StatusEffect.Sleep(player, 5));
	Hit.OnCollidePlayer = Sleep;
	
	def KeepUp(self, time):
		self.SetVelocity(player.Direction * 3, 0);
	Hit.OnUpdate = KeepUp;
Hypnosis.OnUse = Attack;
Hypnosis.Type = Type.Psychic;
Hypnosis.Category = Type.Status;