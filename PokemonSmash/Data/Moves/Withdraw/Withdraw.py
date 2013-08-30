import Move;
import Type;
Withdraw = Move.Add("withdraw");

def Attack(player):
	player.SetAnimation("withdraw", False);
	player.Disable(0.5);
	player.Cooldown = 1.0;
	player.Defense *= 10;
	
	Hit = player.AddDamageBox(player.Direction * 0.8, 0, 0.8, 0.8);
	Hit.Duration = 0.5;
	
	def Withdraw(self, time):
		if (time > 0.4):
			player.Defense /= 10;
			self.Unload();
	Hit.OnUpdate = Withdraw;
Withdraw.OnUse = Attack;
Withdraw.Type = Type.Water;
Withdraw.Category = Type.Status;