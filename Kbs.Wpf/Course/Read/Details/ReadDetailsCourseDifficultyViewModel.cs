using Kbs.Business.Course;

namespace Kbs.Wpf.Course.Read.Details;

public class ReadDetailsCourseDifficultyViewModel
{
    public CourseDifficulty Difficulty { get; set; }

    public ReadDetailsCourseDifficultyViewModel(CourseDifficulty difficulty)
    {
        Difficulty = difficulty;
    }

    // override ToString to display the difficulty in the ComboBox
    public override string ToString()
    {
        return Difficulty.ToDutchString();
    }
}