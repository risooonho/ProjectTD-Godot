using Godot;

namespace ProjectTD.scripts {
public class Game : Node2D {
	private Level _Level;
	private Player _player;

	private int _points;
	private int _money;
	[Export()] private int _startingMoney = 0;
	private int _health;
	[Export()] private int _startingHealth = 10;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		_Level = GetParent<Level>();
		_player = GetNode<Player>("Player");
		_player.SetLevelWidth(_Level.LevelWidth);

		AddToGroup("state");

		_money = _startingMoney;
		_health = _startingHealth;
		_points = 0;

		GD.Print($"Loaded level {_Level.Name}");
	}

	public void addPoints(int amount) {
		_points += amount;

		GetTree().CallGroup("ui", nameof(HUD.updatePoints), _points);
	}

	public void addMoney(int amount) {
		_money += amount;

		GetTree().CallGroup("ui", nameof(HUD.updateMoney), _money);
	}

	public void removeHealth(int amount) {
		_health -= amount;

		GetTree().CallGroup("ui", nameof(HUD.updateHealth), _health);

		if (_health <= 1) {
			GetTree().Paused = true;
			GD.Print("Game over");
		}
	}
}
}