using Kbs.Business.Course;

namespace Kbs.Wpf.Course.Read.Details;

public class ReadDetailsCourseDifficultyViewModel(CourseDifficulty difficulty)
{
    public CourseDifficulty Difficulty { get; set; } = difficulty;

    // override ToString to display the difficulty in the ComboBox
    public override string ToString()
    {
        return Difficulty.ToDutchString();
    }
}