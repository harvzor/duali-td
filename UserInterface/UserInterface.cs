public partial class UserInterface : CanvasLayer
{
	[Export] public int Player;

    private readonly Player _player = new();
    private Label _healthLabel;
    private Label _bankLabel;
    private Label _incomeLabel;
    private Panel _deadContainer;

    private TowerSpawner _towerSpawner;
    private Map _map;

    private Timer _incomeTimer;
    private RadialProgress _incomeTimerRadial;

    public override void _Ready()
    {
        this._bankLabel = this.GetNode<Label>("PlayerStats/MoneyContainer/Bank");
        this.IncreaseBank(0);
        
        this._incomeLabel = this.GetNode<Label>("PlayerStats/MoneyContainer/Income");
        this.IncreaseIncome(0);
        
        this._healthLabel = this.GetNode<Label>("PlayerStats/Health");
        this.TakeDamage(0);

        this._incomeTimer = this.GetNode<Timer>("PlayerStats/IncomeContainer/IncomeTimer");
        this._incomeTimerRadial = this.GetNode<RadialProgress>("PlayerStats/IncomeContainer/IncomeTimerRadial");
        
        this._towerSpawner = this
            .GetTree()
            .GetRoot()
            .GetChild(0)
            .GetNode<TowerSpawner>(nameof(TowerSpawner))!;

        this._map = this
            .GetTree()
            .GetRoot()
            .GetChild(0)
            .GetNode<Map>(nameof(Map))!;
        
        this._deadContainer = this.GetNode<Panel>("DeadContainer");
    }
    
    public void TakeDamage(int damage)
    {
        this._player.TakeDamage(damage);
        this._healthLabel.Text = this._player.Health + "❤️";

        if (this._player.IsDead())
        {
            this._deadContainer.Show();
            this._incomeTimer.Paused = true;
        }
    }

    private void IncreaseBank(int amount)
    {
        this._player.IncreaseBank(amount);
        this._bankLabel.Text = this._player.Bank + "G";
    }
    
    private void IncreaseIncome(int amount)
    {
        this._player.IncreaseIncome(amount);
        this._incomeLabel.Text = this._player.Income + "⬆️";
    }

    public void ShowTower(Vector2 position)
    {
        this._towerSpawner.ShowTower(position);
    }
    
    public void HideTower()
    {
        this._towerSpawner.HideTower();
    }
    
    public void TrySpawnTower(Vector2 position)
    {
        if (this._towerSpawner.Cost > this._player.Bank)
            return;

        if (this._towerSpawner.TowerAlreadyExists(position))
            return;

        this.IncreaseBank(-this._towerSpawner.Cost);
        this._towerSpawner.SpawnTower(position, this.Player);
    }

    public void TrySpawnCritter(PackedScene critterScene, int player)
    {
        if (Map.Cost > this._player.Bank)
            return;

        this.IncreaseIncome(Map.Cost);
        this.IncreaseBank(-Map.Cost);

        this._map.SpawnCritter(critterScene, player);
    }

    private void OnIncomeTimerTimeout()
    {
        if (this._incomeTimerRadial.Progress >= this._incomeTimerRadial.MaxValue)
        {
            this._incomeTimerRadial.Progress = 0;
            this.IncreaseBank(this._player.Income);
        }

        this._incomeTimerRadial.Progress += 1;
    }
}
