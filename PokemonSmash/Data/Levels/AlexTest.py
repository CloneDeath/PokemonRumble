import Arena;

a = Arena.Create(60, 60);

a.LoadTexture("tree", "Data/Tree.png", False);
a.LoadTexture("grass", "Data/Grass.png", False);

a.GroundTexture = a.GetTexture("grass");
a.SetDisplayAreaRange(-30,30,-30,30);

a.AddBlockEntity("tree", 0, 10, -0.0001, 20, 20, True, False);
