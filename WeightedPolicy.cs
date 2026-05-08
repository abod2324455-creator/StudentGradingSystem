using System;
using System.Collections.Generic;
using System.Text;

namespace StudentGradingSystem
{
    internal class WeightedPolicy
    {
        double totagrade = 0;
        public double CalculateFinalGrade(List<GradeComponent> gradeComponents)
        {
            foreach (var component in gradeComponents)
            {
                totagrade += component.Score * component.Weight;
            }
            return totagrade;
        }
    }
}
