using Kbs.Business.BoatType;
using Kbs.Business.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
    }
}
