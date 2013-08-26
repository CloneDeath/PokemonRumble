import Move;
Scratch = Move.Add("scratch");

def Attack(player):
	player.SetAnimation("scratch", False);
	player.Disable(0.25);
	player.Cooldown = 1.0;
	Hit = player.AddDamageBox(player.Direction * 0.4, 0.1, 0.5, 0.8);
	Hit.Duration = 0.5;
	Hit.SetSkeleton("Moves/Scratch/Scratch");
	Hit.SetAnimation("idle", False);
	if (player.Direction > 0):
		Hit.FlipAnimation();
	def KnockBack(self, other):
		other.SetVelocity(player.Direction * 2, 3);
		other.Disable(0.25);
		other.TakeDamage(10, player);
	Hit.OnCollidePlayer = KnockBack;
Scratch.OnUse = Attack;