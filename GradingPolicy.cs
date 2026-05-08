using System;
using System.Collections.Generic;
using System.Text;

namespace StudentGradingSystem
{
    internal interface GradingPolicy
    {
        double CalculateFinalGradePerCourse(List<GradeComponent> gradeComponents);
    }
}
