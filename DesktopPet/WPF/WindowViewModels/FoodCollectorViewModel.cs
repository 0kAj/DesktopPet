using DesktopPet.Handlers;

namespace DesktopPet.WPF.WindowViewModels;

public class FoodCollectorViewModel : MiniGameViewModel
{
    public FoodCollectorViewModel(PetBrain brain) : base(brain)
    {
    }

    public override void Tick()
    {
        base.Tick();
        // create collectable at random pos
        if (Rand.Next(0, 30) == 1)
            switch (Rand.Next(0, 2))
            {
                case 0:
                    CreateFood();
                    break;
                case 1:
                    CreateThirst();
                    break;
            }
    }
}