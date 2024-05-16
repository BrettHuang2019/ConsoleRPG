

using AdancedCSharp;
using AdancedCSharp.Abstract;
using AdancedCSharp.Model;
using AdancedCSharp.Model.Components;
using AdancedCSharp.States;

const int ZoneWidth = 30;
const int ZoneHeight = 30;

// Console.BufferWidth = Console.WindowWidth = ZoneWidth;
// Console.BufferHeight = Console.WindowHeight = ZoneHeight;

Engine engine = new Engine();

var playerModel = new Player();
playerModel.AddAbility(new Ability("Fireball",10));
playerModel.AddAbility(new Ability("Firestorm",100));
playerModel.AddItem(new Item("Axe", false, true, totoalDamage:30));
playerModel.AddItem(new Item("Bow", false, true, totoalDamage:2));

Entity player = new Entity();
player.AddComponent(new SpriteComponent(){Sprite = '$'});
player.AddComponent(new PlayerComponent(playerModel));
player.Position = new Vector3(2, 2, 1);

Entity tallGrass = new Entity();
tallGrass.AddComponent(new SpriteComponent(){Sprite = '#'});
tallGrass.AddComponent(new CombatComponent(()=> new Combat(playerModel, new BasicMob())));
tallGrass.Position = new Vector3(3, 3, 0);

Entity ceiling = new Entity();
ceiling.AddComponent(new SpriteComponent(){Sprite = '@'});
ceiling.Position = new Vector3(4, 4, 2);

Entity grass2 = new Entity();
grass2.AddComponent(new SpriteComponent(){Sprite = '#'});
grass2.Position = new Vector3(4, 4, 0);

Entity wall = new Entity();
wall.AddComponent(new ConstantEntranceComponent(false));
wall.AddComponent(new SpriteComponent(){Sprite = '*'});
wall.Position = new Vector3(5, 5, 0);

Entity npc1 = new Entity();
npc1.AddComponent(new DialogComponent(new Dialog(
    new DialogScreen("Have this item!",
        e => e.GetComponent<PlayerComponent>().Player.AddItem(new Item("Armor"+(new Random().Next(0,100)), true, false,-5)))
)));
npc1.AddComponent(new SpriteComponent(){Sprite = '!'});
npc1.Position = new Vector3(1, 1, 0);

Zone zone = new Zone("Zone 1", new Vector3(ZoneWidth, ZoneHeight, 3));
zone.AddEntity(player);
zone.AddEntity(tallGrass);
zone.AddEntity(ceiling);
zone.AddEntity(grass2);
// zone.RemoveEntity(ceiling);
zone.AddEntity(wall);
zone.AddEntity(npc1);


engine.PushState(new ZoneState(player, zone));

while (engine.IsRunning)
{
    engine.ProcessInput(Console.ReadKey(true));
}