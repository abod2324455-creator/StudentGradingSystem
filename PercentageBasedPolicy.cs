using System;
using System.Collections.Generic;
using System.Text;

namespace StudentGradingSystem
{
    internal class PercentageBasedPolicy : GradingPolicy
    {
        public double CalculateFinalGrade(List<GradeComponent> gradeComponents)
        {
            double totalGrade = 0;
            foreach (var component in gradeComponents)
            {
                totalGrade += component.Score;
            }
            double percentage = (totalGrade / 100) * 100;
            return percentage;
        }
    }
}
