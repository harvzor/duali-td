public partial class UserInterface : Control
{
    [Export] public int Player;

    public PackedScene SelectedTower;

    private readonly Player _player = new();
    private Label _healthLabel;
    private Label _bankLabel;
    private Label _incomeLabel;
    private PanelContainer _deadContainer;

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
            .GetNode<World>("World")
            .GetNode<TowerSpawner>(nameof(TowerSpawner))!;

        this._map = this
            .GetTree()
            .GetRoot()
            .GetNode<World>("World")
            .GetNode<Map>(nameof(Map))!;

        this._deadContainer = this.GetNode<PanelContainer>("DeadContainer");
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
        BulletTower bulletTower = this.SelectedTower.Instantiate<BulletTower>();
        bulletTower.Player = this.Player;

        this._towerSpawner.ShowTower(position, bulletTower);
    }

    public void HideTower()
    {
        this._towerSpawner.HideTower(this.Player);
    }

    public void TrySpawnTower(Vector2 position)
    {
        BulletTower bulletTower = this.SelectedTower.Instantiate<BulletTower>();

        if (bulletTower.Cost > this._player.Bank)
            return;

        if (this._towerSpawner.TowerAlreadyExists(position))
            return;

        this.IncreaseBank(-bulletTower.Cost);
        this._towerSpawner.SpawnTower(position, this.Player, bulletTower);
    }

    public void TrySpawnCritter(PackedScene critterScene, int player)
    {
        CritterBase critter = critterScene.Instantiate<CritterBase>();
        critter.Player = player;

        if (critter.Cost > this._player.Bank)
            return;

        this.IncreaseIncome(critter.Cost / 3);
        this.IncreaseBank(-critter.Cost);

        this._map.SpawnCritter(critter, player);
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

