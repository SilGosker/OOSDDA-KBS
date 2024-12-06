using Kbs.Business.Course;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Course.Components;

public class CourseDifficultyViewModel : ViewModel
{
    public CourseDifficultyViewModel(CourseDifficulty difficulties)
    {
        CourseDifficulty = difficulties;
    }

    public CourseDifficulty CourseDifficulty { get; }

    // Override ToString to display the BoatTypeSeats in Dutch
    public override string ToString()
    {
        return CourseDifficulty.ToDutchString();
    }
}
