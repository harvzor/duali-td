using Godot;
using System;

public partial class UserInterface : CanvasLayer
{
    private int _bank = 100;
    private Label _bankLabel;
    
    private int _income;
    private Label _incomeLabel;

    private TowerSpawner _towerSpawner;
    private PathSpawner _pathSpawner;

    public override void _Ready()
    {
        this._bankLabel = this.FindChild("Bank") as Label;
        this.SetBank(this._bank);
        
        this._incomeLabel = this.FindChild("Income") as Label;
        this.IncreaseIncome(0);
        
        this._towerSpawner = this
            .GetTree()
            .GetRoot()
            .GetChild(0)
            .GetNode<TowerSpawner>(nameof(TowerSpawner));

        this._pathSpawner = this
            .GetTree()
            .GetRoot()
            .GetChild(0)
            .GetNode<PathSpawner>(nameof(PathSpawner));
    }

    private void SetBank(int newBank)
    {
        this._bank = newBank;
        this._bankLabel.Text = this._bank + "G";
    }
    
    private void IncreaseIncome(int amount)
    {
        this._income += amount;
        this._incomeLabel.Text = this._income + "⬆️";
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
        if (this._towerSpawner.Cost > this._bank)
            return;

        this.SetBank(this._bank - this._towerSpawner.Cost);
        this._towerSpawner.SpawnTower(position);
    }

    public void SpawnCritter()
    {
        if (this._pathSpawner.Cost > this._bank)
            return;

        IncreaseIncome(this._pathSpawner.Cost);
        this.SetBank(this._bank - this._pathSpawner.Cost);

        this._pathSpawner.SpawnCritter();
    }
}
