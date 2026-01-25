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
        this._player.OnBankChanged += (newBank) =>
        {
            this._bankLabel.Text = newBank + "G";
        };
        
        this._player.OnIncomeChanged += (newIncome) =>
        {
            this._incomeLabel.Text = newIncome + "⬆️";
        };
        
        this._bankLabel = this.GetNode<Label>("PlayerStats/MoneyContainer/Bank");
        this._player.IncreaseBank(0);

        this._incomeLabel = this.GetNode<Label>("PlayerStats/MoneyContainer/Income");
        this._player.IncreaseIncome(0);

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

    public void ShowTower(Vector2 position)
    {
        BulletTower bulletTower = this.SelectedTower.Instantiate<BulletTower>();
        bulletTower.Player = this._player;

        this._towerSpawner.ShowTower(position, bulletTower);
    }

    public void HideTower()
    {
        this._towerSpawner.HideTower(this._player);
    }

    public void TrySpawnTower(Vector2 position)
    {
        BulletTower bulletTower = this.SelectedTower.Instantiate<BulletTower>();
        bulletTower.Player = this._player;

        if (bulletTower.Cost > this._player.Bank)
            return;

        if (this._towerSpawner.TowerAlreadyExists(position))
            return;

        this._player.IncreaseBank(-bulletTower.Cost);
        this._towerSpawner.SpawnTower(position, bulletTower);
    }

    public void TrySpawnCritter(PackedScene critterScene)
    {
        CritterBase critter = critterScene.Instantiate<CritterBase>();
        critter.Player = this._player;

        if (critter.Cost > this._player.Bank)
            return;

        this._player.IncreaseIncome(critter.Cost / 3);
        this._player.IncreaseBank(-critter.Cost);

        this._map.SpawnCritter(critter, this.Player);
    }

    private void OnIncomeTimerTimeout()
    {
        if (this._incomeTimerRadial.Progress >= this._incomeTimerRadial.MaxValue)
        {
            this._incomeTimerRadial.Progress = 0;
            this._player.IncreaseBank(this._player.Income);
        }

        this._incomeTimerRadial.Progress += 1;
    }
}

