using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopPet.Handlers;
using DesktopPet.MiniGames;

namespace DesktopPet.WPF.WindowViewModels;

public partial class MiniGameViewModel : ObservableObject
{
    [ObservableProperty]
    private int _foodScore;
    
    [ObservableProperty]
    private int _thirstScore;
    
    public ObservableCollection<CollectableViewModel> Collectables { get; } = new();

    private readonly Random _rand = new();

    public event Action? GameFinished;
    public event Action<int>? AddRemainingTime;
    
    private double _canvasWidth;
    private double _canvasHeight;
    private readonly PetBrain _brain;

    public MiniGameViewModel(PetBrain brain)
    {
        _brain = brain;
    }

    public void SetCanvasSize(double canvasWidth, double canvasHeight)
    {
        _canvasWidth = canvasWidth;
        _canvasHeight = canvasHeight;
    }

    [RelayCommand]
    private void FinishGame() => GameFinished?.Invoke();
    
    public void Tick()
    {
        // create collectable at random pos
        if (_rand.Next(0, 30) == 1)
        {
            switch (_rand.Next(0, 2))
            {
                case 0:
                    CreateFood();
                    break;
                case 1:
                    CreateThirst();
                    break;
            }
        }

        MoveAndCollideCollectables();
    }
    
    private void CreateFood()
    {
        CreateFood(_rand.Next(0, (int)_canvasWidth - 20), 0, 2);
    }
    
    private void CreateFood(int posX, int posY, int speed)
    {
        CreateCollectable(CollectableType.FOOD, "pack://application:,,,/Assets/Sprites/Food/apple.png", posX, posY, speed);
    }

    private void CreateThirst()
    {
        CreateThirst(_rand.Next(0, (int)_canvasWidth - 20), 0, 2);
    }
    
    private void CreateThirst(int posX, int posY, int speed)
    {
        CreateCollectable(CollectableType.THIRST, "pack://application:,,,/Assets/Sprites/Food/WaterBottle.png", posX, posY, speed);
    }

    private void CreateCollectable(CollectableType type, string uri, int posX, int posY, int speed)
    {
        var c = new CollectableViewModel()
        {
            Type = type,
            ImageUri = new Uri(uri),
            X = posX,
            Y = posY,
            Speed = speed
        };
        Collectables.Add(c);
    }

    private void MoveAndCollideCollectables()
    {
        for (int i = Collectables.Count - 1; i >= 0; i--)
        {
            var collectable = Collectables[i];
            // move food
            collectable.Move(collectable.Speed);

            // collide
            var foodRect = collectable.CollisionRect;
            var playerRect = _brain.PetWindow.GetCollisionRect();

            if (foodRect.IntersectsWith(playerRect))
            {
                // Collision with player
                switch (collectable.Type)
                {
                    case CollectableType.FOOD: FoodScore++; break;
                    case CollectableType.THIRST: ThirstScore++; break;
                }
                AddRemainingTime?.Invoke(1);
                Collectables.Remove(collectable);
            }
            else if (collectable.Y > _canvasHeight)
            {
                // out of screen (bottom)
                Collectables.Remove(collectable);
            }
        }
    }
}