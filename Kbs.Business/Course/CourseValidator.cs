using Kbs.Business.Helpers;

namespace Kbs.Business.Course
{
    public class CourseValidator
    {
        public Dictionary<string, string> ValidateForCreate(CourseEntity course)
        {
            ThrowHelper.ThrowIfNull(course);
            Dictionary<string, string> validationResult = new();

            if (string.IsNullOrWhiteSpace(course.Name))
            {
                validationResult.Add(nameof(course.Name), "Naam is verplicht");
            }
            else if (course.Name.Length > 255)
            {
                validationResult.Add(nameof(course.Name), "Naam mag niet langer zijn dan 255 karakters");
            }

            if (!Enum.IsDefined(course.Difficulty))
            {
                validationResult.Add(nameof(course.Difficulty), "Moeilijkheid is verplicht");
            }

            byte[] pngImageHeader = new byte[]
            {
                0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A
            };

            byte[] jpgImageHeader = new byte[]
            {
                0xFF, 0xD8, 0xFF
            };

            if (course.Image == null || course.Image.Length == 0)
            {
                validationResult.Add(nameof(course.Image), "Afbeelding is verplicht");
            }
            else if (!course.Image.Take(pngImageHeader.Length).SequenceEqual(pngImageHeader) &&
                     !course.Image.Take(jpgImageHeader.Length).SequenceEqual(jpgImageHeader))
            {
                validationResult.Add(nameof(course.Image), "Afbeelding is geen PNG of JPG");
            }

            return validationResult;
        }

        public Dictionary<string, string> ValidateForUpdate(CourseEntity course)
        {
            var result = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(course.Name))
            {
                result.Add(nameof(course.Name), "Naam is verplicht");
            }
            else if (course.Name.Length > 255)
            {
                result.Add(nameof(course.Name), "Naam mag niet langer zijn dan 255 karakters");
            }

            if (course.Difficulty == default)
            {
                result.Add(nameof(course.Difficulty), "Moeilijkheidsgraad is verplicht");
            }

            byte[] pngImageHeader = new byte[]
            {
                0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A
            };

            byte[] jpgImageHeader = new byte[]
            {
                0xFF, 0xD8, 0xFF
            };

            if (course.Image == null || course.Image.Length == 0)
            {
                result.Add(nameof(course.Image), "Afbeelding is verplicht");
            }
            else if (!course.Image.Take(pngImageHeader.Length).SequenceEqual(pngImageHeader) &&
                    !course.Image.Take(jpgImageHeader.Length).SequenceEqual(jpgImageHeader))
            {
                result.Add(nameof(course.Image), "Afbeelding is geen PNG of JPG");
            }

            return result;
        }
    }
}
