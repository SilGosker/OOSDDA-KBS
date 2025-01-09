using Kbs.Business.Course;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Course.Components;

public class CourseDifficultyViewModel(CourseDifficulty difficulties) : ViewModel
{
    public CourseDifficulty CourseDifficulty { get; } = difficulties;

    // Override ToString to display the BoatTypeSeats in Dutch
    public override string ToString()
    {
        return CourseDifficulty.ToDutchString();
    }
}
