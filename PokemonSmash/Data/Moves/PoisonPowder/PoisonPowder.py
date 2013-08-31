import Move;
import Random;
import Type;
import StatusEffect;
Powder = Move.Add("poisonpowder");
Powder.DisplayName = "Poison Powder";


def CreatePowder(player):
	particle = player.AddProjectile(0, 0, 0.1, 0.1);
	particle.SetVelocity((Random.NextDouble() + 1) * player.Direction, 3 + (Random.NextDouble() * 2));
	particle.CollisionMask = 0x0001;
	particle.SetSkeleton("Moves/PoisonPowder/PoisonPowder");
	particle.SetAnimation("idle", False);
	particle.Z = Random.NextDouble() - 0.5;
	particle.Permanent = True;
	def Drain(self, other):
		other.AddEffect(StatusEffect.Poison(1, player, 10));
		particle.Permanent = False;
		self.Unload();
	particle.OnCollidePlayer = Drain;
	
	def EarthHit(self):
		self.Unload();
	particle.OnCollideEarth = EarthHit;

def Attack(player):
	player.SetAnimation("poisonpowder", False);
	player.Disable(1);
	player.Cooldown = 1.5;
	
	def SpawnSpores(self, time):
		if Random.Next(5) == 0:
			for i in range(Random.Next(2) + 1):
				CreatePowder(player);
	
	powderspawner = player.AddDamageBox(0, 0, 0.1, 0.1);
	powderspawner.Duration = 0.5;
	powderspawner.OnUpdate = SpawnSpores;
		
			
Powder.OnUse = Attack;
Powder.Type = Type.Grass;
Powder.Category = Type.Status;